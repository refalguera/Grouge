using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : RayCastController {

    public CollisionInfo collisions;

    bool found = false;
    RaycastHit2D plataform;
    Vector3 atualizated_velocity;

    float maxClimbAngle = 80;
    float maxDescendAngle = 80;

    [HideInInspector]
    public Vector2 playerInput;

    Player _playerController;

    [SerializeField]
    private Camera _main;

    [HideInInspector]
    public int _capsulaCount;

    [SerializeField]
    private GameObject _shieldPrefab;

    private float _limitMovemmentleft;
    private float _limitMovemmentright;
    private GameManager _gameManager;
    private UIManager _UIManager;
    public AudioClip _shieldsound;

//Initializes components
    public override void Start()
    {
        base.Start();
        collisions.faceDirection = 1;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _playerController = GetComponent<Player>();


        _capsulaCount = 0;
        _gameManager._capsuleCount = _capsulaCount;
    }

    public void Move(Vector3 velocity, bool standinonPlataform)
    {
        Move(velocity, Vector2.zero, standinonPlataform);
    }

//Controls the movement of the player by analyzing vertical, horizontal collisions and ascents and decides on diagonal platforms.
    public void Move(Vector3 velocity, Vector2 input, bool standinonPlataform = false)
    {
        UpdateRayCastOrigins();
        collisions.Reset();
        collisions.velocityOld = velocity;
        playerInput = input;

        if (velocity.x != 0)
        {
            collisions.faceDirection = (int)Mathf.Sign(velocity.x);
        }

        if (velocity.y < 0)
        {
            DescendSlope(ref velocity);
        }

        if (velocity.x != 0) {
            HorizontalCollisions(ref velocity);
           }

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

       
        transform.Translate(velocity);

        // Limit Player movement based on camera position

        MovemmentBounds();

        if (standinonPlataform)
        {
            collisions.bellow = true;
        }
    }

  //Controls vertical collisions
    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLegth = Mathf.Abs(velocity.y) + skinWidth * 8;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayorigin = (directionY == -1) ? rayCastOrigin.bottomLeft : rayCastOrigin.topLeft;
            rayorigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayorigin, Vector2.up * directionY, rayLegth, collisionMask);
            Debug.DrawRay(rayorigin, Vector2.up * directionY * rayLegth * 8, Color.red);

            if (hit)
            {
               //Player can climb from bottom to top on platforms with the tag Through. 
               //If it rises on movable platforms it falls.
                if (hit.collider.tag == "Through")
                {
                    if (directionY == 1 || hit.distance == 0)
                    {
                        continue;
                    }
                    if (collisions.FallingThroughPla)
                    {
                        continue;
                    }
                    if (playerInput.y == -1)
                    {
                        collisions.FallingThroughPla = true;
                        Invoke("ResetFallingThroughPlataform", .5f);
                        continue;
                    }

                }


                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLegth = hit.distance;

                if (collisions.climbSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.diagonalangle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }
                collisions.above = directionY == 1;
                collisions.bellow = directionY == -1;
            }
        }

     //Controls climb collisions

        if (collisions.climbSlope)
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLegth = Mathf.Abs(velocity.x) + skinWidth;
            Vector2 rayorigin = ((directionX == -1) ? rayCastOrigin.bottomLeft : rayCastOrigin.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayorigin, Vector2.right * directionX, rayLegth, collisionMask);

            if (hit)
            {
                float diagonalangle = Vector2.Angle(hit.normal, Vector2.up);
                if (diagonalangle != collisions.diagonalangle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.diagonalangle = diagonalangle;
                }
            }
        }

    }

    //Controls horizontal collisions
    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = collisions.faceDirection;
        float rayLegth = Mathf.Abs(velocity.x) + skinWidth * 8;

        if (Mathf.Abs(velocity.x) < skinWidth)
        {
            rayLegth = 6 * skinWidth;
        }

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayorigin = (directionX == -1) ? rayCastOrigin.bottomLeft : rayCastOrigin.bottomRight;
            rayorigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayorigin, Vector2.right * directionX, rayLegth, collisionMask);

            Debug.DrawRay(rayorigin, Vector2.right * directionX * rayLegth * 8, Color.red);
               
               //If there was a collision, it verifies with what object it had and performs a given action
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Flag")
                {
                    if (hit.collider.name == "Begining_Flag")
                    {
                        _gameManager.startgame = true;
                        hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else if (hit.collider.name == "Final_Flag")
                    {
                        _gameManager.gameover = true;
                        hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
                else if (hit.collider.gameObject.tag == "Capsules")
                {
                    _UIManager.ChangeColorCapsulasParticulesScore(_capsulaCount);
                    _capsulaCount++;
                    _gameManager._capsuleCount = _capsulaCount;
                    if (_capsulaCount == 7)
                    {
                        _capsulaCount++;
                        _UIManager.ChangeColorCapsulasParticulesScore(_capsulaCount);
                    }
                    Destroy(hit.collider.gameObject);
                }

                else if (hit.collider.gameObject.tag == "PowerUp")
                {
                    AudioSource.PlayClipAtPoint(_shieldsound, Camera.main.transform.position, 1f);
                    Destroy(hit.collider.gameObject);
                    _shieldPrefab.SetActive(true);
                    _playerController._shieldAtive = true;
                    StartCoroutine(ShieldAtive());
                    _playerController._shieldAtive = false;

                }
                else if (hit.collider.gameObject.tag == "Life")
                {
                    Destroy(hit.collider.gameObject);
                    _playerController._life++;
                    _UIManager.ChangeLifeBar(_playerController._life);

                }
            
                //Get Angle
                float diagonalangle = Vector2.Angle(hit.normal, Vector2.up);

                if (i == 0 && diagonalangle <= maxClimbAngle)
                {
                    if (collisions.descendSlope)
                    {
                        collisions.descendSlope = false;
                        velocity = collisions.velocityOld;
                    }
                    float distanceSlopeStart = 0;
                    if (diagonalangle != collisions.diagonalangleOld)
                    {
                        distanceSlopeStart = hit.distance - skinWidth;
                        velocity.x -= distanceSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, diagonalangle);
                    velocity.x += distanceSlopeStart * directionX;
                }

                if (!collisions.climbSlope || diagonalangle > maxClimbAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLegth = hit.distance;

                    if (collisions.climbSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.diagonalangle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }

    }

//Controls diagonal rises
    void ClimbSlope(ref Vector3 velocity, float diagonalangle)
    {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(diagonalangle * Mathf.Deg2Rad) * moveDistance;
        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(diagonalangle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.bellow = true;
            collisions.climbSlope = true;
            collisions.diagonalangle = diagonalangle;
        }
    }

//Controlling descending diagonally
    void DescendSlope(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        Vector2 rayOrigin = (directionX == -1) ? rayCastOrigin.bottomRight : rayCastOrigin.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            float diagonalangle = Vector2.Angle(hit.normal, Vector2.up);
            if (diagonalangle != 0 && diagonalangle <= maxDescendAngle)
            {
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if ((hit.distance - skinWidth) <= Mathf.Tan(diagonalangle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(diagonalangle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(diagonalangle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;

                        collisions.diagonalangle = diagonalangle;
                        collisions.descendSlope = true;
                        collisions.bellow = true;

                    }
                }
            }
        }

    }

    void ResetFallingThroughPlataform()
    {
        collisions.FallingThroughPla = false;
    }

    private void MovemmentBounds()
    {
        _limitMovemmentleft = -8.45f + _main.transform.position.x;
        _limitMovemmentright = 8.47f + _main.transform.position.x;

        if (transform.position.x < _limitMovemmentleft)
        {
            transform.position = new Vector3(_limitMovemmentleft, transform.position.y, 0);
        }
        else if (transform.position.x > _limitMovemmentright)
        {
            transform.position = new Vector3(_limitMovemmentright, transform.position.y, 0);
        }

    }

    IEnumerator ShieldAtive()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldPrefab.SetActive(false);
    }

    public struct CollisionInfo
    {
        public bool above, bellow;
        public bool left, right;
        public bool climbSlope;
        public bool descendSlope;
        public float diagonalangle, diagonalangleOld;
        public int faceDirection;
        public bool FallingThroughPla;


        public Vector3 velocityOld;

        public void Reset()
        {
            above = false;
            bellow = false;
            left = false;
            right = false;
            climbSlope = false;
            descendSlope = false;
            diagonalangleOld = diagonalangle;
            diagonalangle = 0;
        }
    }

}
