using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Any object that has this component should have the tag "Grabbable" on a child object (that acts as its trigger)

public class Grabbable : MonoBehaviour
{
    public bool isGrabbable = false,
        hittable = true;

    protected Grabber attachedToGrabber;
    protected FollowObject followScript;

    [HideInInspector] public bool inHitstun = false;
    [HideInInspector] public int totalAttackRecovery, attackRecoveryCounter;
    protected Damager lastDamager;

    protected Rigidbody2D m_Rigidbody2D;
    protected Collider2D m_Collider2D;

    public enum State
    {
        free,
        following,
        launching
    }

    public State currentState;

    // Use this for initialization
    protected virtual void Start()
    {
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_Collider2D = gameObject.GetComponent<Collider2D>();
        followScript = gameObject.AddComponent<FollowObject>();

        currentState = State.free;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (inHitstun)
        {
            attackRecoveryCounter--;
            if (attackRecoveryCounter <= 0)
                RecoverFromHit();
        }
    }

    public void reset()
    {

    }

    public void OnTakeDamage(Damager damager, Vector2 knockbackVector, int duration)
    {
        duration = 0;
        if (!damager.Equals(lastDamager))
        {
            lastDamager = damager;
            inHitstun = true;
            attackRecoveryCounter = totalAttackRecovery = duration;
            m_Rigidbody2D.velocity = knockbackVector;
        }
        EnableGrabberCollisions();
    }

    public void RecoverFromHit()
    {
        attackRecoveryCounter = 0;
        lastDamager = null;
        inHitstun = false;
    }

    // @Ensures No physics changes result from collisions between newPlayer and this ball
    //          The physics for this ball are frozen
    public void FollowEntity(Grabber newGrabber)
    {
        // Re-enable collisions for previous grabber
        if (attachedToGrabber != null)
        {
            Collider2D[] grabbingObject = attachedToGrabber.transform.parent.gameObject.GetComponents<Collider2D>();
            for (int i = 0; i < grabbingObject.Length; i++)
                Physics2D.IgnoreCollision(m_Collider2D, grabbingObject[i], false);
        }

        attachedToGrabber = newGrabber;
        //grabber.parent.gameObject.layer = 13/*SortingLayer.GetLayerValueFromName("ThrowingPlayer")*/;
        currentState = State.following;

        // Disable my physics
        m_Rigidbody2D.simulated = false;

        // Freeze my physics
        m_Rigidbody2D.velocity = new Vector2(0, 0);
        m_Rigidbody2D.angularVelocity = 0;

        // Begin following newGrabber
        followScript.following = attachedToGrabber.transform;
        followScript.enabled = true;
    }

    public void ReleaseFromEntity()
    {
        if (currentState == State.following)
        {
            Collider2D[] grabbingObject = attachedToGrabber.transform.parent.gameObject.GetComponents<Collider2D>();
            for (int i = 0; i < grabbingObject.Length; i++)
                Physics2D.IgnoreCollision(m_Collider2D, grabbingObject[i], true);
            currentState = State.launching;
            m_Rigidbody2D.simulated = true;

            followScript.enabled = false;
        }
    }

    // @Ensures This item will shoot out from the player in a [specified] direction
    public virtual void Launch(float force_x, float force_y)
    {
        ReleaseFromEntity();
        m_Rigidbody2D.velocity = new Vector2(force_x, force_y);
    }

    // Re-enable collisions between this item and the most recent grabber 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnableGrabberCollisions();
    }

    private void EnableGrabberCollisions()
    {
        if (attachedToGrabber != null)
        {
            Collider2D[] grabbingObject = attachedToGrabber.transform.parent.gameObject.GetComponents<Collider2D>();
            for (int i = 0; i < grabbingObject.Length; i++)
                Physics2D.IgnoreCollision(m_Collider2D, grabbingObject[i], false);
        }
    }
}
