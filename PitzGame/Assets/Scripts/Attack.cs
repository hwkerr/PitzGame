using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Move {

    private Collider2D m_Collider2D;
    private Vector2 offset;
    public int startup, hitDuration, endlag;

    public enum State
    {
        Startup,
        Hit,
        Endlag
    }
    
    public Attack(Collider2D collider)
    {
        collider.isTrigger = true;
        m_Collider2D = collider;
        offset = Vector2.zero;

        startup = hitDuration = endlag = 5;
    }

    public Attack(Collider2D collider, Vector2 offset)
    {
        collider.isTrigger = true;
        m_Collider2D = collider;
        this.offset = offset;

        startup = hitDuration = endlag = 5;
    }

    public Collider2D GetCollider()
    {
        return m_Collider2D;
    }

    public Vector2 GetOffset()
    {
        return offset;
    }

    public Vector2 GetDirectedOffset(float dir)
    {
        return new Vector2(dir * offset.x, offset.y);
    }

    public int GetTotalDuration()
    {
        return startup + hitDuration + endlag;
    }

    public int GetFrame(State state)
    {
        if (state == State.Startup)
            return startup;
        else if (state == State.Hit)
            return startup + hitDuration;
        else //if (state == State.Endlag)
            return startup + hitDuration + endlag;
    }

    // @param decision = whether to enable or disable the collider
    public void EnableCollider(bool decision)
    {
        m_Collider2D.enabled = decision;
    }
}
