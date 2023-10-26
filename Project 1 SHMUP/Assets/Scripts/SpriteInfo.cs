using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer render;

    public Color color;

    public float minX;
    public float x;
    public float maxX;

    public float minY;
    public float y;
    public float maxY;

    public bool friendly;

    public Vector2 size;
    void Start()
    {
        //render = this.GetComponent<SpriteRenderer>();
        color = Color.white;
        size = render.size;
    }

    void Update()
    {
        render.color = color;

        minX = render.transform.position.x - (size.x);
        x = render.transform.position.x;
        maxX = render.transform.position.x + (size.x);

        minY = render.transform.position.y - (size.y);
        y = render.transform.position.y;
        maxY = render.transform.position.y + (size.y);
    }
}