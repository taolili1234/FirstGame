using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemies
{
    private Rigidbody2D rb;
    private Collider2D Coll;
    private bool FaceLeft = true;
    private float LeftX, RightX;

    public Transform LeftPoint, RightPoint;
    public float Speed, JumpForce;
    public LayerMask Ground;

    protected override void Start()//override 是和父函数里的 virtual  配合使用的，固定搭配
    {
        base.Start();//获取父函数里面的参数
        rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();

        //transform.DetachChildren();//运行时解除父子关系

        LeftX = LeftPoint.position.x;
        RightX = RightPoint.position.x;
        Destroy(LeftPoint.gameObject);
        Destroy(RightPoint.gameObject);
    }

   
    void Update()
    {
        SwitchAnim();
    }
     void Movement()
    {
        if(FaceLeft)
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if(transform.position.x< LeftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                FaceLeft = false;
            }
        }
        else
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
            if (transform.position.x > RightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                FaceLeft = true; ;
            }
        }
    }

    void SwitchAnim()
    {
        if(Anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        else if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
            Anim.SetBool("idle",true);
        }
    }
}





