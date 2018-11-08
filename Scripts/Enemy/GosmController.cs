using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GosmController : MonoBehaviour {

    [SerializeField]
    private float _speed;

    [HideInInspector]
    public bool _laserDirection;

    private bool _facingright = false;

    // Use this for initialization
    void Start()
    {
        _speed = 6;
    }

    // Update is called once per frame
    void Update()
    {

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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<Player>().Damage();
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
