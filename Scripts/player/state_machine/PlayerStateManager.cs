
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _dir;
    private BoxCollider2D _coll;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpforce;
    [SerializeField] private LayerMask _jumpcheck;
    [SerializeField] private AudioSource _jump_sound;
    private float _dirX;
    public bool _doublejump, _takedamage;
    private enum MovementStates { idle, run, jump, fall };
    private MovementStates _state;
    PlayerBaseState _currentState;
    PlayerStateFactory _states;
    //public PlayerDead P_DeadState = new PlayerDead();
    //public PlayerWalk P_WalkState = new PlayerWalk();
    //public PlayerIdle P_IdleState = new PlayerIdle();
    //public PlayerJump P_Jump = new PlayerJump();
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _dir = GetComponent<SpriteRenderer>();
        _coll = GetComponent<BoxCollider2D>();
        
    }
    private void Start()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.IsGrounded();
        _currentState.EnterState();
    }
}
