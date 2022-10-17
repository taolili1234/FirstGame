using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator Anim;
    public Collider2D Coll;
    public Collider2D DisColl;
    public Collider2D DisColl1;
    public AudioSource JumpAudio;
    public AudioSource HurtAudio;
    public AudioSource CollectionsAudio;
    public Transform CeilingCheck;
    public Text CherryNum;
    public float Speed, JumpForce;   //不用在代码里设置参数直接在inspection中的player下直接修改
    public LayerMask ground;//LayerMask获取碰撞体地面的图层，在Hierarchy中的Layer中添加命名，选择后拖拽到Player下刚添加public LayerMask控件中即可生效
    private int Cherry, Gem;
    private bool isHurt;

    void Start()
    {

    }

    void Update()
    {
        Jump();
        Crouch();//蹲下
    }
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }

        SwitchAnim();
    }

    void Movement()
    {
        float HorizontalMove = Input.GetAxis("Horizontal");      //如果按下按键A那么Horizontal =-1    按下D键则相反 =1   没按=0  用于判定方向
        float Facedirection = Input.GetAxisRaw("Horizontal");    //GetAxisRaw与GetAxis的区别是直接获取-1- 1- 0   中间没有float小数过渡  方向变换更加直接
                                                                 //Input.GetAxis("horizontal");先获取按键项目
                                                                 //input/Axes下的项目名(Horizontal) 这个项目只有-1-1-0   3个参数，判定角色移动

        if (HorizontalMove != 0)//角色判定移动
        {
            rb.velocity = new Vector2(HorizontalMove * Speed * Time.fixedDeltaTime, rb.velocity.y);
            Anim.SetFloat("running", Mathf.Abs(HorizontalMove));//添加Mathf.Abs是因为按住A时代入的值为负数
        }

        //角色方向
        //transform.localScale是为了获取inspector窗口下的Scale的参数
        //因为Scale有xyz三坐标所以  这边得用new Vector3来代入进transform.localScale中
        //替换原来的坐标
        //这次是player换方向，所以只用到x轴，其他保持不变
        if (Facedirection != 0)//角色方向
        {
            transform.localScale = new Vector3(Facedirection, 1, 1);
        }
    }
    //动画切换
    void SwitchAnim()
    {
        if (rb.velocity.y < 0.1f && !Coll.IsTouchingLayers(ground))
        {
            Anim.SetBool("falling", true);
        }

        if (Anim.GetBool("jumping"))//获得jumping真假值
        {
            if (rb.velocity.y < 0.1f)//velocity.y时y轴的速度
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            Anim.SetBool("hurt", true);
            Anim.SetFloat("running", 0);

            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                Anim.SetBool("hurt", false);
                isHurt = false;
            }
        }
        else if (Coll.IsTouchingLayers(ground))//地面碰撞检测--因为上面添加了public Collider2d coll，所以可以把Hierarchy中Player的刚体拖拽到coll控件里
                                               //IsTouchingLayers是碰撞  （ground）是被碰撞刚体
        {
            Anim.SetBool("falling", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce * Time.fixedDeltaTime);
            JumpAudio.Play();
            Anim.SetBool("jumping", true);//SetBool//float//int//Trigger//调用动画及参数
        }
    }

    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CeilingCheck.position, 0.2f, ground))//检测中心点，范围，障碍物
        {
            if (Input.GetButton("Crouch"))
            {
                Anim.SetBool("crouching", true);
                DisColl.enabled = false;
            }
            else
            {
                Anim.SetBool("crouching", false);
                DisColl.enabled = true;
            }
        }
    }




    //物品收集
    private void OnTriggerEnter2D(Collider2D collision)//收集物品碰撞属性下的 isTrigger 勾选的情况下使用
    {
        if (collision.tag == "Cherry")
        {
            CollectionsAudio.Play();//播放效果音
            Destroy(collision.gameObject);//销毁程序
            Cherry += 1;
            CherryNum.text = Cherry.ToString();//Cherry是int数值，不能直接转换成text文本格式所以用  .ToString来转换
        }
        else if (collision.tag == "Gem")
        {
            CollectionsAudio.Play();//播放效果音
            Destroy(collision.gameObject);//销毁程序
            Gem += 1;
        }
    }
    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)//碰撞属性下的   isTrigger  没有勾选的情况下使用   一般是有rigidbody的情况下使用
        {
        if (collision.gameObject.tag == "Enemies")
        {
            Enemies enemies = collision.gameObject.GetComponent<Enemies>();//调用Enemies这个外部函数参数
            if (Anim.GetBool("falling")&&transform.position.y>collision.gameObject.transform.position.y+0.8)
            {
                enemies.Jumpon();//Enemies函数下的参数
                rb.velocity = new Vector2(rb.velocity.x, JumpForce * Time.fixedDeltaTime);
                Anim.SetBool("jumping", true);//SetBool//float//int//Trigger//调用动画及参数
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)//左边
            {
                rb.velocity = new Vector2(-1, 3);//角色的击退效果
                HurtAudio.Play();//播放效果音
                isHurt = true;
                Anim.SetBool("jumping", false);
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)//右边
            {
                rb.velocity = new Vector2(1, 3);//角色的击退效果
                HurtAudio.Play();//播放效果音
                isHurt = true;
                Anim.SetBool("jumping", false);
            }
        }
    }
}
