using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    PlayerPhysics _controller;
    Animator _anim;

    private float _gravity;
    private float _maxjumpvelocity;
    private float _minjumpvelocity;
    [HideInInspector]
    public int _life;
   
    private bool _facingright = false;

    [HideInInspector]
    public float maxjumpHeight = 4;
    [HideInInspector]
    public float minjumpHeight = 1;
    [HideInInspector]
    public float timetoJumpApex = .4f;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _jumpspeed;

    Vector3 velocity;
    [HideInInspector]
    public Vector2 _position;

    [SerializeField]
    private float _FireRate = 0.25f;
    private float CanFire = 0.0f;

    [SerializeField]
    private GameObject _laserprefab;

    [SerializeField]
    private GameObject _handprefab;

    [HideInInspector]
    public bool _shieldAtive;

    [HideInInspector]
    public bool result = false;

    [HideInInspector]
    public bool result_attack = false;

    private GameManager _gameManager;
    private UIManager _UIManager;
    private SpawnManager _spawnManager;
    private AudioSource _lasersound;

    // Use this for initialization
    void Start () {

        Inicializate_PlayerComponents();
        _anim = GetComponent<Animator>();
        _controller = GetComponent<PlayerPhysics>();
	}


//Initialize components, and calculate the severity.
//From this I calculate the maximum and minimum value that the player can skip.
    private void Inicializate_PlayerComponents()
    {
        _gravity = -(2 * maxjumpHeight) / Mathf.Pow(timetoJumpApex, 2);
        _maxjumpvelocity = Mathf.Abs(_gravity) * timetoJumpApex;
        minjumpHeight = Mathf.Sqrt(2 * Mathf.Abs(_gravity) * minjumpHeight);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _lasersound = GetComponent<AudioSource>();

        _life = 5;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (_controller.collisions.bellow)
        {
            if (result)
            {
                velocity.y = 20f;
                result = false;
            }

            if (result_attack)
            {
                LaserShoot();
            }
        }

        Movemment();
    }
    
//Controls the player's movement
    private void Movemment()
    {
        if(_controller.collisions.faceDirection == -1 && _facingright)
        {
            flip();
            _anim.SetBool("Walk", true);
        }
        else if(_controller.collisions.faceDirection == 1 && !_facingright)
        {
            flip();
            _anim.SetBool("Walk", true);
        }

        if (velocity.x == 0)
        {
            _anim.SetBool("Walk", false);
        }

        velocity.x = _position.x * _speed;
        velocity.y += _gravity * Time.deltaTime;

        _controller.Move(velocity * Time.deltaTime, _position);

          if (_controller.collisions.above || _controller.collisions.bellow)
       {
           velocity.y = 0;
       }
    }

//Controls the launch of the laser
    private void LaserShoot()
    {

        if (Time.time > CanFire)
        {
            _lasersound.Play();
            _laserprefab.GetComponent<Laser>()._laserDirection = _facingright;
            Instantiate(_laserprefab, _handprefab.transform.position, Quaternion.identity);
        }

        CanFire = Time.time + _FireRate;

    }

//Controls the damage done to the player
    public void Damage()
    {
        if (!_shieldAtive)
        {
            _life--;
            _UIManager.ChangeLifeBar(_life);

            if (_life < 1)
            {
                _gameManager.gameover = true;
                Destroy(this.gameObject);
            }
        }
    }
   // Change the direction of the animations (right and left);
    private void flip()
    {
        _facingright = !_facingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
}
