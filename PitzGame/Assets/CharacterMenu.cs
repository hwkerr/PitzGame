using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMenu : MonoBehaviour {

    [SerializeField]

    private int playerNum = 0;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.LeftAlt))
            GlobalValues.SetControls(playerNum, ControlScheme.KeyboardLeft);
        else if (Input.GetKeyDown(KeyCode.RightAlt))
            GlobalValues.SetControls(playerNum, ControlScheme.KeyboardRight);
        else
            GlobalValues.SetControls(playerNum, ControlScheme.Joystick);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            GlobalValues.SetPlayer(playerNum, Character.Male);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            GlobalValues.SetPlayer(playerNum, Character.Fem);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt) || Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }*/
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetMale()
    {
        GlobalValues.SetPlayer(playerNum, Character.Male);
        SetControlsJoystick();
        Debug.Log(playerNum + ": " + GlobalValues.GetPlayer(playerNum).character);
        //GlobalValues.SetControls(playerNum, ControlScheme.Joystick);
    }

    public void SetFem()
    {
        GlobalValues.SetPlayer(playerNum, Character.Fem);
        SetControlsKeyboardRight();
        Debug.Log(playerNum + ": " + GlobalValues.GetPlayer(playerNum).character);
        //GlobalValues.SetControls(playerNum, ControlScheme.KeyboardRight);
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
}
