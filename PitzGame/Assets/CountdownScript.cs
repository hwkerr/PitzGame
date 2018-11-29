using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownScript : MonoBehaviour {

    [SerializeField] private float minuteDuration;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    private bool running = false;
    
    // Use this for initialization
	void Start () {
        timer = minuteDuration * 60;
	}
	
	// Update is called once per frame
	void Update () {
        if (running)
        {
            if (timer >= 0.0f && canCount)
            {
                timer -= Time.deltaTime;
            }
            else if (timer <= 0.0f && !doOnce)
            {
                canCount = false;
                doOnce = true;
                timer = 0.0f;
            }
        }
	}

    public void ResetTimer()
    {
        timer = minuteDuration * 60;
        canCount = true;
        doOnce = false;
    }

    public void TogglePause()
    {
        running = !running;
    }

    public void TogglePause(bool pause)
    {
        running = !pause;
    }

    public float GetTime()
    {
        return timer;
    }

    public string GetFormattedTime()
    {
        int minutes = (int)(timer / 60f);
        string minuteString;
        if (minutes >= 10)
            minuteString = "" + minutes;
        else if (minutes < 10 && minutes >= 0)
            minuteString = "0" + minutes;
        else
            minuteString = "00";

        float seconds = timer % 60f;
        string secondString;
        if (seconds < 10)
            secondString = "0" + seconds.ToString("F");
        else
            secondString = "" + seconds.ToString("F");

        string output = minuteString + ":" + secondString;
        return output;
    }
}
