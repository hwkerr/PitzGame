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

        /*if (transform.position.x < -9.46)
        {
            ResetBall();
        }
        if (transform.position.x > 9.44)
        {
            ResetBall();
        }*/
    }

    void ResetBall()
    {
        m_Rigidbody2D.angularVelocity = 0;
        m_Rigidbody2D.rotation = 0;
        transform.position = new Vector2(0, 5);
        m_Rigidbody2D.velocity = Vector2.zero;
    }
}
