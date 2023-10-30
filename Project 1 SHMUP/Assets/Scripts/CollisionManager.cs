using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    GameObject Player1;
    [SerializeField]
    GameObject Player2;
    [SerializeField]
    TextMesh ScoreText;

    [SerializeField]
    SpawnManager Spawn_Manager;

    List<SpriteInfo> playerSprites = new List<SpriteInfo>();
    List<SpriteInfo> enemySprites = new List<SpriteInfo>();
    List<SpriteInfo> missileSprites = new List<SpriteInfo>();

    public float totalCamHeight;
    public float totalCamWidth;

    public int enemyCount = 0;
    public int missileCount = 0;

    int Score = 0;
    string displayScore;


    int player1Lives = 3;
    List<GameObject> player1LifeSprites = new List<GameObject>();

    int player2Lives = 3;
    List<GameObject> player2LifeSprites = new List<GameObject>();
    int spacing = 40;

    [SerializeField]
    GameObject display_player1;
    [SerializeField]
    GameObject display_player2;
    [SerializeField]
    GameObject life;

    [SerializeField]
    TextMesh gameOverText;
    bool gameActive = true;

    // Start is called before the first frame update
    void Start()
    {
        displayScore = "Score: " + Score;
        setupLives("player1");
        setupLives("player2");

        totalCamHeight = Camera.main.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * Camera.main.aspect;

        playerSprites.Add(Player1.GetComponent<SpriteInfo>());
        playerSprites.Add(Player2.GetComponent<SpriteInfo>());
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        {
            if (enemySprites != null && missileSprites != null)
            {
                for (int i = enemySprites.Count - 1; i >= 0; i--)
                {
                    for (int j = missileSprites.Count - 1; j >= 0; j--)
                    {
                        if (enemySprites[i].friendly != missileSprites[j].friendly && AABBCollision(enemySprites[i], missileSprites[j]))
                        { //When collision occurs, remove both sprites and add score
                            Debug.Log("COLLIDE");
                            Destroy(enemySprites[i].GameObject());
                            Destroy(missileSprites[j].GameObject());

                            enemySprites.Remove(enemySprites[i]);
                            missileSprites.Remove(missileSprites[j]);

                            enemyCount--;
                            missileCount--;

                            Score += 50;
                            displayScore = "Score: " + Score;
                            ScoreText.text = displayScore;

                            break;
                        }
                    }
                }
            }

            if (playerSprites != null && missileSprites != null)
            {
                for (int i = playerSprites.Count - 1; i >= 0; i--)
                {
                    if ((i == 0 && player1Lives != 0) || (i == 1 && player2Lives != 0))
                    {
                        for (int j = missileSprites.Count - 1; j >= 0; j--)
                        {
                            if (playerSprites[i].friendly != missileSprites[j].friendly && AABBCollision(playerSprites[i], missileSprites[j]))
                            { //When collision occurs, remove missile sprite and remove a life
                                playerSprites[i].hit();

                                Debug.Log("COLLIDEplayer");
                                Destroy(missileSprites[j].GameObject());
                                missileSprites.Remove(missileSprites[j]);
                                missileCount--;

                                string plr = "player" + (i + 1);
                                loseLife(plr);

                                break;
                            }
                        }

                        for (int j = enemySprites.Count - 1; j >= 0; j--)
                        {
                            if (playerSprites[i].friendly != enemySprites[j].friendly && AABBCollision(playerSprites[i], enemySprites[j]))
                            { //When collision occurs, remove missile sprite and remove a life
                                playerSprites[i].hit();

                                Debug.Log("COLLIDEplayer");
                                Destroy(enemySprites[j].GameObject());
                                enemySprites.Remove(enemySprites[j]);
                                enemyCount--;

                                string plr = "player" + (i + 1);
                                loseLife(plr);

                                break;
                            }
                        }
                    }
                }
            }
        }
    }


    bool AABBCollision(SpriteInfo sprite1, SpriteInfo sprite2)
    {
        if (sprite1.minX < sprite2.maxX &&
            sprite1.maxX > sprite2.minX &&
            sprite1.minY < sprite2.maxY &&
            sprite1.maxY > sprite2.minY)
        {
            //Debug.Log("yeppp");
            return true;
        }
        return false;
    }

    public void onAddSprite(SpriteInfo sprite, string type)
    {
        if(type == "Missile")
        {
            missileSprites.Add(sprite);
            missileCount++;
        }
        else
        {
            enemySprites.Add(sprite);
            enemyCount++;
        }
    }

    public void onRemoveSprite(SpriteInfo sprite, string type)
    {
        if (type == "Missile")
        {
            missileSprites.Remove(sprite);
            missileCount--;
        }
        else
        {
            Debug.Log("mreow");
            enemySprites.Remove(sprite);
            enemyCount--;
        }
    }

    void loseLife(string player)
    {
        if (player == "player1")
        {
            Debug.Log("1");
            Destroy(player1LifeSprites[player1Lives-1]);

            player1Lives--;
            if(player1Lives <= 0)
            {
                //playerSprites.RemoveAt(0); //0 = player 1 || 1 = player 2
                Destroy(Player1);
            }
        }
        else if (player == "player2")
        {
            Debug.Log("2");
            Destroy(player2LifeSprites[2 - (player2Lives-1)]);

            player2Lives--;
            if (player2Lives <= 0)
            {
                //playerSprites.RemoveAt(1); //0 = player 1 || 1 = player 2
                Destroy(Player2);
            }
        }

        if(player1Lives == 0 && player2Lives == 0)
        {
            gameOver();
        }
    }

    void setupLives(string player)
    {
        if(player == "player1")
        {
            for(int i = 0; i < player1Lives; i++)
            {
                int x = -25 + (spacing * i);
                GameObject lifeSprite = Instantiate(life, new Vector3(0, 0, 0), Quaternion.identity, display_player1.transform);
                RectTransform rectTransform = lifeSprite.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(x, -25);
                player1LifeSprites.Add(lifeSprite);
            }
        }
        else if (player == "player2")
        {
            for (int i = 0; i < player2Lives; i++)
            {
                int x = -55 + (spacing * i);
                GameObject lifeSprite = Instantiate(life, new Vector3(0, 0, 0), Quaternion.identity, display_player2.transform);
                RectTransform rectTransform = lifeSprite.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(x, -25);
                player2LifeSprites.Add(lifeSprite);
            }
        }
    }

    void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "GAME OVER\nScore: " + Score;
        gameActive = false;
    }
}