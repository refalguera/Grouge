using UnityEngine;
using System.Collections;

public class AttackMovement : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    private Transform target;
    private bool _facingright = false;

    public AudioClip _gosmsound;

  // Prefab of the magic that will be used by the enemy;
    [SerializeField]
    private GameObject _gosmprefab;

    [SerializeField]
    private Transform _shootposition;
    
    private float _FireRate = 2f;
    private float CanFire = 0.0f;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

// Pursue the player if the distance between the two is less than the stopping distance
    public void LateUpdate()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                GosmShoot();
            }
            else
            {
                this.gameObject.AddComponent<EnemyController>();
                this.GetComponent<AttackMode>().enabled = false;
            }
        }
    }
    
    // Control the launch of the enemy's weapon
    void GosmShoot()
    {
        if (Time.time > CanFire)
        {
            if (Mathf.Sign(target.position.x) == 1)
            {
                _gosmprefab.GetComponent<GosmController>()._laserDirection = false;
            }
            else
            {
                _gosmprefab.GetComponent<GosmController>()._laserDirection = true;
            }

            Instantiate(_gosmprefab, _shootposition.position, Quaternion.identity);
        }

        CanFire = Time.time + _FireRate;

    }

}
