using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    Collider2D myHurtbox;
    
    // Use this for initialization
	void Start () {
        myHurtbox = gameObject.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject incoming = collision.transform.parent.gameObject;
        incoming.GetComponent<DefaultPlayer>().GetHit(this);
    }
}
