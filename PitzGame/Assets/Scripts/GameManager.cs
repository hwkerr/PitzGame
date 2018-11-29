using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject[] thePlayers = new GameObject[4];
    [SerializeField] private GameObject[] characterPrefabs;

    private Ball theBall;

    private CountdownScript timer;

    public enum CharacterPrefab
    {
        MalePlayer,
        FemPlayer
    }

    // Use this for initialization
    void Start () {
        timer = GetComponent<CountdownScript>();
        theBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();

        timer.TogglePause(true);
        timer.ResetTimer();

        theBall.ResetBall();

        GlobalValues.SetPlayer(0, Character.Male);
        GlobalValues.SetPlayer(1, Character.Fem);

        AddPlayer(GlobalValues.GetPlayer(0));
        AddPlayer(GlobalValues.GetPlayer(1));
        AddPlayer(GlobalValues.GetPlayer(2));
        AddPlayer(GlobalValues.GetPlayer(3));
    }

    private void Update()
    {
        GetComponent<CountdownScript>().GetTime();
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
            float xval = ((playerNum+1) * 4f) - 10f; // pick spawn position based on playerNum
            thePlayers[playerNum].GetComponent<Transform>().position = new Vector3(xval, -0.5f, 0f);
        }
    }

    private void OnGUI()
    {
        if (true)
        {
            GUI.Label(new Rect(280, 10, 100, 20), GetComponent<CountdownScript>().GetFormattedTime());
        }
    }
}
