using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Grabbable {

    /* Remove most things in this class and put them in Grabbable
     * The only things in this class are things specific to the ball (like scoring in a goal)
     */

    private CircleCollider2D m_collider;
    
    // Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < -9.46)
        {
            resetBall();
        }
        if (transform.position.x > 9.44)
        {
            resetBall();
        }

        /*if (following)
        {
            m_transform.position = grabber.transform.position;
        }*/
    }
    
    //when character is grounded, the outer circle is touching the ground already
    // @Ensures once this ball collides with something other than the thrower, the thrower can be hit again
    void OnTriggerEnter2D(Collider2D collision)
    {
        /*Debug.Log("Ball Triggered");
        if (launching && collision.gameObject.layer != 13)
        {
            //player.gameObject.layer = 9;
            launching = false;
        }
        launching = false;*/
    }

    void resetBall()
    {
        m_Rigidbody2D.angularVelocity = 0;
        m_Rigidbody2D.rotation = 0;
        transform.position = new Vector2(0, 5);
        m_Rigidbody2D.velocity = Vector2.zero;
    }
}
