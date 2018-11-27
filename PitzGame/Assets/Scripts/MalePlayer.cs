using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalePlayer : DefaultPlayer {

    /** @Requires myHitboxes[0] is type CircleCollider2D
     *  @Requires myHitboxes[1] is type CapsuleCollider2D
     */

    protected override void Init_StatValues()
    {
        m_RunSpeed = 40f;
        m_CrouchSpeed = 0f;
        m_JumpForce = 15f; //15f if double jump, 16f or 17f if only one jump
        m_aerialJumps = 1;
        groundedHitRecovery = 20;
    }

    protected override void SetStateIdle(int keyframe)
    {
        
        m_Head.GetComponent<CircleCollider2D>().radius = 0.20f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.15f, 0.30f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
        m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(0.07f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.08f, -0.60f);
        }
        else if (keyframe == 1)
        {
            m_Head.transform.localPosition = new Vector2(0.07f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.08f, -0.60f);
        }
        else if (keyframe == 2)
        {
            m_Head.transform.localPosition = new Vector2(0.07f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.08f, -0.60f);
        }
        else
        {
            m_Head.transform.localPosition = new Vector2(0.07f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.08f, -0.60f);
        }
        
    }

    protected override void SetStateCrouch(int keyframe)
    {
        
        m_Head.GetComponent<CircleCollider2D>().radius = 0.20f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.30f, 0.15f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
        m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(0.00f, -0.55f);
            m_Torso.transform.localPosition = new Vector2(0.16f, -0.68f);
        }
        else
        {
            m_Head.transform.localPosition = new Vector2(0.00f, -0.55f);
            m_Torso.transform.localPosition = new Vector2(0.16f, -0.68f);
        }
        
    }

    protected override void SetStateWalk(int keyframe)
    {
        
        m_Head.GetComponent<CircleCollider2D>().radius = 0.20f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.15f, 0.30f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
        m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(0.01f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.02f, -0.60f);
        }
        else if (keyframe == 1)
        {
            m_Head.transform.localPosition = new Vector2(0.01f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.02f, -0.60f);
        }
        else if (keyframe == 2)
        {
            m_Head.transform.localPosition = new Vector2(0.01f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.02f, -0.60f);
        }
        else if (keyframe == 3)
        {
            m_Head.transform.localPosition = new Vector2(0.01f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.02f, -0.60f);
        }
        else if (keyframe == 4)
        {
            m_Head.transform.localPosition = new Vector2(0.01f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.02f, -0.60f);
            
        }
    }

    protected override void SetStateAir(int keyframe)
    {
        
        m_Head.GetComponent<CircleCollider2D>().radius = 0.20f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.18f, 0.27f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
        m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(0.05f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.05f, -0.57f);
        }
        else
        {
            m_Head.transform.localPosition = new Vector2(0.05f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.05f, -0.57f);
        }
        
    }

    protected override void SetStateStab(int keyframe)
    {
        m_Head.GetComponent<CircleCollider2D>().radius = 0.20f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.26f, 0.24f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
        
        m_Sword.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 0.1f);

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(-0.13f, -0.34f);
            m_Torso.transform.localPosition = new Vector2(0.00f, -0.63f);
            m_Sword.transform.localPosition = new Vector2(-0.5f, -0.53f);
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
            
        }
        else if (keyframe == 1)
        {
            m_Head.transform.localPosition = new Vector2(-0.13f, -0.34f);
            m_Torso.transform.localPosition = new Vector2(0.00f, -0.63f);
            m_Sword.GetComponent<Transform>().localPosition = new Vector2(-0.5f, -0.53f);
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = true;
            sfx.PlaySwordAttack();
        }
        else if (keyframe == 2)
        {
            m_Head.transform.localPosition = new Vector2(-0.13f, -0.34f);
            m_Torso.transform.localPosition = new Vector2(0.00f, -0.63f);
            m_Sword.transform.localPosition = new Vector2(-0.5f, -0.53f);
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    protected override void SetStateHitstun(int keyframe)
    {
        
        m_Head.GetComponent<CircleCollider2D>().radius = 0.20f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.18f, 0.27f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
        m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(0.05f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.05f, -0.57f);
        }
        else
        {
            m_Head.transform.localPosition = new Vector2(0.05f, -0.28f);
            m_Torso.transform.localPosition = new Vector2(0.05f, -0.57f);
        }
    }

    protected override void SetStateStabAir(int keyframe)
    {
        m_Head.GetComponent<CircleCollider2D>().radius = 0.20f;
        m_Torso.GetComponent<CapsuleCollider2D>().size = new Vector2(0.26f, 0.24f);
        m_Torso.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;

        m_Sword.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 0.1f);

        if (keyframe == 0)
        {
            m_Head.transform.localPosition = new Vector2(-0.13f, -0.34f);
            m_Torso.transform.localPosition = new Vector2(0.00f, -0.63f);
            m_Sword.transform.localPosition = new Vector2(-0.5f, -0.53f);
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else if (keyframe == 1)
        {
            m_Head.transform.localPosition = new Vector2(-0.13f, -0.34f);
            m_Torso.transform.localPosition = new Vector2(0.00f, -0.63f);
            m_Sword.GetComponent<Transform>().localPosition = new Vector2(-0.5f, -0.53f);
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = true;
            sfx.PlaySwordAttack();
        }
        else if (keyframe == 2)
        {
            m_Head.transform.localPosition = new Vector2(-0.13f, -0.34f);
            m_Torso.transform.localPosition = new Vector2(0.00f, -0.63f);
            m_Sword.transform.localPosition = new Vector2(-0.5f, -0.53f);
            m_Sword.GetComponent<CapsuleCollider2D>().enabled = false;
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
