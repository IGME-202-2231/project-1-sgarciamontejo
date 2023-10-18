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
    [SerializeField]
    GameObject Player2;

    [SerializeField]
    List<GameObject> missiles;
    [SerializeField]
    List<GameObject> enemies;

    List<SpriteInfo> playerSprites;
    List<SpriteInfo> missileSprites;
    List<SpriteInfo> enemySprites;

    public float totalCamHeight;
    public float totalCamWidth;

    // Start is called before the first frame update
    void Start()
    {
        playerSprites.Add(Player1.GetComponent<SpriteInfo>());
        playerSprites.Add(Player1.GetComponent<SpriteInfo>());
        foreach (GameObject f in enemies)
        {
            enemySprites.Add(f.GetComponent<SpriteInfo>());
        }
        foreach (GameObject j in missiles)
        {
            missileSprites.Add(j.GetComponent<SpriteInfo>());
        }

        totalCamHeight = Camera.main.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}