using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour {

	public GameObject rewardObject;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Instantiate(rewardObject, transform.position, Quaternion.identity);
			other.gameObject.GetComponent<playerHealth>().addRuby();
			Destroy(transform.root.gameObject);
		}
	}
}
