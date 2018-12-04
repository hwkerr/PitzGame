using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalePlayer : DefaultPlayer {

    /** @Requires Head has component CircleCollider2D
     *  @Requires Torso has component CapsuleCollider2D
     */

    protected override void Init_StatValues()
    {
        m_RunSpeed = 40f;
        m_CrouchSpeed = 0f;
        m_JumpForce = 15f;
        m_aerialJumps = 1;
        groundedHitRecovery = 20;
    }

    private void SetDefaultValues()
    {
        m_Head.GetComponent<CircleCollider2D>().radius = 0.20f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.15f, 0.30f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
        m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
        m_Sword.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 0.1f);

        m_Head.transform.localPosition = new Vector2(0.07f, -0.28f);
        m_Torso.transform.localPosition = new Vector2(0.08f, -0.60f);
        m_Torso.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        m_Sword.transform.localPosition = new Vector2(-0.5f, -0.53f);
        m_Sword.transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    protected override void SetStateIdle(int keyframe)
    {
        SetDefaultValues();
    }

    protected override void SetStateCrouch(int keyframe)
    {
        SetDefaultValues();

        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.30f, 0.15f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;

        m_Head.transform.localPosition = new Vector2(0.00f, -0.55f);
        m_Torso.transform.localPosition = new Vector2(0.16f, -0.68f);
        
    }

    protected override void SetStateWalk(int keyframe)
    {
        SetDefaultValues();

        m_Head.transform.localPosition = new Vector2(0.01f, -0.28f);
        m_Torso.transform.localPosition = new Vector2(0.02f, -0.60f);
    }

    protected override void SetStateAir(int keyframe)
    {
        SetDefaultValues();

        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.18f, 0.27f);

        m_Head.transform.localPosition = new Vector2(0.05f, -0.28f);
        m_Torso.transform.localPosition = new Vector2(0.05f, -0.57f);
    }

    protected override void SetStateStab(int keyframe)
    {
        SetDefaultValues();

        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.26f, 0.24f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;

        m_Head.transform.localPosition = new Vector2(-0.13f, -0.34f);
        m_Torso.transform.localPosition = new Vector2(0.00f, -0.63f);
        m_Sword.transform.localPosition = new Vector2(-0.5f, -0.53f);

        if (keyframe == 0)
        {
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else if (keyframe == 1)
        {
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = true;
            sfx.PlaySwordAttack();
        }
        else if (keyframe == 2)
        {
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    protected override void SetStateHitstun(int keyframe)
    {
        SetDefaultValues();

        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.18f, 0.27f);

        m_Head.transform.localPosition = new Vector2(0.05f, -0.28f);
        m_Torso.transform.localPosition = new Vector2(0.05f, -0.57f);
    }

    protected override void SetStateStabAir(int keyframe)
    {
        SetDefaultValues();

        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.26f, 0.24f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;

        m_Head.transform.localPosition = new Vector2(-0.13f, -0.34f);
        m_Torso.transform.localPosition = new Vector2(0.00f, -0.63f);
        m_Sword.transform.localPosition = new Vector2(-0.5f, -0.53f);

        if (keyframe == 0)
        {
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else if (keyframe == 1)
        {
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = true;
            sfx.PlaySwordAttack();
        }
        else if (keyframe == 2)
        {
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    protected override void SetStateStabProne(int keyframe)
    {
        SetStateCrouch(keyframe);

        m_Sword.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 0.2f);
        m_Sword.transform.localPosition = new Vector2(-0.32f, -0.72f);

        if (keyframe == 1)
        {
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    protected override Attack GetAttackStabO1(GameObject AttackObject)
    {
        CapsuleCollider2D collider = AttackObject.AddComponent<CapsuleCollider2D>();
        Vector2 offset = new Vector2(0.96f, -1.04f);
        collider.size = new Vector2(1.16f, 0.28f);
        collider.direction = CapsuleDirection2D.Horizontal;

        return new Attack(collider, offset);
    }
}
