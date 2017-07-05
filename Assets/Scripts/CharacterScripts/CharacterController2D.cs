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

    private float speed;
    private bool facingRight = true;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        HandleHorizontalSpeed();
        HandleSwitchingDirections();

        if (Input.GetButton("Jump") && groundCheck.OnLayer(Tags.GROUND_LAYER))
        {
            rb.AddForce(Vector2.up * jumpPower);
        }

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
