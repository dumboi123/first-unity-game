
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public PlayerInput _playerInput;
    
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _dir = GetComponent<SpriteRenderer>();
        _coll = GetComponent<BoxCollider2D>();
        _playerInput = GetComponent<PlayerInput>();
        //var action = new InputAction();
    }
    private void Start()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.IsGrounded();
        _currentState.EnterState();
    }
    private void Update()
    {
        UpdateAnimation();
        _currentState.UpdateState();
    }
    private void UpdateAnimation()
    {
        switch (_dirX)
        {
            case > 0:
                _state = MovementStates.run;
                _dir.flipX = false;
                break;
            case < 0:
                _state = MovementStates.run;
                _dir.flipX = true;
                break;
            default:
                _state = MovementStates.idle;
                break;
        }
        if (!Grounded())
        {
            switch (_rb.velocity.y)
            {
                case > .1f:
                    _state = MovementStates.jump;
                    break;
                case < -.1f:
                    _state = MovementStates.fall;
                    _anim.SetBool("double_jump", false);
                    break;
            }
        }
        _anim.SetInteger("State", (int)_state);
    }
    public bool Grounded()
    {
        return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, _jumpcheck);
    }
    public void DoubleJump()
    {
        _anim.SetBool("double_jump", true);
        _doublejump = false;
    }
    public void Jumping()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce);
    }

    public bool JumpGetButton(){
        return _playerInput.actions["Jump"].IsPressed();
    }
    public bool JumpGetButtonDown(){
        return _playerInput.actions["Jump"].WasPressedThisFrame();
    }
}
