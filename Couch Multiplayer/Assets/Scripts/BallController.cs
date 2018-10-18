using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
    }
    void OnTriggerEnter2D(Collider2D other)
    {

    }
    void resetBall()
    {
        this.transform.position = new Vector2(0, 5);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
