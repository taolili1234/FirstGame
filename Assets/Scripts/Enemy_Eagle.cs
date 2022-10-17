using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemies
{
    private Rigidbody2D rb;
    private float TopY, BottomY;
    private bool isUP=true;

    public Transform TopPoint, BottomPoint;
    public float Speed;


    protected override void Start()//override 是和父函数里的 virtual  配合使用的，固定搭配
    {
        base.Start();//获取父函数里面的参数
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
        if (isUP)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > TopY)
            {
                isUP = false;
            }
        }
        else 
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < BottomY)
            {
                isUP = true;
            }
        }
    }
}
