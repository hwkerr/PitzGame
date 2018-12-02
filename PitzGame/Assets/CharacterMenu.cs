using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMenu : MonoBehaviour {

    private int playerNum = 0;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetMale()
    {
        GlobalValues.SetPlayer(playerNum, Character.Male);
        playerNum++;
        if (playerNum >= 2)
            PlayGame();
    }

    public void SetFem()
    {
        GlobalValues.SetPlayer(playerNum, Character.Fem);
        playerNum++;
        if (playerNum >= 2)
            PlayGame();
    }
}
