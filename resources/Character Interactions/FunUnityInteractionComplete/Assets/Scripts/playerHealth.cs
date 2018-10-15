using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {


	AudioSource playerAS;

	public Image damageIndicator;
	public Text rubyCount;
	public CanvasGroup endGameCanvas;
	public Text EndGameText;

	Color flashColour = new Color (255f, 255f, 255f, 0.5f);

	//Player death
	playerControllerScript controlMovement;
	public GameObject playerDeathFX;


	//ruby collection
	int collectedRubies = 0;


	// Use this for initialization
	void Start () {
		controlMovement = GetComponent<playerControllerScript> ();
		playerAS = GetComponent<AudioSource> ();
		rubyCount.text = collectedRubies.ToString();
	}
		
	public void makeDead(){
		//kill the player - death particles - destroy character - sound
		Instantiate (playerDeathFX, transform.position, transform.rotation);
		damageIndicator.color = flashColour;
		EndGameText.text = "You Died!";
		winGame();
		Destroy (gameObject);
	}

	public void addRuby(){
		collectedRubies +=1;
		rubyCount.text = collectedRubies.ToString();
		if(collectedRubies>2){
			EndGameText.text = "You Win!";
			GetComponent<playerControllerScript>().toggleCanMove();
			winGame();
		}
	}

	public void winGame(){
		endGameCanvas.alpha = 1f;
		endGameCanvas.interactable = true;
	}
}
