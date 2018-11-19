using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Grabbable {

    private CircleCollider2D m_collider;
    
    // Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
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
        m_Rigidbody2D.angularVelocity = 0;
        m_Rigidbody2D.rotation = 0;
        transform.position = new Vector2(x, y);
        m_Rigidbody2D.velocity = Vector2.zero;
        EnableGrabberCollisions(); //Without this, the scorer won't be able to initially touch the ball (unless ball was scored with a rebound)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the ball crosses the goal, then reset
        // scoring is handled in the goal class
        if (collision.gameObject.CompareTag("Goal"))
        {
            ResetBall();
        }
    }
}
