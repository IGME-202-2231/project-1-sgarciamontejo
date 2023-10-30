using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject missileManager;
    [SerializeField]
    CollisionManager collisionManager;
    [SerializeField]
    GameObject missile;

    [SerializeField] float speed = 1f;
    public Vector2 velocity = Vector2.zero;
    public Vector2 direction = Vector2.down;

    float totalCamHeight;
    float totalCamWidth;

    float cooldown = 2f;
    float cooldownTimestamp;
    float ran;

    [SerializeField]
    bool type; // true - enemy1 | false - enemy2

    // Start is called before the first frame update
    void Start()
    {
        //missile manager
        missileManager = GameObject.Find("MissileManager");

        GameObject Collision = GameObject.Find("Collision");
        collisionManager = Collision.GetComponent<CollisionManager>();
        collisionManager.onAddSprite((SpriteInfo)transform.GetComponent("SpriteInfo"), "Enemy");
        totalCamHeight = Camera.main.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (type)
        {
            ran = Random.Range(.7f, 2.2f); //random cool down addition
            if (!(Time.time < cooldownTimestamp))
            {
                cooldownTimestamp = Time.time + cooldown + ran; //firerate - cooldown
                Instantiate(missile, transform.position, Quaternion.identity, missileManager.transform);
            }
        }

        direction = direction.normalized;

        //velocity = direction * speed
        velocity = direction * speed * Time.deltaTime;

        //add velocity to pos
        transform.position += (Vector3)velocity;

        if(transform.position.y < -(totalCamHeight/2))
        {
            Debug.Log("offscreen");
            collisionManager.onRemoveSprite((SpriteInfo)transform.GetComponent("SpriteInfo"), "Enemy");
            Destroy(transform.gameObject);
        }
    }
}
