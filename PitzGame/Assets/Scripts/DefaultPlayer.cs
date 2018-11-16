using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultPlayer : MonoBehaviour {

    protected CharacterController2D controller;
    protected Rigidbody2D m_Rigidbody2D;
    protected Grabber m_grabber;

    protected Dictionary<State, Collider2D[]> animColliders;

    [HideInInspector]
    public string BTTN_HORIZONTAL,
        BTTN_JUMP,
        BTTN_CROUCH,
        BTTN_FIRE1,
        BTTN_INTERACT,
        BTTN_THROW;

    public float runSpeed,
        crouchSpeed,
        jumpForce;
    public int groundedAttackRecovery;

    public enum State
    {
        Idle,
        Crouch,
        Walk,
        Air,
        Stab,
        Hitstun,
        MaxState
    }

    public State currentState;
    [HideInInspector] public State lastState;

    [HideInInspector] public bool isCrouching = false, isJumping = false;

    public bool inHitstun = false;
    [HideInInspector] public int totalAttackRecovery, attackRecoveryCounter;
    [HideInInspector] public Damager lastDamager;

    // Use this for initialization
    protected virtual void Start () {
        BTTN_HORIZONTAL = "Horizontal";
        BTTN_JUMP = "Jump";
        BTTN_CROUCH = "Crouch";
        BTTN_FIRE1 = "Fire1";
        BTTN_INTERACT = "Interact";
        BTTN_THROW = "Throw";

        lastState = State.Idle;
        currentState = State.Idle;

        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_grabber = GetComponentInChildren<Grabber>();

        Init_AllColliders();
        Init_StatValues();
    }
	
	// Update is called once per frame
	void Update () {
        if (inHitstun)
        {
            //Debug.Log(attackRecoveryCounter);
            attackRecoveryCounter--;
            if (attackRecoveryCounter <= 0)
                RecoverFromHit();
        }
    }

    // Testing this function in DefaultPlayer instead of PlayerMovement
    public void OnTakeDamage(Damager damager, Vector2 knockbackVector, int duration)
    {
        if (!inHitstun && !damager.Equals(lastDamager))
        {
            lastDamager = damager;

            inHitstun = true;
            
            if (isCrouching)
            {
                attackRecoveryCounter = totalAttackRecovery = groundedAttackRecovery;
                m_Rigidbody2D.velocity = new Vector2(knockbackVector.x, 0f);
            }
            else
            {
                attackRecoveryCounter = totalAttackRecovery = duration;
                if (knockbackVector.x > 0)
                    controller.FaceLeft();
                else
                    controller.FaceRight();
                m_Rigidbody2D.velocity = knockbackVector;
            }
            isJumping = false;
        }
    }

    public void RecoverFromHit()
    {
        attackRecoveryCounter = 0;
        lastDamager = null;
        inHitstun = false;
    }

    // @requires inHitstun = true
    public void GroundedRecovery()
    {
        attackRecoveryCounter = groundedAttackRecovery;
    }

    // Tells m_grabber to pick up an item
    // Note: Should I get the caller of this function to just find the Grabber and call pickUpItem() from there?
    public void PickUpItem()
    {
        m_grabber.PickUpItem();
    }

    public void ReleaseItem()
    {
        m_grabber.ReleaseItem();
    }

    // @ensures the item is thrown with parameter input forces (force_x and force_y)
    //          the item is thrown in the direction that the player is facing
    //          the item retains player x momentum and positive y momentum
    public void ThrowItem(float force_x, float force_y)
    {
        float dir = -1;
        if (controller.FacingRight())
            dir = 1;
        force_x = force_x * dir + m_Rigidbody2D.velocity.x;
        if (m_Rigidbody2D.velocity.y > 0)
            force_y += m_Rigidbody2D.velocity.y / 2;
        m_grabber.ThrowItem(force_x, force_y);
    }
    
    // Sets the controller so that this class can control parameters like speed, jumpForce, etc.
    public void SetController(CharacterController2D acontroller)
    {
        controller = acontroller;
    }

    // @returns GetState = currentState
    public State GetState()
    {
        return currentState;
    }

    // @Requires newState < State.MaxState
    // @Ensures currentState = #newState
    public void SetState(State newState)
    {
        lastState = currentState;
        if (newState != currentState)
        {
            for (int i = 0; i < animColliders[currentState].Length; i++)
                animColliders[currentState][i].enabled = false;
            for (int i = 0; i < animColliders[newState].Length; i++)
                animColliders[newState][i].enabled = true;
        }
        currentState = newState;
    }

    protected virtual void Init_StatValues()
    {
        runSpeed = 40f;
        crouchSpeed = 0f;
        jumpForce = 15f;
        groundedAttackRecovery = 20;
    }

    // @Ensures all of the colliders for this GameObject are declared with set values
    public void Init_AllColliders()
    {
        animColliders = new Dictionary<State, Collider2D[]>();

        animColliders.Add(State.Idle, Init_StateIdle());
        animColliders.Add(State.Hitstun, Init_StateHitstun());
        animColliders.Add(State.Crouch, Init_StateCrouch());
        animColliders.Add(State.Walk, Init_StateWalk());
        animColliders.Add(State.Air, Init_StateAir());
        animColliders.Add(State.Stab, Init_StateStab());
    }

    // @Ensures Colliders for character state: idle are added to the gameObject
    // @returns Init_StateIdle = an array consisting of all colliders for the state: idle
    protected abstract Collider2D[] Init_StateIdle();

    // @Ensures Colliders for character state: hitstun are added to the gameObject
    // @returns Init_StateIdle = an array consisting of all colliders for the state: hitstun
    protected abstract Collider2D[] Init_StateHitstun();

    // @Ensures Colliders for character state: crouch are added to the gameObject
    // @returns Init_StateCrouch = an array consisting of all colliders for the state: crouch
    protected abstract Collider2D[] Init_StateCrouch();

    // @Ensures Colliders for character state: walk are added to the gameObject
    // @returns Init_StateWalk = an array consisting of all colliders for the state: walk
    protected abstract Collider2D[] Init_StateWalk();

    // @Ensures Colliders for character state: air are added to the gameObject
    // @returns Init_StateAir = an array consisting of all colliders for the state: air
    protected abstract Collider2D[] Init_StateAir();

    // @Ensures Colliders for character state: stab are added to the gameObject
    // @returns Init_StateStab = an array consisting of all colliders for the state: stab
    protected abstract Collider2D[] Init_StateStab();
}
