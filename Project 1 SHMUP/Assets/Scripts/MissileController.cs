using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    //prefab
    [SerializeField]
    Vector2 direction;

    float speed = 3.5f;
    Vector2 velocity;

    float totalCamHeight;
    float totalCamWidth;

    // Start is called before the first frame update
    void Start()
    {
        totalCamHeight = Camera.main.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * Camera.main.aspect;
    }

    void Update()
    {
        direction = direction.normalized;

        //velocity = direction * speed
        velocity = direction * speed * Time.deltaTime;

        //add velocity to pos
        transform.position += (Vector3)velocity;

        if (direction == Vector2.up)
        {
            if (transform.position.y > (totalCamHeight / 2) + 1)
            {
                Debug.Log("cool");
                Destroy(transform.GameObject());
            }
        }
    }
}
