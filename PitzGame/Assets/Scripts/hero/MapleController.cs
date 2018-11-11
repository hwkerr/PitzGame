using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapleController : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.


    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // Array of clips for when the player jumps.
    public float jumpForce = 1000f; 		// Delay for when the taunt should happen.
    public string jumpButton = "Jump_P1";
    public string horizontalCtrl = "Horizontal_P1";
    public string interactButton = "Interact_P1";
    public string throwButton = "Throw_P1";
    public bool holding = false;
    public bool canPickup = false;

    private Transform groundCheck;
    private bool grounded = false;          // Whether or not the player is grounded.
    private Animator anim;                  // Reference to the player's animator component.

    private void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

        if (groundCheck.position.y < -.9)
        {
            grounded = true;
        }
        else grounded = false;


        if (Input.GetButtonDown(jumpButton) && grounded)
        {
            jump = true;
        }
        if (Input.GetButtonDown(interactButton) && canPickup && !holding)
        {
            pickUpBall();
        }
        if (Input.GetButtonDown(throwButton) && holding)
        {
            throwBall();
        }
    }
    void FixedUpdate()
    {
        // Cache the horizontal input.
        float h = Input.GetAxis(horizontalCtrl);

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();



        // If the player should jump...
        if (jump)
        {
            // Set the Jump animator trigger parameter.
            anim.SetTrigger("Player_Jump");

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }
    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void pickUpBall()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        GameObject hand = GameObject.FindGameObjectWithTag("Hand");
        ball.transform.parent = hand.transform;
        ball.transform.position = hand.transform.position;
        ball.GetComponent<Rigidbody2D>().simulated = false;
        holding = true;

    }
    void throwBall()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        ball.transform.parent = null;
        ball.GetComponent<Rigidbody2D>().simulated = true;
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f, 200f));
        holding = false;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Ball"))
        {
            canPickup = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            canPickup = false;
        }
    }
}
