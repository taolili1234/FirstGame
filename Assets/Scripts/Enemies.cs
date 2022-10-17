using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource DeathAudio;

    protected virtual void Start()//加上 virtual虚拟函数可以让在子函数自行添加需要的参数而不被父函数使用
    {
        Anim = GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
    }

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void Jumpon()
    {
        DeathAudio.Play();
        Anim.SetTrigger("death");
    }
}
