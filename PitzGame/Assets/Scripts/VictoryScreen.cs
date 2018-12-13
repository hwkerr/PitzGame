using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {

    //public GameObject selectedButton;
    
    // Use this for initialization
	void Start () {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReturnToMainMenu()
    {
        GlobalValues.ResetValues();
        Destroy(GameObject.Find("BackgroundMusic"));
        SceneManager.LoadScene(0);
    }
}
