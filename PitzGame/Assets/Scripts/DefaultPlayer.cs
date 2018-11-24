using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultPlayer : MonoBehaviour {

    public GameObject AttackPrefab;

    [SerializeField] protected GameObject m_Head;
    [SerializeField] protected GameObject m_Torso;
    [SerializeField] protected GameObject m_Sword;

    protected AnimationController anim;
    protected CharacterController2D controller;
    protected Rigidbody2D m_Rigidbody2D;
    protected Grabber m_grabber;

    [HideInInspector]
    public string BTTN_HORIZONTAL,
        BTTN_JUMP,
        BTTN_CROUCH,
        BTTN_FIRE1,
        BTTN_INTERACT,
        BTTN_THROW;

    public int playerNum;

    // Character-Specific Stats
    public float runSpeed,
        crouchSpeed,
        jumpForce;
    public int groundedHitRecovery;


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

    [Range(0, 100)] public float health = 100;

    [HideInInspector] public bool isCrouching = false, isJumping = false;

    protected int animKeyframe, animCounter;

    public Attack.State attackState;
    public bool attacking = false;
    public Attack currentAttack;

    [HideInInspector] public bool inHitstun = false;
    [HideInInspector] public int totalHitRecovery, hitRecoveryCounter;
    [HideInInspector] public Damager lastDamager;

    // Use this for initialization
    protected virtual void Start () {
        lastState = State.Idle;
        currentState = State.Idle;

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 14 - playerNum;

        anim = gameObject.GetComponent<AnimationController>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_grabber = GetComponentInChildren<Grabber>();

        m_Sword.GetComponent<Damager>().IgnoreObject(gameObject);

        //Init_AllColliders();
        Init_Buttons(playerNum);
        Init_StatValues();
    }
	
	// Update is called once per frame
	void Update () {

        if (animCounter == anim.GetCurrentDuration())
        {
            State_AdvanceSprite();
            animCounter = 0;
        }
        animCounter++;


        if (inHitstun)
        {
            //Debug.Log(hitRecoveryCounter);
            hitRecoveryCounter--;
            if (hitRecoveryCounter <= 0)
                RecoverFromHit();
        }

        /*if (attacking && currentAttack != null)
        {
            
            //attackState++;
            anim.Advance();
            if (animCounter == currentAttack.GetFrame(Attack.State.Hit))//attackState == Attack.State.Hit)
            {
                currentAttack.EnableCollider(true);
                anim.Advance();
            }
            else if (animCounter == currentAttack.GetFrame(Attack.State.Hit))//attackState == Attack.State.Endlag)
            {
                currentAttack.EnableCollider(false);
                anim.Advance();
            }
            else if (animCounter == currentAttack.GetFrame(Attack.State.Hit))//if (attackState == Attack.State.Startup)
            {
                attacking = false;
                currentAttack = null;
            }
        }*/
    }

    // @returns An integer value corresponding to the player's current health
    // @Ensures GetHealth >= 0
    public int GetHealth()
    {
        if (health > 0)
            return Mathf.RoundToInt(health);
        else return 0;
    }

    public void OnTakeDamage(Damager damager, Vector2 knockbackVector, float damage, int duration)
    {
        if (!(inHitstun && damager.Equals(lastDamager)))
        {
            ReleaseItem();
            lastDamager = damager;
            inHitstun = true;

            if (health == 0) DeathSequence();
            health -= damage;
            if (health < 0) health = 0;

            if (isCrouching)
            {
                hitRecoveryCounter = totalHitRecovery = groundedHitRecovery;
                m_Rigidbody2D.velocity = new Vector2(knockbackVector.x, 0f);
            }
            else
            {
                hitRecoveryCounter = totalHitRecovery = duration;
                if (knockbackVector.x > 0)
                    controller.FaceLeft();
                else
                    controller.FaceRight();
                m_Rigidbody2D.velocity = knockbackVector;
            }
            isJumping = false;
        }
    }

    // @requires inHitstun = true
    public void GroundedRecovery()
    {
        Debug.Log("DPlayer.GroundedRecovery");
        hitRecoveryCounter = groundedHitRecovery;
    }

    public void RecoverFromHit()
    {
        hitRecoveryCounter = 0;
        lastDamager = null;
        inHitstun = false;
    }

    public void DeathSequence()
    {
        m_Rigidbody2D.simulated = false;
        ReleaseItem();
        GetComponent<Animator>().SetBool("Death", true);
        Invoke("Perish", 0.6f);
    }

    private void Perish()
    {
        Destroy(gameObject);
    }

    public void ResetPlayer()
    {
        health = 100;
    }

    // Tells m_grabber to pick up an item
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
            SetSpecificState(newState);
            animCounter = 0;
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
        groundedHitRecovery = 20;
    }

    // @Ensures this DefaultPlayer has a new Damager object
    public GameObject SpecialAttack(int attackNum)
    {
        float dir = -1;
        if (controller.FacingRight())
            dir = 1;

        GameObject AttackPrefabClone = Instantiate(AttackPrefab);
        Attack theAttack = GetAttackCollider(AttackPrefabClone, attackNum);
        AttackPrefabClone.GetComponent<Damager>().IgnoreObject(gameObject);
        Collider2D collider = theAttack.GetCollider();
        collider.enabled = false;
        AttackPrefabClone.GetComponent<FollowObject>().Follow(transform, theAttack.GetDirectedOffset(dir));

        currentAttack = theAttack;
        attacking = true;
        attackState = Attack.State.Startup;

        return AttackPrefabClone;
    }

    protected Attack GetAttackCollider(GameObject AttackObject, int attackNum)
    {
        return GetAttackStabO1(AttackObject);
    }

    protected abstract Attack GetAttackStabO1(GameObject AttackObject);

    // @Requires state < State.MaxState;
    // @Ensures The Head, Torso, and Sword objects are modified for the specified state
    protected void SetSpecificState(State state)
    {
        animKeyframe = 0;
        anim.Set((int)state);
        int frame = 0;
        if (state == State.Idle)
            SetStateIdle(frame);
        else if (state == State.Crouch)
            SetStateCrouch(frame);
        else if (state == State.Walk)
            SetStateWalk(frame);
        else if (state == State.Air)
            SetStateAir(frame);
        else if (state == State.Stab)
            SetStateStab(frame);
        else if (state == State.Hitstun)
            SetStateHitstun(frame);
    }

    protected int State_AdvanceSprite()
    {
        anim.Advance();
        int frame = anim.GetKeyframe();
        if (currentState == State.Idle)
            SetStateIdle(frame);
        else if (currentState == State.Crouch)
            SetStateCrouch(frame);
        else if (currentState == State.Walk)
            SetStateWalk(frame);
        else if (currentState == State.Air)
            SetStateAir(frame);
        else if (currentState == State.Stab)
            SetStateStab(frame);
        else if (currentState == State.Hitstun)
            SetStateHitstun(frame);
        return anim.GetCurrentDuration();
    }

    public int GetStateDuration(State state)
    {
        return anim.GetStateDuration((int)state);
    }

    // @Ensures Initializes collider values for character state: idle
    // @returns SetStateIdle = duration of the specified keyframe
    protected abstract void SetStateIdle(int keyframe);

    // @Ensures Initializes collider values for character state: crouch
    // @returns SetStateCrouch = duration of the specified keyframe
    protected abstract void SetStateCrouch(int keyframe);

    // @Ensures Initializes collider values for character state: walk
    // @returns SetStateWalk = duration of the specified keyframe
    protected abstract void SetStateWalk(int keyframe);

    // @Ensures Initializes collider values for character state: air
    // @returns SetStateAir = duration of the specified keyframe
    protected abstract void SetStateAir(int keyframe);

    // @Ensures Initializes collider values for character state: stab
    // @returns SetStateStab = duration of the specified keyframe
    protected abstract void SetStateStab(int keyframe);

    // @Ensures Initializes collider values for character state: hitstun
    // @returns SetStateIdle = duration of the specified keyframe
    protected abstract void SetStateHitstun(int keyframe);
}
