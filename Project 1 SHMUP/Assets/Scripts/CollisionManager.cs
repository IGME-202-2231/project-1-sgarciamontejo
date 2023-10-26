using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    GameObject Player1;
    //[SerializeField]
    //GameObject Player2;

    [SerializeField]
    SpawnManager Spawn_Manager;

    SpriteInfo playerSprite;
    List<SpriteInfo> enemySprites = new List<SpriteInfo>();
    List<SpriteInfo> missileSprites = new List<SpriteInfo>();

    public float totalCamHeight;
    public float totalCamWidth;

    public int enemyCount = 0;
    public int missileCount = 0;

    int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalCamHeight = Camera.main.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * Camera.main.aspect;

        playerSprite = Player1.GetComponent<SpriteInfo>();
        //playerSprites.Add(Player2.GetComponent<SpriteInfo>());
    }

    // Update is called once per frame
    void Update()
    {
        if(enemySprites != null && missileSprites != null)
        {
            for (int i = enemySprites.Count - 1; i >= 0; i--)
            {
                for (int j = missileSprites.Count - 1; j >= 0; j--)
                {
                    if (enemySprites[i].friendly != missileSprites[j].friendly && AABBCollision(enemySprites[i], missileSprites[j]))
                    {
                        Debug.Log("COLLIDE");
                        Destroy(enemySprites[i].GameObject());
                        Destroy(missileSprites[j].GameObject());

                        enemySprites.Remove(enemySprites[i]);
                        missileSprites.Remove(missileSprites[j]);

                        enemyCount--;
                        missileCount--;

                        break;
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
            Score
        }
    }
}