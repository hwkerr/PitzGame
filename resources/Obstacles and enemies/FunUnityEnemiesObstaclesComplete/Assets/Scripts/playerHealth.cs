using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

	public AudioClip playerDamaged;
	AudioSource playerAS;

	public float fullHealth;
	float currentHealth;

	public GameObject playerDeathFX;

	public Image healthSlider;
	public Image damageIndicator;
	public Text rubyCount;
	public CanvasGroup endGameCanvas;
	public Text EndGameText;

	Color flashColour = new Color (255f, 255f, 255f, 0.5f);
	float indicatorSpeed = 5f;

	bool damaged;

	//Player death
	playerControllerScript controlMovement;

	//Coin collection
	int collectedRubies = 0;

	// Use this for initialization
	void Start () {
		controlMovement = GetComponent<playerControllerScript> ();
		rubyCount.text = collectedRubies.ToString();
		currentHealth = fullHealth;
		playerAS = GetComponent<AudioSource> ();
		healthSlider.fillAmount = 0f;
	}

	void Update(){
		if (damaged) {
			damageIndicator.color = flashColour;
		} else {
			damageIndicator.color = Color.Lerp (damageIndicator.color, Color.clear, indicatorSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void addDamage(float damage){
		if (damage <= 0)
			return;
		currentHealth -= damage;

		healthSlider.fillAmount = 1 - currentHealth / fullHealth;

		playerAS.PlayOneShot (playerDamaged);

		damaged = true;

		if (currentHealth <= 0) {
			makeDead ();
		}
	}
		
	public void makeDead(){
		damageIndicator.color = flashColour;
		Instantiate (playerDeathFX, transform.position, Quaternion.identity);
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
