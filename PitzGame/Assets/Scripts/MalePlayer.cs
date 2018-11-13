using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalePlayer : DefaultPlayer {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        BTTN_HORIZONTAL = "Horizontal_P2";
        BTTN_JUMP = "Jump_P2";
        BTTN_CROUCH = "Crouch_P2";
        BTTN_FIRE1 = "Fire1_P2";
        BTTN_INTERACT = "Interact_P2";
        BTTN_THROW = "Throw_P2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Init_StatValues()
    {
        runSpeed = 40f;
        crouchSpeed = 0f;
        jumpForce = 15f;
        groundedAttackRecovery = 20;
    }

    protected override Collider2D[] Init_StateIdle()
    {
        Collider2D[] myColliders = new Collider2D[2];

        //Head
        CircleCollider2D head = gameObject.AddComponent<CircleCollider2D>();
        head.offset = new Vector2(-0.01f, 0.10f);
        head.radius = 0.26f;
        head.enabled = false;
        myColliders[0] = head;

        //Torso
        CapsuleCollider2D torso = gameObject.AddComponent<CapsuleCollider2D>();
        torso.offset = new Vector2(-0.03f, -0.28f);
        torso.size = new Vector2(0.21f, 0.39f);
        torso.enabled = false;
        myColliders[1] = torso;

        return myColliders;
    }

    protected override Collider2D[] Init_StateCrouch()
    {
        Collider2D[] myColliders = new Collider2D[1];

        //Torso
        BoxCollider2D torso = gameObject.AddComponent<BoxCollider2D>();
        torso.offset = new Vector2(-0.01f, -0.27f);
        torso.size = new Vector2(0.63f, 0.41f);
        torso.enabled = false;
        myColliders[0] = torso;

        return myColliders;
    }

    protected override Collider2D[] Init_StateWalk()
    {
        Collider2D[] myColliders = new Collider2D[2];

        //Head
        CircleCollider2D head = gameObject.AddComponent<CircleCollider2D>();
        head.offset = new Vector2(-0.01f, 0.10f);
        head.radius = 0.26f;
        head.enabled = false;
        myColliders[0] = head;

        //Torso
        CapsuleCollider2D torso = gameObject.AddComponent<CapsuleCollider2D>();
        torso.offset = new Vector2(-0.03f, -0.28f);
        torso.size = new Vector2(0.21f, 0.39f);
        torso.enabled = false;
        myColliders[1] = torso;

        return myColliders;
    }

    protected override Collider2D[] Init_StateAir()
    {
        Collider2D[] myColliders = new Collider2D[2];

        //Head
        CircleCollider2D head = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        head.offset = new Vector2(-0.05f, 0.09f);
        head.radius = 0.26f;
        head.enabled = false;
        myColliders[0] = head;

        //Torso
        CapsuleCollider2D torso = gameObject.AddComponent(typeof(CapsuleCollider2D)) as CapsuleCollider2D;
        torso.offset = new Vector2(-0.06f, -0.27f);
        torso.size = new Vector2(0.23f, 0.39f);
        torso.enabled = false;
        myColliders[1] = torso;

        return myColliders;
    }

    // Values need to be fixed to the correct positions for the Stab animation
    protected override Collider2D[] Init_StateStab()
    {
        Collider2D[] myColliders = new Collider2D[2];

        //Head
        CircleCollider2D head = gameObject.AddComponent<CircleCollider2D>();
        head.offset = new Vector2(-0.01f, 0.10f);
        head.radius = 0.26f;
        head.enabled = false;
        myColliders[0] = head;

        //Torso
        CapsuleCollider2D torso = gameObject.AddComponent<CapsuleCollider2D>();
        torso.offset = new Vector2(-0.03f, -0.28f);
        torso.size = new Vector2(0.21f, 0.39f);
        torso.enabled = false;
        myColliders[1] = torso;

        return myColliders;
    }

    protected override Collider2D[] Init_StateHitstun()
    {
        Collider2D[] myColliders = new Collider2D[2];

        //Head
        CircleCollider2D head = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        head.offset = new Vector2(-0.05f, 0.09f);
        head.radius = 0.26f;
        head.enabled = false;
        myColliders[0] = head;

        //Torso
        CapsuleCollider2D torso = gameObject.AddComponent(typeof(CapsuleCollider2D)) as CapsuleCollider2D;
        torso.offset = new Vector2(-0.06f, -0.27f);
        torso.size = new Vector2(0.23f, 0.39f);
        torso.enabled = false;
        myColliders[1] = torso;

        return myColliders;
    }
}
