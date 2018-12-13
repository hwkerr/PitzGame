using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour {

    private Rigidbody2D m_Rigidbody2D;
    
    public Damager lastDamager;

    // Use this for initialization
    void Awake () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public virtual void OnTakeDamage(Damager damager, Vector2 knockbackVector, int duration)
    {
        Debug.Log("Hit");
        if (!damager.Equals(lastDamager))
        {
            lastDamager = damager;
            m_Rigidbody2D.velocity = knockbackVector;
        }
    }

    public virtual void RecoverFromHit()
    {
        lastDamager = null;
    }
}
