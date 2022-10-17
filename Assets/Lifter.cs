using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifter : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform TopPoint, BottomPoint;
    private float TopY, BottomY;
    private bool isUp = true;
    public float Speed;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TopY = TopPoint.position.y;
        BottomY = BottomPoint.position.y;
        Destroy(TopPoint.gameObject);
        Destroy(BottomPoint.gameObject);

    }

    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (rb.position.y > TopY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (rb.position.y < BottomY)
            {
                isUp = true;
            }
        }
    }
}
