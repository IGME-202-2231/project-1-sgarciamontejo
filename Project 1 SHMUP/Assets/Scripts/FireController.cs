using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireController : MonoBehaviour
{
    //prefab
    [SerializeField]
    Vector2 direction;

    float speed = 3.5f;
    Vector2 velocity;

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
