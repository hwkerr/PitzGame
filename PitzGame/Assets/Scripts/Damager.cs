using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    Collider2D myHurtbox;

    public float knockback;
    public float m_Angle;
    public int duration;

    private float force_x,
        force_y;
    
    // Use this for initialization
	void Start () {
        myHurtbox = gameObject.GetComponent<Collider2D>();
        
        force_x = knockback * Mathf.Cos(m_Angle * Mathf.Deg2Rad);
        force_y = knockback * Mathf.Sin(m_Angle * Mathf.Deg2Rad);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector2 GetKnockbackVector(Transform obj)
    {
        float relative_force_x;
        if (obj.position.x > transform.position.x) // obj is to the right of this Damager
            relative_force_x = 1 * force_x;
        else // obj is to the left of this Damager
            relative_force_x = -1 * force_x;
        return new Vector2(relative_force_x, force_y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject incoming = collision.gameObject;
        PlayerMovement incomingPlayer = incoming.GetComponent<PlayerMovement>();
        if (incomingPlayer != null)
            incomingPlayer.OnGetHit(this, GetKnockbackVector(incoming.transform), duration);
    }
}
