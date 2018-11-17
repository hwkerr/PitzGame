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

    public int playerNum;

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

    [SerializeField] private Collider2D[] myHitboxes;

    [HideInInspector] public bool isCrouching = false, isJumping = false;

    [HideInInspector] public bool inHitstun = false;
    [HideInInspector] public int totalAttackRecovery, attackRecoveryCounter;
    [HideInInspector] public Damager lastDamager;

    // Use this for initialization
    protected virtual void Start () {
        lastState = State.Idle;
        currentState = State.Idle;

        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_grabber = GetComponentInChildren<Grabber>();

        //Init_AllColliders();
        Init_Buttons(playerNum);
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
            SetStateColliders(newState);
        }
        currentState = newState;
    }

    protected virtual void Init_Buttons(int playerNum)
    {
        BTTN_HORIZONTAL = "Horizontal_P" + playerNum;
        BTTN_JUMP = "Jump_P" + playerNum;
        BTTN_CROUCH = "Crouch_P" + playerNum;
        BTTN_FIRE1 = "Fire1_P" + playerNum;
        BTTN_INTERACT = "Interact_P" + playerNum;
        BTTN_THROW = "Throw_P" + playerNum;
    }

    protected virtual void Init_StatValues()
    {
        runSpeed = 40f;
        crouchSpeed = 0f;
        jumpForce = 15f;
        groundedAttackRecovery = 20;
    }

    /*protected void SetStateColliders(State state)
    {
        Collider2D[] myColliders = GetStateColliders(state);
        for (int i = 0; i < myHitboxes.Length; i++)
        {
            myHitboxes[i] = myColliders[i];
        }
    }*/

    // @Requires state < State.MaxState;
    // @returns GetStateColliders = an array consisting of all colliders for the specified state
    protected void SetStateColliders(State state)
    {
        if (state == State.Idle)
            SetCollidersIdle(myHitboxes);
        else if (state == State.Crouch)
            SetCollidersCrouch(myHitboxes);
        else if (state == State.Walk)
            SetCollidersWalk(myHitboxes);
        else if (state == State.Air)
            SetCollidersAir(myHitboxes);
        else if (state == State.Stab)
            SetCollidersStab(myHitboxes);
        else if (state == State.Hitstun)
            SetCollidersHitstun(myHitboxes);
    }

    // @Ensures Initializes collider values for character state: idle
    // @returns SetCollidersIdle = an array consisting of all colliders for the state: idle
    protected abstract void SetCollidersIdle(Collider2D[] hitboxColliders);

    // @Ensures Initializes collider values for character state: crouch
    // @returns SetCollidersCrouch = an array consisting of all colliders for the state: crouch
    protected abstract void SetCollidersCrouch(Collider2D[] hitboxColliders);

    // @Ensures Initializes collider values for character state: walk
    // @returns SetCollidersWalk = an array consisting of all colliders for the state: walk
    protected abstract void SetCollidersWalk(Collider2D[] hitboxColliders);

    // @Ensures Initializes collider values for character state: air
    // @returns SetCollidersAir = an array consisting of all colliders for the state: air
    protected abstract void SetCollidersAir(Collider2D[] hitboxColliders);

    // @Ensures Initializes collider values for character state: stab
    // @returns SetCollidersStab = an array consisting of all colliders for the state: stab
    protected abstract void SetCollidersStab(Collider2D[] hitboxColliders);

    // @Ensures Initializes collider values for character state: hitstun
    // @returns SetCollidersIdle = an array consisting of all colliders for the state: hitstun
    protected abstract void SetCollidersHitstun(Collider2D[] hitboxColliders);
}
