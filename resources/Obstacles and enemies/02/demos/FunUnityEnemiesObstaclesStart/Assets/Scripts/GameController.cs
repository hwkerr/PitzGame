using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public void QuitGame(){
		Application.Quit();
	}

	public void RestartGame(){
		SceneManager.LoadScene("Main");
	}
}
