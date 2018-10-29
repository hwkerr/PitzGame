using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*public class ScoreManager : MonoBehaviour
{
    public static int score;


    Text text;

    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + score;
    }

}*/
public class ScoreManager : MonoBehaviour
{
    public static int score1;
    public static int score2;

    Text text1;
    Text text2;

    // Use this for initialization
    void Awake()
    {
        text1 = GameObject.FindGameObjectWithTag("Score1").GetComponent<Text>();
        text2 = GameObject.FindGameObjectWithTag("Score2").GetComponent<Text>();
        score1 = 0;
        score2 = 0;

    }

    // Update is called once per frame
    void Update()
    {
        text1.text = "" + score1;
        text2.text = "" + score2;
    }

}