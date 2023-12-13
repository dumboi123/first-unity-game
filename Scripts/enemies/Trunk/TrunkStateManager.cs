using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class TrunkStateManager : MonoBehaviour
{
    [SerializeField] Transform _wallCheckPoint;
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField] Transform _playerCheckPoint;
    [SerializeField] Transform _shootPoint;
    [SerializeField] Enemy_Parameter _parameter;
//===================================================================================
    private Animator _anim;
    private Rigidbody2D _rb;
    private Vector3 _direction;
    private bool _isIdle,_isStopShooting;
    TrunkBaseState _currentState;
    TrunkStateFactory _states;

//===================================================================================
    public enum MovementStates { idle, patrol, shoot};
    public MovementStates _animState;
    [NonSerialized]public bool _continue;
//===================================================================================
    public TrunkBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsIdle { get {return _isIdle;} set {_isIdle = value;} }
    public bool IsStopShooting { get {return _isStopShooting;} set{_isStopShooting = value;}}

    void OnDisable() => _rb.velocity = Vector2.zero;
    void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _direction = GetComponent<Transform>().localScale;
    }
    void Start()
    {
        _states = new TrunkStateFactory(this);
        _currentState = _states.Patrol();
        _currentState.EnterState();
    }
    void Update()
    {
        _currentState.UpdateState();
        HandleAnimation(_animState);

    }
    void FixedUpdate()
    {
        if(!_isIdle)
            _rb.velocity = new Vector2(-_parameter.MovementSpeed * _direction.x, _rb.velocity.y);
        else 
            _rb.velocity = Vector2.zero;
    }

    public void HandleAnimation(MovementStates states){
        _anim.SetInteger("State",(int)states);
    }
    
    public bool WallDetect()
    {
        return Physics2D.Raycast(_wallCheckPoint.position, _direction * Vector2.left ,_parameter.WallDetectDistance , _parameter._layer[0]);  
    }
    public bool GroundDetect()
    {
        return Physics2D.Linecast(transform.position, _groundCheckPoint.position , _parameter._layer[0]);
    }
    public bool PlayerDetect()
    {
        return Physics2D.Raycast(_playerCheckPoint.position, _direction* Vector2.left, _parameter.PlayerDetectDistance, _parameter._layer[1]);
    }

    public void StartIdleCoroutine()
    {
        StartCoroutine("Idle");
    }
    private IEnumerator Idle()
    {
        float curTime = 0;
        while(curTime <1.5f)
        {
            if(PlayerDetect())
            yield break;              
            curTime += Time.deltaTime;
            yield return null;
        }
        if(_continue) _continue = false;
        else          _direction.x *= -1;
        transform.localScale = _direction;
        _isIdle = false;
        yield break;
    }

    public void Shoot()
    {
        GameObject Bullet = Instantiate(_parameter.Bullet, _shootPoint.position, _shootPoint.rotation);
        Bullet.transform.localScale *= transform.localScale.x;
        Bullet.GetComponent<Rigidbody2D>().velocity = _direction * Vector2.left * _parameter.BulletSpeed;
    }
    public void CheckShootStop()
    {
        if(PlayerDetect())
            _isStopShooting = false;
        else 
            _isStopShooting = true;
    }

}
        // Debug.DrawRay(_playerCheckPoint.position, _direction* Vector2.left *_playerDistance, Color.green);
        // Debug.DrawRay(_wallCheckPoint.position, _direction* Vector2.left *_wallDistance, Color.red);
        // Debug.DrawLine(transform.position, _groundCheckPoint.position, Color.green);
