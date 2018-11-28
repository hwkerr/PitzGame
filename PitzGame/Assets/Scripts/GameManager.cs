using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject[] thePlayers = new GameObject[4];
    [SerializeField] private GameObject[] characterPrefabs;

    public enum CharacterPrefab
    {
        MalePlayer,
        FemPlayer
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        AddPlayer(GlobalValues.GetPlayer(1));
        AddPlayer(GlobalValues.GetPlayer(2));
        AddPlayer(GlobalValues.GetPlayer(3));
        AddPlayer(GlobalValues.GetPlayer(4));
    }

    // @Requires 1 <= playerNum <= 4
    // @Ensures Instantiates a 'character' in the slot for player number 'playerNum'
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

            if (thePlayers[playerNum - 1] != null)
                Destroy(thePlayers[playerNum - 1]);
            thePlayers[playerNum - 1] = Instantiate(characterPrefabs[(int)character]);
            thePlayers[playerNum - 1].GetComponent<DefaultPlayer>().playerNum = playerNum;
            float xval = (playerNum * 4f) - 10f; // pick spawn position based on playerNum
            thePlayers[playerNum - 1].GetComponent<Transform>().position = new Vector3(xval, -0.5f, 0f);
        }
    }
}
