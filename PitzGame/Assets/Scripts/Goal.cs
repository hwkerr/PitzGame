using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal: MonoBehaviour {

    private int score; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Pickup"))  
        {
            this.score++;
        }
    }

    public int getScore()
    {
        return this.score;
    }

}
