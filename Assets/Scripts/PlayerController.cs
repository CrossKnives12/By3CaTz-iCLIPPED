using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public int extraJumpsValue;

    public float dashSpeed;
    public float startDashTime;

    public Transform frontCheck;
    public float wallSlidingSpeed;

    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    private int extraJumps;

    private bool isTouchingFront;
    private bool wallSliding;



    [SerializeField]
    private float dashTime;

    [SerializeField]
    private int direction;

    [SerializeField]
    private int currentCounter;

    public int startingCounter;

    // Start is called before the first frame update
    void Start()
    {
        currentCounter = startingCounter;
        dashTime = startDashTime;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
        //PlayerDash();
        PlayerMovement();
        PlayerWallJump();

        if(currentCounter == 1)
        {
            //PlayerDash();
        }
        DashCD();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //moveInput = Input.GetAxisRaw("Horizontal");
        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Flip the player facing direction
        if(facingRight == false && moveInput == 2)
        {
            Flip();
        }
        else if(facingRight == true && moveInput == 1)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void PlayerJump()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0 || (Input.GetKeyDown(KeyCode.W) && extraJumps > 0))
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true || Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void PlayerDash()
    {
        // Air dash is only allowed
        // Air dash will have a cooldown
        if(isGrounded == false)
        {
            if (direction == 0)
            {
                if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
                {
                    direction = 1;
                }
                else if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
                {
                    direction = 2;
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (direction == 1)
                    {
                        rb.velocity = Vector2.left * dashSpeed;
                        currentCounter = 0;
                    }
                    else if (direction == 2)
                    {
                        rb.velocity = Vector2.right * dashSpeed;
                        currentCounter = 0;
                    }
                }
            }
        }
        
    }

    void DashCD()
    {
        if (currentCounter >= 1)
        {
            currentCounter = 1;
        }

        if (currentCounter <= 0 && isGrounded == true)
        {
            currentCounter = 1;
        }


    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = 1;
            transform.position += Vector3.left * speed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 2;
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    void PlayerWallJump()
    {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isTouchingFront == true && isGrounded == false && moveInput !=0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Fog")
        {
            Destroy(gameObject);
        }
    }
}
