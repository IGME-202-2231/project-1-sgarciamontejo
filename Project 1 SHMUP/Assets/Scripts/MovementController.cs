using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 2.5f;

    public Vector2 velocity = Vector2.zero;
    public Vector2 direction = Vector2.zero;

    float totalCamHeight;
    float totalCamWidth;

    

    // Start is called before the first frame update
    void Start()
    {
        totalCamHeight = Camera.main.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteInfo sprite = (SpriteInfo)transform.gameObject.GetComponent("SpriteInfo");
        //Debug.Log(sprite.minY);
        Vector3 currentPosition = transform.position;
        if (currentPosition.x > totalCamWidth / 2)
        {
            currentPosition.x = -totalCamWidth / 2;
        }
        else if (currentPosition.x < -totalCamWidth / 2)
        {
            currentPosition.x = totalCamWidth / 2;
        }

        if (currentPosition.y > (totalCamHeight / 2) - (sprite.size.y / 2))
        {
            currentPosition.y = (totalCamHeight / 2) - (sprite.size.y / 2);
        }
        else if (currentPosition.y < (-totalCamHeight / 2) + (sprite.size.y / 2))
        {
            currentPosition.y = (-totalCamHeight / 2) + (sprite.size.y / 2);
        }
        transform.position = currentPosition;


        direction = direction.normalized;

        //if (direction != Vector2.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        //}

        //update velocity
        //velocity = direction * speed
        velocity = direction * speed * Time.deltaTime;

        //add velocity to pos
        transform.position += (Vector3)velocity;
    }
}