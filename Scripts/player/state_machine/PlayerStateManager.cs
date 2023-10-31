
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerStateManager : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private BoxCollider2D _coll;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpforce;
    [SerializeField] private LayerMask _jumpcheck;
    [SerializeField] private AudioSource _jump_sound;
    public float _input, _currentMoveInput;
    public bool _doublejump, _takedamage, _movePressed;
    public enum MovementStates { idle, run, jump, fall };
    public MovementStates _animState;
    private MovementStates _state;
    PlayerBaseState _currentState;
    PlayerStateFactory _states;
    //public PlayerDead P_DeadState = new PlayerDead();
    //public PlayerWalk P_WalkState = new PlayerWalk();
    //public PlayerIdle P_IdleState = new PlayerIdle();
    //public PlayerJump P_Jump = new PlayerJump();
    private Control _playerInput;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coll = GetComponent<BoxCollider2D>();
        _playerInput = new Control();
    }
    private void OnEnable(){
        _playerInput.Enable();
        _playerInput.Player.Move.started += MoveGetAxisRaw;
        _playerInput.Player.Move.performed += MoveGetAxisRaw;
        _playerInput.Player.Move.canceled += MoveGetAxisRaw;
    }
    private void OnDisable(){
        _playerInput.Disable();
        _playerInput.Player.Move.started -= MoveGetAxisRaw;
        _playerInput.Player.Move.performed -= MoveGetAxisRaw;
        _playerInput.Player.Move.canceled -= MoveGetAxisRaw;
    }
    private void Start()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.IsGrounded();
        _currentState.EnterState();
    }
    private void Update()
    {
        _currentState.UpdateStates();
        _rb.velocity = new Vector2(_speed*_input, _rb.velocity.y);
        HandleAnimation(_animState);
        //UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        switch (_input)
        {
            case > 0:
                _state = MovementStates.run;
                transform.localScale = new Vector2(1,1);
                break;
            case < 0:
                _state = MovementStates.run;
                transform.localScale = new Vector2(-1,1);
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
    private void HandleAnimation(MovementStates states){
        _anim.SetInteger("State",(int)states);
    }

    public bool Grounded()
    {
        return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, _jumpcheck);
    }    
    public void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce);
    }
    public void DoubleJump()
    {
        _anim.SetBool("double_jump", true);
        _doublejump = false;
    }

    public bool JumpGetButton(){
        return _playerInput.Player.Jump.IsPressed();
    }
    public bool JumpGetButtonDown(){
        return _playerInput.Player.Jump.WasPressedThisFrame();
    }
    private void MoveGetAxisRaw(InputAction.CallbackContext ctx){
        _movePressed = ctx.action.WasPressedThisFrame();
        _currentMoveInput =ctx.ReadValue<Vector2>().x;
        if(_currentMoveInput !=0)
            transform.localScale = _currentMoveInput > 0 ? new Vector2(1,1) : new Vector2(-1,1);
    }
    // private void checking(InputAction.CallbackContext ctx ){
    //     if(ctx.action.IsPressed())
    //     Debug.Log("Fire");
    // }

}
