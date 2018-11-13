using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultPlayer : MonoBehaviour {

    private CharacterController2D controller;
    protected Rigidbody2D m_rigidbody2D;
    protected Grabber m_grabber;

    protected Dictionary<State, Collider2D[]> animColliders;

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

    public State currentState, lastState;

    public bool inHitstun = false;
    public Damager lastDamager;

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

        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_grabber = GetComponentInChildren<Grabber>();

        Init_AllColliders();
        Init_StatValues();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // @requires !inHitstun
    public void GetHit(Damager damager)
    {
        if (!damager.Equals(lastDamager))
        {
            lastDamager = damager;
        }
    }

    public void RecoverFromHit()
    {
        lastDamager = null;
    }

    // Tells m_grabber to pick up an item
    // Note: Should I get the caller of this function to just find the Grabber and call pickUpItem() from there?
    public void PickUpBall()
    {
        m_grabber.pickUpItem();
    }

    // Throws the ball
    public void ThrowBall()
    {
        m_grabber.releaseItem();
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
