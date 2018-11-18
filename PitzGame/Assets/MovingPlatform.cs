using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    Rigidbody2D rb;
    Transform tf;
    bool movingRight;

	// Use this for initialization
	void Start () {

        Transform tf = GetComponent<Transform>();
        movingRight = true;
	}
	
	// Update is called once per frame
	void Update () {

        tf = GetComponent<Transform>();
		if(tf.position.x > 5)
        {
            tf.SetPositionAndRotation(new Vector3(tf.position.x + getMoveSpeed(), tf.position.y, 0f), tf.rotation);
            movingRight = false;
        }
        else if (GetComponent<Transform>().position.x < -5)
        {
            tf.SetPositionAndRotation(new Vector3(tf.position.x + getMoveSpeed(), tf.position.y, 0f), tf.rotation);
            movingRight = true;
        }
        else
        {
            tf.SetPositionAndRotation(new Vector3(tf.position.x + getMoveSpeed(), tf.position.y, 0f), tf.rotation);
        }
	}
    float getMoveSpeed()
    {
        if (movingRight)
        {
            return .05f;
        }
        else
        {
            return -.05f;
        }
    }

}
