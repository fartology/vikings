using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float moveForce = 50;
    public float maxSpeed = 5;
    public float friction = 1.2f;
    public float jumpForce = 400;

    [SerializeField]
    private bool grounded;
    private bool lookingRight = true;
    private bool jump = false;
    private bool doubleJump = true;
    private Rigidbody2D rb2d;
    private Transform groundCheck;
    private Animator animController;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        animController = GetComponent<Animator>();
        groundCheck = transform.Find("groundCheck");
	}

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                jump = true;
            } else if(!doubleJump)
            {
                jump = true;
                doubleJump = true;
            }
            
        }
    }

    void Jump()
    {

    }
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontalMovement = Input.GetAxis("Horizontal");

        // Only allow movement if player is grounded.
        if (grounded)
        {
            if (horizontalMovement * rb2d.velocity.x < maxSpeed)
            {
                rb2d.AddForce(Vector2.right * horizontalMovement * moveForce);
            }

            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            {
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
            }
            else if (horizontalMovement == 0 && Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon)
            {   
                // stopping
                rb2d.velocity = new Vector2(rb2d.velocity.x / friction, rb2d.velocity.y);
            }

            // Reset doubleJump variable when player is again grounded.
            if (doubleJump)
            {
                doubleJump = false;
            }

            if (rb2d.velocity.x > Mathf.Epsilon)
            {
                if (!lookingRight)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                lookingRight = true;
            } else if (rb2d.velocity.x < -Mathf.Epsilon) {
                if (lookingRight)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                lookingRight = false;
            }
            animController.SetBool("run", Mathf.Abs(rb2d.velocity.x) > 0);
            
        }

        if (jump)
        {
            jump = false;
            animController.SetTrigger("jump");
            rb2d.AddForce(new Vector2(0.0f, jumpForce));
        }

        Debug.Log(rb2d.velocity.y);
        animController.SetBool("falling", rb2d.velocity.y < 0);
        animController.

    }
}
