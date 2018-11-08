using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    [SerializeField]
    private float _speed;

    [HideInInspector]
    public bool _laserDirection;

    private bool _facingright = false;

    private Spawn_Manager _spawn;

	// Use this for initialization
	void Start () {
        _speed = 6;
        _spawn = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
	}
	
	//Controls laser movement
	void Update () {

        if (_laserDirection)
        {
            flip();
            transform.Translate(Vector3.right * _speed * Time.deltaTime);

        }
        else if (!_laserDirection)
        {
            flip();
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        Destroy(this.gameObject, 2f);
    }

	//Check laser bumps with other objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<Enemy_Controller>().Damage();
            Destroy(this.gameObject);

        }
    }

    private void flip()
    {
        _facingright = !_facingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
}
