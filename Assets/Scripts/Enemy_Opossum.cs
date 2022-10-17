using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Opossum : Enemies
{
    private Rigidbody2D rb;
    private Collider2D Coll;
    private float LeftX, RightX;
    private bool FaceLeft = true;

    public Transform LetfPoint, RightPoint;
    public float Speed;

    protected override void Start()//override 是和父函数里的 virtual  配合使用的，固定搭配
    {
        base.Start();//获取父函数里面的参数
        rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();

        LeftX = LetfPoint.position.x;
        RightX = RightPoint.position.x;
        Destroy(LetfPoint.gameObject);
        Destroy(RightPoint.gameObject);
    }

    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (FaceLeft)
        {
            rb.velocity = new Vector2(-Speed,rb.velocity.y);
            if (transform.position.x < LeftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                FaceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            if (transform.position.x > RightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                FaceLeft = true;
            }
        }
    }
}
