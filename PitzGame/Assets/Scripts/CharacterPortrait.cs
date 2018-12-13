using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterPortrait : MonoBehaviour {

    [Range(0, 3)] public int playerNum;

    private int port = 0; // Range(1, 8)
    private ControlScheme controller;

    private Image image;
    private GameObject selector;
    private Animator anim;

    private GameObject[] characters;

    private bool selecting = false;
    private int selectorPosition = 1;
    private int selectorDelay = 15;
    private int counter = 0;

    private MenuSFX sfx;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
        sfx = GameObject.Find("MenuSFX").GetComponent<MenuSFX>();
        foreach (Transform child in transform)
        {
            if (child.tag == "CharacterSelector")
            {
                selector = child.gameObject;
            }
        }
        if (selector == null)
            Debug.Log("Each portrait should have a child object with tag CharacterSelector");
        selector.GetComponent<Image>().enabled = false;

        characters = GameObject.FindGameObjectsWithTag("SelectableCharacter");

        anim.SetInteger("State", 0);
        image.color = new Color(255f, 255f, 255f);
	}
	
	// Update is called once per frame
	void Update () {
        if (selecting)
        {
            //string verticalAxis = "Vertical_P" + port;
            string horizontalAxis = "Horizontal";
            if (port > 0)
                horizontalAxis = "Horizontal_P" + port;
            else if (controller == ControlScheme.KeyboardLeft)
                horizontalAxis = "HorizontalKeyboardLeft";
            else if (controller == ControlScheme.KeyboardRight)
                horizontalAxis = "HorizontalKeyboardRight";

            if (!IsLocked())
            {
                if (Input.GetKeyDown(KeyCode.JoystickButton13 + (20 * port)) || Input.GetAxisRaw(horizontalAxis) > 0.5)
                    IncrementSelector(1);
                else if (Input.GetKeyDown(KeyCode.JoystickButton15 + (20 * port)) || Input.GetAxisRaw(horizontalAxis) < -0.5)
                    IncrementSelector(-1);
            }

            if (controller == ControlScheme.Joystick)
            {
                if (Input.GetKeyDown(KeyCode.JoystickButton1 + (20 * port)))
                    LockSelection(true);
                else if (Input.GetKeyDown(KeyCode.JoystickButton2 + (20 * port)))
                    LockSelection(false);
            }
            else if (controller == ControlScheme.KeyboardLeft)
            {
                if (Input.GetKeyDown(KeyCode.X))
                    LockSelection(true);
                else if (Input.GetKeyDown(KeyCode.Z))
                    LockSelection(false);
            }
            else if (controller == ControlScheme.KeyboardRight)
            {
                if (Input.GetKeyDown(KeyCode.M))
                    LockSelection(true);
                else if (Input.GetKeyDown(KeyCode.Comma))
                    LockSelection(false);
            }

            counter--;
        }
    }

    private void IncrementSelector(int direction)
    {
        if (counter <= 0)
        {
            selectorPosition += direction;
            if (selectorPosition >= characters.Length)
                selectorPosition = 0;
            if (selectorPosition < 0)
                selectorPosition = characters.Length - 1;
            
            if (characters[selectorPosition].name.Equals("Male"))
                selector.transform.localPosition = new Vector2(10f, -55f);
            else if (characters[selectorPosition].name.Equals("Female"))
                selector.transform.localPosition = new Vector2(0f, -55f);
            selector.transform.SetParent(characters[selectorPosition].transform, false);

            counter = selectorDelay;
        }
    }

    public bool IsLocked()
    {
        return selector.GetComponent<Animator>().GetBool("Locked");
    }

    public void LockSelection(bool locked)
    {
        if (locked)
            sfx.PlaySelection();
        else
            sfx.PlayDeselection();
        selector.GetComponent<Animator>().SetBool("Locked", locked);
    }

    public Character GetCharacter()
    {
        if (selectorPosition == 1)
            return Character.Male;
        else if (selectorPosition == 0)
            return Character.Fem;
        else
            return 0;
    }

    public void SetController(ControlScheme controlScheme)
    {
        controller = controlScheme;
    }

    public ControlScheme GetController()
    {
        return controller;
    }

    // @returns GetPort = [Range(1, 8)] int
    public int GetPort()
    {
        return port;
    }

    public void SetPort(int port)
    {
        this.port = port;
        anim.SetInteger("State", ((int)controller)+1);
        SetPlayerColor();
        selector.GetComponent<Image>().enabled = true;
        selecting = true;

        IncrementSelector(0);
    }

    private void SetPlayerColor()
    {
        float red = 255f, green = 255f, blue = 255f;

        if (playerNum == 0)
        {
            red = 255f;
            green = 150f;
            blue = 150f;
        }
        else if (playerNum == 1)
        {
            red = 0f;
            green = 200f;
            blue = 255f;
        }
        else if (playerNum == 2)
        {
            red = 255f;
            green = 110f;
            blue = 110f;
        }
        else if (playerNum == 3)
        {
            red = 0f;
            green = 150f;
            blue = 255f;
        }

        image.color = new Color(red / 255f, green / 255f, blue / 255f);
    }
}
