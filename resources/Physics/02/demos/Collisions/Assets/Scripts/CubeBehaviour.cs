using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour {

    public Vector2 _forceVector;
    public float _rotateValue;
    private Rigidbody2D _rb2d;

	//Use this for initialization
	void Start ()
    {
        _rb2d = GetComponent<Rigidbody2D>();
       /* _rb2d.AddForce(_forceVector, ForceMode2D.Impulse);
        _rb2d.AddTorque(_rotateValue, ForceMode2D.Impulse);*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _rb2d.AddForce(_forceVector, ForceMode2D.Impulse);
            _rb2d.AddTorque(_rotateValue, ForceMode2D.Impulse);
        }    
    }
}
