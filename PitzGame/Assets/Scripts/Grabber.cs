﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

    bool holding;
    bool canPickup;
    public Collider2D grabBox;

    private Grabbable heldItem;

    private Grabbable targetInRange;
    
    // Picks up any item that is within range
    public void PickUpItem()
    {
        //Grabbable item = targetInRange;
        if (canPickup && !holding)
        {
            targetInRange.FollowEntity(this);
            holding = true;
            heldItem = targetInRange;
        }
    }

    // Releases any held item
    public void ReleaseItem()
    {
        if (holding)
        {
            heldItem.ReleaseFromEntity();
            holding = false;
        }
    }

    public void ThrowItem(float force_x, float force_y)
    {
        if (holding)
        {
            heldItem.Launch(force_x, force_y);
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
