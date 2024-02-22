using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float moveInput;
    public Animator animator;
    public float high = 8f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Run", true);
            moveInput = 1;
            GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Run", true);
            moveInput = -1;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        else
        {
            moveInput = 0;
            animator.SetBool("Run", false);
        }
        transform.Translate(Vector2.right * moveSpeed * moveInput * Time.deltaTime);

        // jump
        animator.SetFloat("AirSpeedY", rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, high);
            animator.SetBool("Grounded", false);
        }
        if(Input.GetKey(KeyCode.J)) 
        {
            animator.SetTrigger("Attack1");
        }


        //fall  
        if (animator.GetFloat("AirSpeedY") < 0)
        {
            animator.SetBool("Grounded", false);

        }
        else { animator.SetBool("Grounded", true); }
    }
}
