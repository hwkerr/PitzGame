using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Male,
    Fem
}
public enum ControlScheme
{
    Joystick,
    KeyboardLeft,
    KeyboardRight
}

public static class GlobalValues {

    private static Player[] players = new Player[4];
    public static Player GetPlayer(int num)
    {
        return players[num];
    }
    public static void SetPlayer(int num, Character character)
    {
        players[num] = new Player(num, character);
    }
    public static void SetControls(int num, ControlScheme controlScheme)
    {
        players[num].SetControls(controlScheme);
    }
    public static void SetController(int playerNum, int port)
    {
        players[playerNum].SetController(port);
    }
    public static void ResetValues()
    {
        players = new Player[4];
    }
}

public class Player
{
    public int playerNum;
    public Character character;
    public ControlScheme controlScheme;
    public int port = 0;
    public Player(int num, Character character)
    {
        playerNum = num;
        this.character = character;
    }
    public void SetControls(ControlScheme controlScheme)
    {
        this.controlScheme = controlScheme;
    }
    public void SetController(int port)
    {
        controlScheme = ControlScheme.Joystick;
        this.port = port;
    }
}