using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;
public class BlueBird: MonoBehaviour
{
    [SerializeField] Transform[] _wayPoint;
    [SerializeField] float _speed;
    private sbyte _currentWayPoint = 0;
    private Vector3 _direction;

    void Start()
    {
        _direction = GetComponent<Transform>().localScale;
    }
    private void Update()
    {
        if(Vector2.Distance(transform.position, _wayPoint[_currentWayPoint].position) < .1f)
        {
            _currentWayPoint++;
            _direction.x *= -1;
            transform.localScale = _direction;
            if (_currentWayPoint >= _wayPoint.Length)
            {
                _currentWayPoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, _wayPoint[_currentWayPoint].position, Time.deltaTime * _speed);
    }
}
