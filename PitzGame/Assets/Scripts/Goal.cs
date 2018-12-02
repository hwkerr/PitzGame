using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal: MonoBehaviour {

    public enum Side { Left, Right }
    [SerializeField] public Side side;
    [SerializeField] private int score;
    private BoxCollider2D m_goalHitbox;

	// Use this for initialization
	void Start () {
        m_goalHitbox = GetComponentInParent<BoxCollider2D>();
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
        GUIStyle localStyle = new GUIStyle(GUI.skin.box)
        {
            font = Resources.Load<Font>("BetterPixels")
        };
        localStyle.normal.textColor = side == Side.Left ? Color.red : Color.blue;
        localStyle.fontSize = 36;
        localStyle.alignment = side == Side.Left ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
        Vector3 currentLoc = Camera.main.WorldToScreenPoint(m_goalHitbox.transform.position);
        GUI.Label(new Rect((Screen.width / 2) - 50, Screen.height - currentLoc.y - 65, 100, 40), score.ToString(), localStyle);
    }
}
