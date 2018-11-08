using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : RayCastController
{
    CollisionInfo _collisions;

    [SerializeField]
    private LayerMask _layer;

    private Vector3[] localWayPoints = new Vector3[2];
    private Vector3[] globalWayPoints;
   
    Animator anim;

    public int _life;

    public float speed = 2;
    public bool cyclic = true;
    public float WaitTime = 1;
    [Range(0, 2)]
    public float EaseAmount = 1;
    Vector3 velocity;

    private Transform _target;
    private float _stoppingDistance = 8f;

    private bool _facingright = false;

    int fromWayPointIndex;
    float percentBetweenWayPoints;
    float NextMovetime;

    private bool foundplayer = false;
    private bool _fallplataform = false;

    public override void Start()
    {
        base.Start();

        _collisions = new CollisionInfo();
        anim = GetComponent<Animator>();

        Inicializate();
    }

    void Inicializate()
    {
        collisionMask = 512;
        _layer = 256;

        localWayPoints[0] = new Vector3(0, 0, 0);
        localWayPoints[1] = new Vector3(4, 0, 0);

        globalWayPoints = new Vector3[localWayPoints.Length];

        for (int i = 0; i < localWayPoints.Length; i++)
        {
            globalWayPoints[i] = localWayPoints[i] + transform.position;
        }
    }
    private void Update()
    {
        UpdateRayCastOrigins();

        if (foundplayer == false)
        {
            velocity = CalculateMovement();
            HorizontalCollisions(ref velocity);

            if (_fallplataform == false)
            {
                VerticalCollisions(ref velocity);
            }
            transform.Translate(velocity);
        }
        else
        {
            this.gameObject.GetComponent<AttackMovemment>().enabled = true;
            Destroy(this.gameObject.GetComponent<EnemyController>());
        }
        
    }

    float Ease(float x)
    {
        float a = EaseAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }
    // Movimentação da Plataforma: Calcula de que ponto a que ponto a plataforma se movimenta, e a sua velocidade e o tempo de espera para se locomover para o outro ponto
    Vector3 CalculateMovement()
    {
        if (Time.time < NextMovetime)
        {
            return Vector3.zero;
        }
        fromWayPointIndex %= globalWayPoints.Length;

        int toWayPonitIndex = (fromWayPointIndex + 1) % globalWayPoints.Length;
        float DistanceBetweenWayPoints = Vector3.Distance(globalWayPoints[fromWayPointIndex], globalWayPoints[toWayPonitIndex]);
        percentBetweenWayPoints += Time.deltaTime * speed / DistanceBetweenWayPoints;

        //Deixa a variavel co valores entre 0 e 1 para que nao tenha valores diferentes da função Ease retorna; 
        percentBetweenWayPoints = Mathf.Clamp01(percentBetweenWayPoints);
        float EasePercBetweenWayPoints = Ease(percentBetweenWayPoints);

        Vector3 newPos = Vector3.Lerp(globalWayPoints[fromWayPointIndex], globalWayPoints[toWayPonitIndex], EasePercBetweenWayPoints);
        if (percentBetweenWayPoints >= 1)
        {
            percentBetweenWayPoints = 0;
            fromWayPointIndex++;

            if (!cyclic)
            {
                if (fromWayPointIndex >= globalWayPoints.Length - 1)
                {
                    fromWayPointIndex = 0;
                    System.Array.Reverse(globalWayPoints);
                }
                NextMovetime = Time.time + WaitTime;
            }
        }

        return newPos - transform.position;

    }

    // Função que trabalha com qualquer objeto que foi afetado pelo movimento da plataforma (que esta sendo; foi movimentado pela plataforma);
    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLegth = Mathf.Abs(velocity.x) + skinWidth * 10;

        if (Mathf.Abs(velocity.x) < skinWidth)
        {
            rayLegth = 2 * skinWidth;
        }

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayorigin = (directionX == -1) ? rayCastOrigin.bottomLeft : rayCastOrigin.bottomRight;
            rayorigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayorigin, Vector2.right * directionX, rayLegth, collisionMask);

            Debug.DrawRay(rayorigin, Vector2.right * directionX * rayLegth * 10, Color.yellow);

            if (hit)
            {
                if (hit.collider.tag == "Player")
                {
                    foundplayer = true;
                }
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLegth = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayorigin = (directionY == -1) ? rayCastOrigin.bottomLeft : rayCastOrigin.topLeft;
            rayorigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayorigin, Vector2.up * directionY, rayLegth, _layer);
            Debug.DrawRay(rayorigin, Vector2.up * directionY * rayLegth, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Base")
                {
                    _fallplataform = true;

                    velocity.y = (hit.distance - skinWidth) * directionY;
                    rayLegth = hit.distance;
                    _collisions.above = directionY == 1;
                    _collisions.bellow = directionY == -1;

                }
            }
        }

    }

    public void Damage()
    {
        if(_life < 1)
        {
            Destroy(this.gameObject);
        }

        _life--;
    }
    private void flip()
    {
        _facingright = !_facingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    private void OnDrawGizmos()
    {
        if (localWayPoints != null)
        {
            Gizmos.color = Color.red;
            float size = .3f;

            for (int i = 0; i < localWayPoints.Length; i++)
            {
                Vector3 globalWayPointPos = (Application.isPlaying) ? globalWayPoints[i] : localWayPoints[i] + transform.position;
                Gizmos.DrawLine(globalWayPointPos - Vector3.up * size, globalWayPointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWayPointPos - Vector3.left * size, globalWayPointPos + Vector3.left * size);
            }
        }
    }

    public struct CollisionInfo
    {
        public bool above, bellow;
        public bool left, right;
        public int faceDirection;
   
        public Vector3 velocityOld;

        public void Reset()
        {
            above = false;
            bellow = false;
            left = false;
            right = false;
        }
    }

}
