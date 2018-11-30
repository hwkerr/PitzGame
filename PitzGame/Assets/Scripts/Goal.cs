using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal: MonoBehaviour {

    public enum Side { Left, Right }
    [SerializeField] public Side side;
    [SerializeField] private int score;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // if the ball goes in the goal, add one to score and reset ball
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball incomingBall = collision.gameObject.GetComponent<Ball>();
        if (incomingBall != null)
        {
            incomingBall.Score();
            this.score++;
        }
    }

    public int GetScore()
    {
        return this.score;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(250 + 21 * transform.position.x, 140 + -25 * transform.position.y, 100, 20), score + " points");
    }
}
