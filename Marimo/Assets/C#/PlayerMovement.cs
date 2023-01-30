using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;

    bool jump = false;
    Vector2 move;
    int numPos = 1;

    //GroundCheck
    public LayerMask groundLayer;
    //bool isGrounded;
    public Transform groundCheck;
    
    //flip the sprite
    bool isFacingRight = true;

    private SpriteRenderer spriteRender;
    
    //hint
    public GameObject e;
    private bool E;
    public GameObject hint;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        move = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            move.x = -1 * speed;
            numPos = -1;
            Flip();
        }

        if (Input.GetKey(KeyCode.D))
        {
            move.x = 1 * speed;
            numPos = 1;
            spriteRender.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                jump = true;
                rb.AddForce(Vector2.up * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            hint.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
            jump = false;
            rb.AddForce(Vector2.up * 8, ForceMode2D.Impulse);

        }
        rb.AddForce(move * 6);
    }

    void Flip()
    {
        if (isFacingRight && numPos < 0f)
        {
            //isFacingRight = !isFacingRight;
            //Vector3 localSacle = transform.localScale;
            //localSacle.x *= -1;
            //transform.localScale = localSacle;
            spriteRender.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //throw new NotImplementedException();
        if (col.tag == "Door")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("End");
        }

        if (col.tag == "hint")
        {
            e.SetActive(true);
        }
    }
}
