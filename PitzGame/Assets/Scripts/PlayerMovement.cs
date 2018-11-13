using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;
    public DefaultPlayer player;
    
    protected SpriteRenderer spriteRenderer; // For different colliders on each frame of an animation

    public float runSpeed;
    private float horizontalMove = 0f;

    protected bool busy = false,
        inHitstun = false,
        jump = false,
        crouch = false;
    protected float speed = 0;
    protected int busyCounter,
        duration;

    private bool debug;

    // Use this for initialization
    protected void Start () {
        Debug.Log("Press PageUp to enable Debug Mode");
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        player.Init_AllColliders();
        runSpeed = player.runSpeed;
        debug = false;

        player.SetController(controller);

        //Debug.Log("Future Task: Make object retain momentum from Damager even after hitstun");
        //Debug.Log("Future Task: Combine DefaultPlayer and CharacterController2D classes");
        Debug.Log("Current Task: ?");
        Debug.Log("Current Issue: ?");
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            debug = true;
            Debug.Log("Debug Mode Enabled");
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
            debug = false;
            Debug.Log("Debug Mode Disabled");
        }

        if (debug)
            TestUpdate();
        else
            NormalUpdate();
    }

    // This is the sequence normally executed during the Update function
    protected void NormalUpdate () {

        if (player.justHit)
        {
            player.justHit = false;
            busy = true;
            if (!crouch)
            {
                SetState(DefaultPlayer.State.Hitstun);
            }
        }

        if (!busy)
        {
            horizontalMove = Input.GetAxisRaw(player.BTTN_HORIZONTAL) * runSpeed;
            speed = Mathf.Abs(horizontalMove);

            animator.SetFloat("Speed", speed);

            DefaultPlayer.State myState = player.GetState();

            if (Input.GetButtonDown(player.BTTN_JUMP))
            {
                jump = true;
                //SetState(STATE_AIR);
            }

            if (!controller.m_Grounded) //Finds out when the player is aerial
            {
                SetState(DefaultPlayer.State.Air);
            }
            else //if (controller.m_Grounded)
            {
                if (Input.GetButtonDown(player.BTTN_CROUCH))
                {
                    player.isCrouching = crouch = true;
                    SetState(DefaultPlayer.State.Crouch);
                }
                else if (Input.GetButtonUp(player.BTTN_CROUCH))
                {
                    player.isCrouching = crouch = false;
                }

                if (!crouch)
                {
                    if (speed > 0.01)
                        SetState(DefaultPlayer.State.Walk);
                    else
                        SetState(DefaultPlayer.State.Idle);
                }
            }

            if (player.GetState() == DefaultPlayer.State.Idle)
            {
                if (Input.GetButtonDown(player.BTTN_FIRE1))
                {
                    SetState(DefaultPlayer.State.Stab);
                    busy = true;
                    busyCounter = 0;
                    duration = 30;
                }
            }

            if (Input.GetButtonDown(player.BTTN_INTERACT))
                player.PickUpBall();
            else if (Input.GetButtonDown(player.BTTN_THROW))
                player.ThrowBall();
        }
        else // (busy)
        {
            if (player.attackRecoveryCounter <= 0)
            {
                busy = false;
                if (!Input.GetButton(player.BTTN_CROUCH))
                    player.isCrouching = crouch = false;
            }
        }
    }

    protected void TestUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            SetState((DefaultPlayer.State) 0);
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            SetState((DefaultPlayer.State) 1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SetState((DefaultPlayer.State) 2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SetState((DefaultPlayer.State) 3);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            SetState((DefaultPlayer.State) 4);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            SetState((DefaultPlayer.State) 5);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            SetState((DefaultPlayer.State) 6);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            SetState((DefaultPlayer.State) 7);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            SetState((DefaultPlayer.State) 8);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            SetState((DefaultPlayer.State) 9);
    }

    public void OnLanding()
    {
        if (busy)
        {
            player.GroundedRecovery();
            SetState(DefaultPlayer.State.Idle);
        }
        else // (!busy)
        {
            if (speed > 0.01)
                SetState(DefaultPlayer.State.Walk);
            else
                SetState(DefaultPlayer.State.Idle);
        }
    }

    /*public void OnGetHit(Damager damager, Vector2 knockbackVector, int framelength)
    {
        Debug.Log("Hit");
        if (!inHitstun)
        {
            if (crouch)
            {
                inHitstun = true;
                busyCounter = player.groundedAttackRecovery;
                controller.SetVelocity(new Vector2(knockbackVector.x, 0f));
            }
            else
            {
                inHitstun = true;
                busyCounter = framelength;
                SetState(DefaultPlayer.State.Hitstun);
                if (knockbackVector.x > 0)
                    controller.FaceLeft();
                else
                    controller.FaceRight();
                controller.SetVelocity(knockbackVector);
            }
            player.GetHit(damager);
            jump = false;
            //player.isCrouching = crouch = false;
        }
    }*/

    void OnGUI()
    {
        if (debug)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Debug Mode Enabled");
            GUI.Label(new Rect(10, 25, 200, 20), "Press PageDown to exit");
            GUI.Label(new Rect(10, 40, 400, 20), "Press a number key to switch to its corresponding state");

            GUI.Label(new Rect(10, 340, 300, 20), "Player 1: WASD+ZX, Player 2: IJKL+M,");
        }
    }

    void FixedUpdate()
    {
        if (!busy)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }

    // @Requires newState < State.MaxState
    // @Ensures player.currentState == #newState && animator.State == #newstate && currentColliders = colliders[newState]
    protected void SetState(DefaultPlayer.State newState)
    {
        //Debug.Log("PlayerMovement: State = " + newState);
        if (newState < DefaultPlayer.State.MaxState)
        {
            animator.SetInteger("State", (int)newState);
            player.SetState(newState);
        }
    }
}
