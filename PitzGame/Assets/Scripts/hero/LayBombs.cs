﻿using UnityEngine;
using System.Collections;

public class LayBombs : MonoBehaviour
{
	[HideInInspector]
	public bool bombLaid = false;		// Whether or not a bomb has currently been laid.
	public int bombCount = 0;			// How many bombs the player has.
	public AudioClip bombsAway;			// Sound for when the player lays a bomb.
	public GameObject bomb;				// Prefab of the bomb.
    public string bombButton = "Fire2_P1";

	private GUITexture bombHUD;			// Heads up display of whether the player has a bomb or not.


	void Awake ()
	{
		// Setting up the reference.
		bombHUD = GameObject.Find("ui_bombHUD").GetComponent<GUITexture>();
	}


	void Update ()
	{
		// If the bomb laying button is pressed, the bomb hasn't been laid and there's a bomb to lay...
		if(Input.GetButtonDown(bombButton) && bombCount > 0)
		{
			// Decrement the number of bombs.
			bombCount--;

			// Play the bomb laying sound.
			AudioSource.PlayClipAtPoint(bombsAway,transform.position);

			// Instantiate the bomb prefab.
			Instantiate(bomb, transform.position, transform.rotation);
		}

		// The bomb heads up display should be enabled if the player has bombs, other it should be disabled.
		bombHUD.enabled = bombCount > 0;
	}
}
