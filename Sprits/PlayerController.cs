using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Collider2D circle,box;
    public LayerMask Ground;
    public float speed, jumpforce;
    
    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movements();
        SwitchAnim();
    }

    void Movements()
    {
        float hmove = Input.GetAxis("Horizontal");
        float faced = Input.GetAxisRaw("Horizontal");
        
        //行走
        if (hmove != 0)
        {
            rb.velocity = new Vector2(speed * hmove * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(faced));
        }
        //面向
        if (faced != 0)
        {
            transform.localScale = new Vector3(faced, 1, 1);
        }
        
        
        Jump();
    }

    //跳跃
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && circle.IsTouchingLayers(Ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            anim.SetBool("jumping", true);//触发跳起的动画
        }
    }
    
    //动画切换
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        //跳跃下降动画
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)//y轴受重力
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (circle.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling", false);
        }
        //奔跑或者静止下降
        if (rb.velocity.y < 0 && !circle.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling", true);
        }
        else
        {
            anim.SetBool("falling", false);
        }
        
    }
}
