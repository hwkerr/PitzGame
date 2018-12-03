using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public DefaultPlayer player;
    
    protected SpriteRenderer spriteRenderer; // For different colliders on each frame of an animation
    private AnimationController anim;

    public float runSpeed;
    private float horizontalMove = 0f;

    protected bool busy = false,
        inHitstun = false,
        hitstunFirstLoopComplete = false,
        jump = false,
        crouch = false;
    protected float speed = 0;
    protected int minDuration;

    private bool debug = false, displayText;

    private float horizontalAxisRunPosition = 0.5f,
        verticalAxisCrouchPosition = 0.5f;

    public GameObject attack1;

    // Use this for initialization
    protected void Start () {
        Debug.Log("Press PageUp to enable Debug Mode");
        player = GetComponent<DefaultPlayer>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        
        debug = false;
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
        displayText = inHitstun;

        if (player.inHitstun && !hitstunFirstLoopComplete)
        {
            hitstunFirstLoopComplete = true;
            
            inHitstun = true;
            if (!crouch)
            {
                SetState(DefaultPlayer.State.Hitstun);
            }
        }

        if (!inHitstun && !busy)
        {
            horizontalMove = Input.GetAxisRaw(player.axisHorizontal);
            if (Mathf.Abs(horizontalMove) < horizontalAxisRunPosition)
                horizontalMove = 0;
            speed = Mathf.Abs(horizontalMove);
            if (Input.GetKeyDown(player.bttnJump1) || Input.GetKeyDown(player.bttnJump2))
            {
                jump = true;
                //SetState(STATE_AIR);
            }

            if (!player.m_Grounded) //Finds out when the player is aerial
            {
                SetState(DefaultPlayer.State.Air);
            }
            else //if (player.m_Grounded)
            {
                if (CrouchAxisValueReached())
                {
                    crouch = true;
                    SetState(DefaultPlayer.State.Crouch);
                }
                else if (!CrouchAxisValueReached())
                {
                    crouch = false;
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
                if (Input.GetKeyDown(player.bttnFire1))
                {
                    SetState(DefaultPlayer.State.Stab);
                    //if (attack1 != null)
                    //    Destroy(attack1);
                    //attack1 = player.SimpleAttack(0);
                    busy = true;
                    minDuration = player.GetStateDuration(DefaultPlayer.State.Stab);
                }
            }
            else if (player.GetState() == DefaultPlayer.State.Air)
            {
                if (Input.GetKeyDown(player.bttnFire1))
                {
                    SetState(DefaultPlayer.State.StabAir);
                    busy = true;
                    minDuration = player.GetStateDuration(DefaultPlayer.State.StabAir);
                }
            }

            if (Input.GetKeyDown(player.bttnInteract))
                player.PickUpItem();
            else if (Input.GetKeyDown(player.bttnThrow))
                player.ThrowItem(5, 5);
        }
        else if (inHitstun)
        {
            // If it has been enough time since player has been hit
            if (player.hitRecoveryCounter <= 0)
            {
                inHitstun = false;
                hitstunFirstLoopComplete = false;
                if (!CrouchAxisValueReached())
                    crouch = false;
                //FinishAttack();
            }
        }
        else if (busy)
        {
            //Debug.Log(minDuration);
            minDuration--;
            if (minDuration <= 0)
            {
                FinishAttack();
            }
        }
    }

    protected bool CrouchAxisValueReached()
    {
        return Input.GetAxisRaw(player.axisVertical) >= verticalAxisCrouchPosition;
    }

    protected void FinishAttack()
    {
        busy = false;
        Destroy(attack1);
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
        //Debug.Log("PlayerMovement.OnLanding");
        //Debug.Log("inHitstun = " + inHitstun);
        if (inHitstun)
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

    void OnGUI()
    {
        if (debug)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Debug Mode Enabled");
            GUI.Label(new Rect(10, 25, 200, 20), "Press PageDown to exit");
            GUI.Label(new Rect(10, 40, 400, 20), "Press a number key to switch to its corresponding state");

            GUI.Label(new Rect(10, 340, 300, 20), "Player 1: WASD+ZX, Player 2: IJKL+M,");
        }
        if (displayText)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "inHitstun");
        }
    }

    void FixedUpdate()
    {
        if (!busy && !inHitstun)
        {
            player.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }

    // @Requires newState < State.MaxState
    // @Ensures player.currentState == #newState && currentColliders = colliders[newState]
    protected void SetState(DefaultPlayer.State newState)
    {
        //Debug.Log("PlayerMovement: State = " + newState);
        if (newState < DefaultPlayer.State.MaxState)
        {
            player.SetState(newState);
        }
    }
}
