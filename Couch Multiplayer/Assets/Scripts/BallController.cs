using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public static void RocketHit()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(400f, 400f));
        ScoreManager.score1 += 1;

    }

	// Update is called once per frame
	void Update () {
        if (transform.position.x < -9.46)
        {
            resetBall(1);
        }
        if (transform.position.x > 9.44)
        {
            resetBall(2);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

    }
    void resetBall(int player)
    {
        this.transform.position = new Vector2(0, 5);

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(player == 1)
        {
            ScoreManager.score1 += 1;
        }
        else
        {
            ScoreManager.score2 += 1;
        }
    }
}
