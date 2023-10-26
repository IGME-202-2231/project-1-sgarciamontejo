using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    //prefab
    [SerializeField]
    Vector2 direction;

    [SerializeField]
    CollisionManager collisionManager;

    float speed = 3.5f;
    Vector2 velocity;

    float totalCamHeight;
    float totalCamWidth;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Collision = GameObject.Find("Collision");
        collisionManager = Collision.GetComponent<CollisionManager>();
        SpriteInfo sprite = (SpriteInfo)transform.GetComponent("SpriteInfo");
        collisionManager.onAddSprite(sprite, "Missile");
        if(!sprite.friendly)
        {
            speed = 3f;
        }
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
            if (transform.position.y > (totalCamHeight / 2) + .25)
            {
                collisionManager.onRemoveSprite((SpriteInfo)transform.GetComponent("SpriteInfo"), "Missile");
                Destroy(transform.gameObject);
            }
        }
        else if(direction == Vector2.down)
        {
            if (transform.position.y < -(totalCamHeight / 2) - .25)
            {
                collisionManager.onRemoveSprite((SpriteInfo)transform.GetComponent("SpriteInfo"), "Missile");
                Destroy(transform.gameObject);
            }
        }
    }
}
