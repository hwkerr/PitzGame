using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float _speed;
    private float _horizontal;
    public float _jumpForce;
    private Rigidbody2D _rb2d;

    public Material _red;

	// Use this for initialization
    private void Start ()
    {
        _rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
       Movement();
       Jumping();
	}

    private void Movement()
    {
        _horizontal = Input.GetAxis("Horizontal");
        Vector3 Mov = new Vector2(_horizontal, 0f);
        transform.position += Mov * _speed * Time.deltaTime;
    }

    private void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rb2d.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Cube"))
        {
            GetComponent<Renderer>().material = _red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            collision.gameObject.transform.localScale *= 0.5f;
        }
    }

    
}
