using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 1f;
    public Vector2 velocity = Vector2.zero;
    public Vector2 direction = Vector2.down;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = direction.normalized;

        //velocity = direction * speed
        velocity = direction * speed * Time.deltaTime;

        //add velocity to pos
        transform.position += (Vector3)velocity;
    }
}
