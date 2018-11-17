using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalePlayer : DefaultPlayer {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/

    protected override void Init_StatValues()
    {
        runSpeed = 40f;
        crouchSpeed = 0f;
        jumpForce = 15f;
        groundedAttackRecovery = 20;
    }

    protected override void SetCollidersIdle(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(0.01f, 0.10f);
        head.radius = 0.26f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(0.03f, -0.28f);
        torso.size = new Vector2(0.21f, 0.39f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    /*protected override void SetCollidersIdle(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = gameObject.AddComponent<CircleCollider2D>();
        head.offset = new Vector2(0.01f, 0.10f);
        head.radius = 0.26f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = gameObject.AddComponent<CapsuleCollider2D>();
        torso.offset = new Vector2(0.03f, -0.28f);
        torso.size = new Vector2(0.21f, 0.39f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }*/

    protected override void SetCollidersCrouch(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(-0.03f, -0.22f);
        head.radius = 0.26f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(0.15f, -0.38f);
        torso.size = new Vector2(0.39f, 0.21f);
        torso.direction = CapsuleDirection2D.Horizontal;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersWalk(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(-0.01f, 0.10f);
        head.radius = 0.26f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(-0.03f, -0.28f);
        torso.size = new Vector2(0.21f, 0.39f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersAir(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(-0.05f, 0.09f);
        head.radius = 0.26f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(-0.06f, -0.27f);
        torso.size = new Vector2(0.23f, 0.39f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersStab(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(-0.01f, 0.10f);
        head.radius = 0.26f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(-0.03f, -0.28f);
        torso.size = new Vector2(0.21f, 0.39f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersHitstun(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(-0.05f, 0.09f);
        head.radius = 0.26f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(-0.06f, -0.27f);
        torso.size = new Vector2(0.23f, 0.39f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    public override GameObject AttackBasic()
    {
        if (AttackPrefab.GetComponent<Collider2D>() == null)
        {
            Collider2D attackBox = AttackPrefab.AddComponent<PolygonCollider2D>();
            attackBox.isTrigger = true;
            Debug.Log("Another one");
        }
        AttackPrefab.GetComponent<FollowObject>().Follow(transform, new Vector3(0.7f, 0f));
        AttackPrefab.GetComponent<Damager>().IgnoreObject(gameObject);
        return Instantiate(AttackPrefab);
    }
}
