using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Male,
    Fem
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
}

public class Player
{
    public int playerNum;
    public Character character;
    public Player(int num, Character character)
    {
        playerNum = num;
        this.character = character;
    }
}