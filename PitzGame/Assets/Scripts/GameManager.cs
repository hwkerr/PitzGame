﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    public int winningScore = 5;

    private GameObject[] thePlayers = new GameObject[4];
    [SerializeField] private GameObject[] characterPrefabs;

    private Ball theBall;
    private Goal goalLeft, goalRight;

    private CountdownScript timer;
    private bool startSequence = true;

    public Transform CameraTarget;

    public enum CharacterPrefab
    {
        MalePlayer,
        FemPlayer
    }

    // Use this for initialization
    void Start () {
        Time.timeScale = 1.0f;
        timer = GetComponent<CountdownScript>();
        theBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();

        timer.TogglePause(true);

        GameObject[] theGoalObjects = GameObject.FindGameObjectsWithTag("Goal");
        for (int i = 0; i < theGoalObjects.Length; i++)
        {
            if (theGoalObjects[i].GetComponent<Goal>().side == Goal.Side.Left)
                goalLeft = theGoalObjects[i].GetComponent<Goal>();
            else goalRight = theGoalObjects[i].GetComponent<Goal>();
        }

        if (GlobalValues.GetPlayer(0) == null && GlobalValues.GetPlayer(1) == null)
        {
            GlobalValues.SetPlayer(0, Character.Male);
            GlobalValues.SetPlayer(1, Character.Fem);
        }

        AddPlayer(GlobalValues.GetPlayer(0));
        AddPlayer(GlobalValues.GetPlayer(1));
        AddPlayer(GlobalValues.GetPlayer(2));
        AddPlayer(GlobalValues.GetPlayer(3));
    }

    private void Update()
    {
        //Log every key pressed
        /*foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }*/

        if (Input.GetKeyDown(KeyCode.JoystickButton9))
            PauseGame();

        if (startSequence)
        {
            theBall.ResetBall();
            startSequence = false;
            timer.ResetTimer();
            timer.TogglePause(false);
        }

        if (goalLeft != null && goalRight != null)
        {
            if (goalLeft.GetScore() >= winningScore)
                GameOver("Left Wins");
            if (goalRight.GetScore() >= winningScore)
                GameOver("Right Wins");
            if (timer.GetTime() <= 0)
            {
                if (goalLeft.GetScore() > goalRight.GetScore())
                    GameOver("Left Wins");
                else if (goalLeft.GetScore() < goalRight.GetScore())
                    GameOver("Right Wins");
                else
                    GameOver("Tie");
            }
        }
    }

    public void PauseGame()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            foreach (GameObject p in thePlayers)
            {
                if (p != null)
                {
                    p.GetComponent<DefaultPlayer>().Freeze(false);
                    p.GetComponent<PlayerMovement>().running = true;
                }
            }
        }
        else if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            foreach (GameObject p in thePlayers)
            {
                if (p != null)
                {
                    p.GetComponent<DefaultPlayer>().Freeze(true);
                    p.GetComponent<PlayerMovement>().running = false;
                }
            }
        }
    }

    // @Requires 0 <= player.playerNum <= 3
    // @Ensures Instantiates a Character player.character in the slot for player number int player.playerNum
    public void AddPlayer(Player player)
    {
        if (player != null)
        {
            int playerNum = player.playerNum;
            CharacterPrefab character;
            if (player.character == Character.Fem)
                character = CharacterPrefab.FemPlayer;
            else //if (player.character == Character.Male)
                character = CharacterPrefab.MalePlayer;

            if (thePlayers[playerNum] != null)
                Destroy(thePlayers[playerNum - 1]);
            thePlayers[playerNum] = Instantiate(characterPrefabs[(int)character]);
            thePlayers[playerNum].GetComponent<DefaultPlayer>().playerNum = playerNum;
            thePlayers[playerNum].GetComponent<DefaultPlayer>().m_ControlScheme = player.controlScheme;
            thePlayers[playerNum].GetComponent<DefaultPlayer>().port = player.port;
            float xval = 0; // pick spawn position based on playerNum
            if (playerNum == 0)
                xval = -6;
            else if (playerNum == 1)
                xval = 6;
            else if (playerNum == 2)
                xval = -2;
            else if (playerNum == 3)
                xval = 2;
            thePlayers[playerNum].GetComponent<Transform>().position = new Vector3(xval, -0.5f, 0f);
        }
    }

    private void GameOver(string message)
    {
        Debug.Log(message);
        Time.timeScale = 0.5f;
        timer.TogglePause(true);
        for (int i = 0; i < thePlayers.Length; i++)
        {
            if (thePlayers[i] != null)
            {
                thePlayers[i].GetComponent<AnimationController>().Freeze(true);
                thePlayers[i].GetComponent<PlayerMovement>().enabled = false;
            }
        }
        theBall.GameOver();
        Invoke("NextScene", 1);
    }

    private void NextScene()
    {
        Time.timeScale = 1.0f;
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    private void OnGUI()
    {
        if (true)
        {
            GUIStyle localStyle = new GUIStyle(GUI.skin.box);
            localStyle.font = Resources.Load<Font>("BetterPixels");
            localStyle.normal.textColor = Color.yellow;
            localStyle.alignment = TextAnchor.MiddleCenter;
            localStyle.fontSize = 32;
            GUI.Box(new Rect((Screen.width/2) - 50, 18, 100, 40), timer.GetFormattedTime(), localStyle);
        }
    }
}
