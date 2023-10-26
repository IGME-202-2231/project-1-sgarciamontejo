using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    int ran;

    protected SpawnManager() { }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (transform.childCount < 5) //spawn more enemies if below 5
        {
            ran = Random.Range(5, 15);
            for (int i = 0; i < ran; i++)
            {
                Spawn();
            }
        }
    }

    private GameObject SpawnCreature()
    {
        return Instantiate(enemyPrefab, transform);
    }

    public void Spawn()
    {
        //randomize positions
        Vector2 spawnPosition = new Vector2(
            Random.Range(-7.3f, 7.3f),
            Random.Range(6f, 15f)
            );
        SpawnCreature().transform.position = spawnPosition;
    }
}