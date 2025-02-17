using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] protected float m_JumpForce = 15f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] protected float m_CrouchSpeed = 0f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] protected float m_RunSpeed = 40f;                          // Speed to use for movement
    [SerializeField] protected float m_AirSpeed = 40f;                          // Speed to use for movement
    [SerializeField] protected int m_aerialJumps = 1;                           // Number of jumps available once in the air
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] protected bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] protected bool m_AerialTurning = true;                      // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    protected Rigidbody2D m_Rigidbody2D;
    [SerializeField] protected bool m_FacingRight = true;  // For determining which way the player is currently facing.
    protected Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    protected bool m_isCrouching = false;
    protected bool m_wasCrouching = false;
    protected int m_aerialJumpsRemaining;

    protected SFXController sfx;

    protected virtual void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        sfx = GetComponent<SFXController>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        if (m_Grounded)
            move *= m_RunSpeed;
        else
            move *= m_AirSpeed;
        m_isCrouching = crouch;
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            } else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If m_AerialTurning is false, character cannot turn around while in the air
            if (m_Grounded || m_AerialTurning)
            {
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
        }
        // If the player should jump...
        if (jump)
        {
            if (m_Grounded)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.velocity = new Vector2(0f, m_JumpForce);
                m_aerialJumpsRemaining = m_aerialJumps;
                sfx.PlayJump();
            }
            else if (!m_Grounded)
            {
                if (m_aerialJumpsRemaining > 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(0f, m_JumpForce * 0.9f);
                    m_aerialJumpsRemaining--;
                    sfx.PlayJump();
                }
            }
        }
    }

    public void SetVelocity(Vector2 velVector)
    {
        m_Rigidbody2D.velocity = velVector;
    }

    public void FaceRight()
    {
        if (!m_FacingRight)
            Flip();
    }

    public void FaceLeft()
    {
        if (m_FacingRight)
            Flip();
    }

    // temp function until I combine DefaultPlayer with CharacterController2D
    public bool FacingRight()
    {
        return m_FacingRight;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
