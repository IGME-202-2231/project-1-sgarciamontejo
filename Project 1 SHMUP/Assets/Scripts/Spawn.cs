using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemy1Prefab;
    [SerializeField] 
    GameObject enemy2Prefab;

    float ran;

    protected SpawnManager() { }

    // Start is called before the first frame update
    void Start()
    {
        Spawn1();
    }

    // Update is called once per frame
    void Update()
    {
        ran = Random.Range(0f, 1f);
        if(ran <= .5f)
        {
            if (transform.childCount < 5)
            {
                ran = Random.Range(1, 3);
                for (int i = 0; i < ran; i++)
                {
                    Spawn2();
                }
            }
        }
        else
        {
            if (transform.childCount < 5) //spawn more enemies if below 5
            {
                ran = Random.Range(5, 15);
                for (int i = 0; i < ran; i++)
                {
                    Spawn1();
                }
            }
        }
    }

    private GameObject Spawn1Creature()
    {
        return Instantiate(enemy1Prefab, transform);
    }

    public void Spawn1()
    {
        //randomize positions
        Vector2 spawnPosition = new Vector2(
            Random.Range(-7.3f, 7.3f),
            Random.Range(6f, 15f)
            );
        Spawn1Creature().transform.position = spawnPosition;
    }

    private GameObject Spawn2Creature()
    {
        return Instantiate(enemy2Prefab, transform);
    }

    public void Spawn2()
    {
        //randomize positions
        Vector2 spawnPosition = new Vector2(
            Random.Range(-7.3f, 7.3f),
            Random.Range(6f, 15f)
            );
        Spawn2Creature().transform.position = spawnPosition;
    }
}