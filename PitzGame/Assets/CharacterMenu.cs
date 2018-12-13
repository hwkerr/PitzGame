using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class CharacterMenu : MonoBehaviour {

    [SerializeField] private int playerNum = 0;

    private CharacterPortrait[] characterPortraits;
    private int nextPortrait = 0;

    private GameObject doneButton;

    private string keyDown;

    private void Start()
    {
        characterPortraits = new CharacterPortrait[4];
        GameObject[] cps = GameObject.FindGameObjectsWithTag("CharacterPortrait");
        for (int i = 0; i < cps.Length; i++)
        {
            int num = cps[i].GetComponent<CharacterPortrait>().playerNum;
            characterPortraits[num] = cps[i].GetComponent<CharacterPortrait>();
        }
        
        doneButton = GameObject.Find("DoneButton");
        doneButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        doneButton.GetComponent<Button>().interactable = false;
    }

    void Update()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                keyDown = "KeyCode down: " + kcode;
        }

        if (nextPortrait >= 2)
        {
            doneButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            doneButton.GetComponent<Button>().interactable = true;
        }

        if (nextPortrait >= 2 && Input.GetKeyUp(KeyCode.JoystickButton9))
            PlayGame();

        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) //Set the controller port that is being used for each player
        {
            if (Input.GetKey(kcode) && Input.GetKey(kcode + 1))
            {
                if ((kcode >= KeyCode.Joystick1Button4 && kcode <= KeyCode.Joystick8Button4) && ((kcode - KeyCode.JoystickButton4) % 20 == 0)) //Check if kcode is a variant of JoystickButton4
                {
                    //Debug.Log("KeyCode down: " + kcode + ", " + (kcode + 1));
                    int port = (kcode - KeyCode.JoystickButton4) / 20; //Set the port accordingly
                    bool alreadyTaken = false;
                    foreach (CharacterPortrait cp in characterPortraits)
                    {
                        if (cp.GetPort() == port)
                            alreadyTaken = true;
                    }
                    if (!alreadyTaken)
                    {
                        if (nextPortrait < characterPortraits.Length)
                        {
                            Debug.Log("port " + port + " for player " + characterPortraits[nextPortrait].playerNum);
                            characterPortraits[nextPortrait].SetPort(port);
                            nextPortrait++;
                        }
                        else
                            Debug.Log("Too many controllers");
                    }
                }
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetMale()
    {
        GlobalValues.SetPlayer(playerNum, Character.Male);
    }

    public void SetFem()
    {
        GlobalValues.SetPlayer(playerNum, Character.Fem);
    }

    public void SetControlsJoystick()
    {
        GlobalValues.SetControls(playerNum, ControlScheme.Joystick);
    }

    public void SetControlsKeyboardLeft()
    {
        GlobalValues.SetControls(playerNum, ControlScheme.KeyboardLeft);
    }

    public void SetControlsKeyboardRight()
    {
        GlobalValues.SetControls(playerNum, ControlScheme.KeyboardRight);
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), keyDown);
    }
}
