using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemPlayer : DefaultPlayer
{

    /** @Requires Head has component CircleCollider2D
     *  @Requires Torso has component CapsuleCollider2D
     */

    protected override void Init_StatValues()
    {
        m_RunSpeed = 50f;
        m_AirSpeed = 25f;
        m_CrouchSpeed = 0f;
        m_JumpForce = 16f; //15f if double jump, 16f or 17f if only one jump
        m_aerialJumps = 1;
        groundedHitRecovery = 20;
    }

    private void SetDefaultValues()
    {
        m_Head.GetComponent<CircleCollider2D>().radius = 0.18f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.14f, 0.30f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
        m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
        m_Sword.GetComponent<CapsuleCollider2D>().size = new Vector2(0.43f, 0.17f);
        

        m_Head.transform.localPosition = new Vector2(0.035f, -0.277f);
        m_Torso.transform.localPosition = new Vector2(0.04f, -0.60f);
        m_Torso.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        m_Sword.transform.localPosition = new Vector2(0.00f, -0.638f);
        m_Sword.transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    protected override void SetStateIdle(int keyframe)
    {
        SetDefaultValues();
    }

    protected override void SetStateCrouch(int keyframe)
    {
        SetDefaultValues();

        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.30f, 0.072f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;

        m_Head.transform.localPosition = new Vector2(-0.044f, -0.585f);
        m_Torso.transform.localPosition = new Vector2(0.18f, -0.735f);
    }

    protected override void SetStateWalk(int keyframe)
    {
        SetDefaultValues();

        m_Torso.transform.localPosition = new Vector2(0.05f, -0.60f);
    }

    protected override void SetStateAir(int keyframe)
    {
        SetDefaultValues();

        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.15f, 0.25f);

        m_Head.transform.localPosition = new Vector2(0.047f, -0.27f);
        m_Torso.transform.localPosition = new Vector2(0.06f, -0.56f);

    }

    protected override void SetStateStab(int keyframe)
    {
        SetDefaultValues();

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(0.046f, -0.31f);
            m_Torso.transform.localPosition = new Vector2(0.05f, -0.60f);
            m_Sword.transform.localPosition = new Vector2(0.00f, -0.638f);
        }
        else if (keyframe == 1)
        {
            m_Head.transform.localPosition = new Vector2(-0.145f, -0.335f);
            m_Torso.transform.localPosition = new Vector2(0.00f, -0.62f);
            m_Torso.transform.eulerAngles = new Vector3(0f, 0f, 36.5f);

            m_Sword.transform.localPosition = new Vector2(-0.5f, -0.57f);
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = true;
            sfx.PlaySwordAttack();
        }
        else if (keyframe == 2)
        {
            m_Head.transform.localPosition = new Vector2(0.046f, -0.31f);
            m_Torso.transform.localPosition = new Vector2(0.05f, -0.60f);
            m_Sword.transform.localPosition = new Vector2(0.00f, -0.638f);
        }
    }

    protected override void SetStateHitstun(int keyframe)
    {
        SetDefaultValues();

        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.15f, 0.25f);

        m_Head.transform.localPosition = new Vector2(0.047f, -0.27f);
        m_Torso.transform.localPosition = new Vector2(0.06f, -0.56f);
    }

    protected override void SetStateStabAir(int keyframe)
    {
        SetDefaultValues();

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(-0.01f, -0.327f);
            m_Torso.transform.localPosition = new Vector2(0.044f, -0.604f);
            m_Torso.transform.eulerAngles = new Vector3(0f, 0f, 21.2f);

            m_Sword.transform.localPosition = new Vector2(0.31f, -0.275f);
            m_Sword.transform.eulerAngles = new Vector3(0f, 0f, 70.4f);
        }
        else if (keyframe == 1)
        {
            m_Head.transform.localPosition = new Vector2(-0.09f, -0.324f);
            m_Torso.transform.localPosition = new Vector2(0.24f, -0.604f);
            m_Torso.transform.eulerAngles = new Vector3(0f, 0f, 21.2f);

            m_Sword.GetComponent<CapsuleCollider2D>().size = new Vector2(0.43f, 0.12f);
            m_Sword.transform.localPosition = new Vector2(-0.417f, -0.553f);
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = true;
            sfx.PlaySwordAttack();
        }
        else if (keyframe == 2)
        {
            m_Head.transform.localPosition = new Vector2(-0.01f, -0.334f);
            m_Torso.transform.localPosition = new Vector2(0.085f, -0.607f);
            m_Torso.transform.eulerAngles = new Vector3(0f, 0f, 21.2f);

            m_Sword.GetComponent<CapsuleCollider2D>().size = new Vector2(0.43f, 0.14f);
            m_Sword.transform.localPosition = new Vector2(0.251f, -0.593f);
        }
        else
        {
            m_Head.transform.localPosition = new Vector2(-0.01f, -0.327f);
            m_Torso.transform.localPosition = new Vector2(0.044f, -0.604f);
            m_Torso.transform.eulerAngles = new Vector3(0f, 0f, 21.2f);

            m_Sword.transform.localPosition = new Vector2(0.31f, -0.275f);
            m_Sword.transform.eulerAngles = new Vector3(0f, 0f, 70.4f);
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
