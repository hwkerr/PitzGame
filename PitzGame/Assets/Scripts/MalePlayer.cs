using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalePlayer : DefaultPlayer {

    /** @Requires myHitboxes[0] is type CircleCollider2D
     *  @Requires myHitboxes[1] is type CapsuleCollider2D
     */
    
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
        jumpForce = 20f; //15f if double jump, 16f or 17f if only one jump
        groundedAttackRecovery = 20;
    }

    protected override void SetCollidersIdle(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(0.07f, -0.28f);
        head.radius = 0.20f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(0.08f, -0.60f);
        torso.size = new Vector2(0.15f, 0.30f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersCrouch(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(0.00f, -0.55f);
        head.radius = 0.20f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(0.16f, -0.68f);
        torso.size = new Vector2(0.30f, 0.15f);
        torso.direction = CapsuleDirection2D.Horizontal;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersWalk(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(0.01f, -0.28f);
        head.radius = 0.20f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(0.02f, -0.6f);
        torso.size = new Vector2(0.15f, 0.30f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersAir(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(0.05f, -0.28f);
        head.radius = 0.20f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(0.05f, -0.57f);
        torso.size = new Vector2(0.18f, 0.27f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersStab(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(-0.13f, -0.34f);
        head.radius = 0.20f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(0.00f, -0.63f);
        torso.size = new Vector2(0.26f, 0.24f);
        torso.direction = CapsuleDirection2D.Horizontal;
        hitboxColliders[1] = torso;
    }

    protected override void SetCollidersHitstun(Collider2D[] hitboxColliders)
    {
        CircleCollider2D head = hitboxColliders[0] as CircleCollider2D;
        head.offset = new Vector2(0.05f, -0.28f);
        head.radius = 0.20f;
        hitboxColliders[0] = head;

        CapsuleCollider2D torso = hitboxColliders[1] as CapsuleCollider2D;
        torso.offset = new Vector2(0.05f, -0.57f);
        torso.size = new Vector2(0.18f, 0.27f);
        torso.direction = CapsuleDirection2D.Vertical;
        hitboxColliders[1] = torso;
    }

    public override GameObject AttackBasic()
    {
        float dir = -1;
        if (controller.FacingRight())
            dir = 1;

        GameObject AttackPrefabClone = Instantiate(AttackPrefab);
        Collider2D attackBox = AttackPrefabClone.AddComponent<PolygonCollider2D>();
        attackBox.isTrigger = true;
        AttackPrefabClone.GetComponent<FollowObject>().Follow(transform, new Vector3(0.7f * dir, 0f));
        AttackPrefabClone.GetComponent<Damager>().IgnoreObject(gameObject);
        return AttackPrefabClone;
    }
}
