using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    [SerializeField]
    private Transform _plataform, _start, _end;
    private float speed = 2f;

    private Vector3 _destinypoint;

    private void Start()
    {
        _plataform.position = _start.position;
        _destinypoint = _end.position;

    }

    private void Update()
    {
        _plataform.position = Vector3.MoveTowards(_plataform.position, _destinypoint, speed * Time.deltaTime);

        if (_plataform.position == _destinypoint)
        {
            if (_plataform.position == _start.position)
            {
                _destinypoint = _end.position;
            }
            else if (_plataform.position == _end.position)
            {
                _destinypoint = _start.position;
            }
        }
    }

}
