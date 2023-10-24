using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    CollisionManager collisionManager;

    List<SpriteInfo> enemies;

    protected SpawnManager() { }

    // Start is called before the first frame update
    void Start()
    {
        enemies = collisionManager.enemySprites;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies != null)
        {
            for (int i = enemies.Count-1; i >= 0; i--)
            {
                //Debug.Log(enemies[i].transform.position.y);
;                if (enemies[i].transform.position.y < (-collisionManager.totalCamHeight / 2))
                {
                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                }
            }
        }

        if (enemies.Count < 5) //spawn more enemies if below 5
        {
            Spawn();
        }
    }

    private GameObject SpawnCreature()
    {
        return Instantiate(enemyPrefab, transform);
    }

    public void Spawn()
    {
        List<GameObject> temp = new List<GameObject>();
        int ran = Random.Range(5, 15);
        //DestroyAnimals();
        for (int i = 0; i < ran; i++)
        {

            temp.Add(SpawnCreature());

            //randomize positions
            Vector2 spawnPosition = new Vector2(
                Random.Range(-7.3f, 7.3f),
                Random.Range(6f, 15f)
                );
            temp[i].transform.position = spawnPosition;


            //enemies[i].color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        }

        foreach(GameObject go in temp)
        {
            enemies.Add((SpriteInfo)go.GetComponent("SpriteInfo"));
        }
    }

    /*private void DestroyEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }*/
}