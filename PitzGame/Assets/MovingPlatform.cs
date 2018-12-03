using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    Rigidbody2D rb;
    Transform tf;
    bool movingRight;
    Rigidbody2D rigidb;
    float moveSpeed = 1f; 
	// Use this for initialization
	void Start () {

        Transform tf = GetComponent<Transform>();
        rigidb = GetComponent<Rigidbody2D>();
        movingRight = true;
	}

    // Update is called once per frame
    /*void Update () {

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
	}*/
    private void Update()
    {
        tf = GetComponent<Transform>();
        rigidb = GetComponent<Rigidbody2D>();
        if (tf.position.x > 5)
        {
            movingRight = false;
            rigidb.velocity = new Vector2(getMoveSpeed(), 0f);
        }
        else if (GetComponent<Transform>().position.x < -5)
        {
            movingRight = true;
            rigidb.velocity = new Vector2(getMoveSpeed(), 0f);
        }
        else
        {
            rigidb.velocity = new Vector2(getMoveSpeed(), 0f);
        }
    }

    float getMoveSpeed()
    {
        if (movingRight)
        {
            return moveSpeed;
        }
        else
        {
            return -1 * moveSpeed;
        }
    }

}
