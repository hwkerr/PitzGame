using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

    bool holding;
    bool canPickup;
    public Collider2D grabBox;

    private Grabbable heldItem;

    private Grabbable targetInRange;
    
    // Use this for initialization
	void Start () {
        //grabBox = gameObject.AddComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Picks up any item that is within range
    public void pickUpItem()
    {
        //Grabbable item = targetInRange;
        if (canPickup && !holding)
        {
            targetInRange.followEntity(this);
            holding = true;
            heldItem = targetInRange;
        }
    }

    // Releases any held item
    public void releaseItem()
    {
        if (holding)
        {
            heldItem.releaseFromEntity();
            holding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grabbable"))
        {
            GameObject trigger_parent = collision.transform.parent.gameObject;
            if (trigger_parent.GetComponent<Grabbable>().isGrabbable)
            {
                canPickup = true;
                targetInRange = trigger_parent.GetComponent<Grabbable>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Grabbable"))
        {
            GameObject trigger_parent = collision.transform.parent.gameObject;
            if (trigger_parent.GetComponent<Grabbable>().isGrabbable)
            {
                canPickup = false;
                targetInRange = null;
            }
        }
    }
}
