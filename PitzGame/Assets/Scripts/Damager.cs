﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    Collider2D myHurtbox; //@requires myHurtbox.isTrigger = true

    public float knockback = 10;
    public float m_Angle = 45;
    public float damage = 5;
    public int duration = 100;
    public bool usesRelativeDirection = false;

    private GameObject ignoreObject;

    private float force_x,
        force_y;

    private List<GameObject> targets;
    
    // Use this for initialization
	void Awake () {
        myHurtbox = GetComponent<Collider2D>();
        targets = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ResetTargets()
    {
        targets.Clear();
    }

    public void IgnoreObject(GameObject obj)
    {
        ignoreObject = obj;
    }

    // @param obj = the transform of the target (is checked for relative x position)
    // @param useRelativeDirection = whether to use the relative x position of obj (to knock obj away from the center of this object)
    // @Ensures GetKnockbackVector is this Damager's knockback vector (in the direction the player is facing)
    public Vector2 GetKnockbackVector(Transform obj)
    {
        float relative_force_x = force_x;
        CharacterController2D player = GetComponentInParent<CharacterController2D>();
        if (player!= null && !player.FacingRight())
        {
            relative_force_x *= -1;
        }

        if (usesRelativeDirection)
        {
            if (obj.position.x < transform.position.x) // obj is to the left of this Damager
                relative_force_x *= -1;
        }

        return new Vector2(relative_force_x, force_y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UpdateAngle();
        bool isIgnoredObject = false;
        if (ignoreObject != null)
        {
            Collider2D[] colliders = ignoreObject.GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < colliders.Length; i++)
                if (collision == colliders[i])
                    isIgnoredObject = true;
        }
        GameObject incoming = collision.gameObject;
        if (!isIgnoredObject && !AlreadyHit(collision))
        {
            if (collision.gameObject.name == "Head" || collision.gameObject.name == "Torso")
            {
                targets.Add(collision.transform.parent.gameObject);
                DefaultPlayer incomingPlayer = incoming.GetComponentInParent<DefaultPlayer>();
                if (incomingPlayer != null)
                    incomingPlayer.OnTakeDamage(this, GetKnockbackVector(incoming.transform), damage, duration);
                else
                    Debug.Log("Damager.OnTriggerEnter2D could not apply to incomingPlayer");
            }
            else if (collision.gameObject.name == "Ball")
            {
                targets.Add(collision.gameObject);
                Grabbable grabbable = incoming.GetComponent<Grabbable>();
                if (grabbable != null)
                    grabbable.OnTakeDamage(this, GetKnockbackVector(incoming.transform), duration);
                else
                    Debug.Log("Damager.OnTriggerEnter2D could not apply to incoming object");
            }
        }
    }

    // @returns AlreadyHit = true if this damager has already damaged collision.gameObject
    private bool AlreadyHit(Collider2D collision)
    {
        if (collision.gameObject.name == "Head" || collision.gameObject.name == "Torso")
            return targets.Contains(collision.transform.parent.gameObject);
        else
            return targets.Contains(collision.gameObject);
    }

    private void UpdateAngle()
    {
        force_x = knockback * Mathf.Cos(m_Angle * Mathf.Deg2Rad);
        force_y = knockback * Mathf.Sin(m_Angle * Mathf.Deg2Rad);
    }
}
