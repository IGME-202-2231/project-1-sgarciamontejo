using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    CollisionManager collisionManager;

    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();

    protected SpawnManager() { }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject SpawnCreature()
    {
        return Instantiate(enemyPrefab, transform);
    }

    public void Spawn()
    {
        //DestroyAnimals();
        for (int i = 0; i < 10; i++)
        {
            enemies.Add(SpawnCreature());

            //randomize positions
            Vector2 spawnPosition = new Vector2(
                Random.Range(1f, collisionManager.totalCamWidth-1f),
                Random.Range(collisionManager.totalCamHeight-3f, collisionManager.totalCamHeight)
                );
            enemies[i].transform.position = spawnPosition;


            //enemies[i].color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        }
    }

    private void DestroyEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }
}