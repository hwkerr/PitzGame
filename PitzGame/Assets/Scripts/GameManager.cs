using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject[] thePlayers = new GameObject[4];
    [SerializeField] private GameObject[] characterPrefabs;

    public enum Character
    {
        MalePlayer,
        FemPlayer
    }
    
    // Use this for initialization
	void Start () {
        AddPlayer(1, Character.MalePlayer);
        AddPlayer(4, Character.MalePlayer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // @requires 1 <= playerNum <= 4
    private void AddPlayer(int playerNum, Character character)
    {
        if (thePlayers[playerNum-1] != null)
            Destroy(thePlayers[playerNum-1]);
        thePlayers[playerNum-1] = Instantiate(characterPrefabs[(int)character]);
        thePlayers[playerNum-1].GetComponent<DefaultPlayer>().playerNum = playerNum;
        float xval = (playerNum * 4f) - 10f;
        thePlayers[playerNum-1].GetComponent<Transform>().position = new Vector3(xval, -0.5f, 0f);
    }
}
