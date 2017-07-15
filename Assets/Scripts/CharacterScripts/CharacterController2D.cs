using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public GroundCheck groundCheck;
    public float maxSpeedX = 1;
    public float acceleration = 0.1f;
    public float jumpPower = 1;
    public float jumpCooldown = 1f;

    private float speed;
    private bool facingRight = true;
    private float timeSinceJump = 0;
    private bool wasOnGround = false;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!wasOnGround && groundCheck.OnLayer(Tags.GROUND_LAYER)){
            speed = speed/2;
        }
       // timeSinceJump += Time.deltaTime;
       // if (timeSinceJump > jumpCooldown)
       // {
        timeSinceJump = 0;
        if (Input.GetButtonDown("Jump") && groundCheck.OnLayer(Tags.GROUND_LAYER))
        {
            rb.AddForce(Vector2.up * jumpPower);
        }
        // }
        wasOnGround = groundCheck.OnLayer(Tags.GROUND_LAYER);
    }

    void FixedUpdate()
    {
        HandleHorizontalSpeed();
        HandleSwitchingDirections();

        anim.SetFloat("SpeedY", rb.velocity.y);
        anim.SetFloat("SpeedX", speed);
    }

    private void HandleSwitchingDirections()
    {
        if (facingRight && speed < 0)
        {
            spriteRenderer.flipX = true;
            facingRight = false;
        }
        else if (!facingRight && speed > 0)
        {
            spriteRenderer.flipX = false;
            facingRight = true;
        }
    }
    private void HandleHorizontalSpeed()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        if (horizontalAxis != 0)
        {
            speed = speed + acceleration * Mathf.Sign(horizontalAxis);
            speed = Mathf.Abs(speed) > Mathf.Abs(maxSpeedX) ? maxSpeedX * Mathf.Sign(horizontalAxis) : speed;
            rb.velocity = new Vector2(speed, groundCheck.OnLayer(Tags.GROUND_LAYER) ? 0 : rb.velocity.y);
        }
        else
        {
            speed = rb.velocity.x;
        }
    }
}
