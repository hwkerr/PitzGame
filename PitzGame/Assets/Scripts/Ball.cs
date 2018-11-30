using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Grabbable {

    private CircleCollider2D m_collider;

    public void Score()
    {
        Poof();
        Invoke("ResetBall", 1);
    }

    public void TempFreeze()
    {
        ToggleFreeze(true);
        Invoke("ToggleFreeze", 1);
    }

    private void Poof()
    {
        m_Rigidbody2D.simulated = false;
        transform.localScale = new Vector3(2f, 2f, 1f);
        GetComponent<Animator>().SetBool("Poof", true);
    }

    // @Ensures this Ball stops all movement
    //          this Ball resets its position to the position (0, 5)
    public void ResetBall()
    {
        ResetBall(0, 5);
    }

    // @Ensures this Ball resets all momentum
    //          transform.position.x = x
    //          transform.position.y = y
    public void ResetBall(float x, float y)
    {
        GetComponent<Animator>().SetBool("Poof", false);
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        m_Rigidbody2D.angularVelocity = 0;
        m_Rigidbody2D.rotation = 0;
        transform.eulerAngles = Vector3.zero;
        transform.position = new Vector2(x, y);
        m_Rigidbody2D.velocity = Vector2.zero;
        EnableGrabberCollisions(); //Without this, the scorer won't be able to initially touch the ball (unless ball was scored with a rebound)

        TempFreeze();
    }
}
