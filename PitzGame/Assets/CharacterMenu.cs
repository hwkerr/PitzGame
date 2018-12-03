using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMenu : MonoBehaviour {

    [SerializeField]

    private int playerNum = 0;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetMale()
    {
        GlobalValues.SetPlayer(playerNum, Character.Male);
        playerNum++;
    }

    public void SetFem()
    {
        GlobalValues.SetPlayer(playerNum, Character.Fem);
        playerNum++;
    }
}
