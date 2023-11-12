
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerStateManager : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private float _localScaleX;
    private BoxCollider2D _coll;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpforce;
    [SerializeField] private LayerMask _jumpcheck;
    [SerializeField] private AudioSource _jump_sound;
    public float _input, _currentMoveInput;
    public bool _doublejump, _takedamage, _movePressed, _jumpPressed, _jumpPressing ;
    public enum MovementStates { idle, walk, jump, fall };
    public MovementStates _animState;
    private MovementStates _state;
    PlayerBaseState _currentState;
    PlayerStateFactory _states;
    
    private Control _playerInput;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coll = GetComponent<BoxCollider2D>();
        _playerInput = new Control();
        _localScaleX = GetComponent<Transform>().localScale.x;
    }
    private void OnEnable(){
        _playerInput.Enable();
        _playerInput.Player.Move.started += MoveGetAxisRaw;
        _playerInput.Player.Move.performed += MoveGetAxisRaw;
        _playerInput.Player.Move.canceled += MoveGetAxisRaw;

        // _playerInput.Player.Jump.started += JumpGetButtonDown;
        // _playerInput.Player.Jump.performed += JumpGetButtonDown;
        // _playerInput.Player.Jump.canceled += JumpGetButtonDown;
    }
    private void OnDisable(){
        _playerInput.Disable();
        _playerInput.Player.Move.started -= MoveGetAxisRaw;
        _playerInput.Player.Move.performed -= MoveGetAxisRaw;
        _playerInput.Player.Move.canceled -= MoveGetAxisRaw;

        // _playerInput.Player.Jump.started -= JumpGetButtonDown;
        // _playerInput.Player.Jump.performed -= JumpGetButtonDown;
        // _playerInput.Player.Jump.canceled -= JumpGetButtonDown;
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
    // private void UpdateAnimation()
    // {
    //     switch (_input)
    //     {
    //         case > 0:
    //             _state = MovementStates.walk;
    //             transform.localScale = new Vector2(1,1);
    //             break;
    //         case < 0:
    //             _state = MovementStates.walk;
    //             transform.localScale = new Vector2(-1,1);
    //             break;
    //         default:
    //             _state = MovementStates.idle;
    //             break;
    //     }
    //     if (!Grounded())
    //     {
    //         switch (_rb.velocity.y)
    //         {
    //             case > .1f:
    //                 _state = MovementStates.jump;
    //                 break;
    //             case < -.1f:
    //                 _state = MovementStates.fall;
    //                 _anim.SetBool("double_jump", false);
    //                 break;
    //         }
    //     }
    //     _anim.SetInteger("State", (int)_state);
    // }
    public void HandleAnimation(MovementStates states){
        _anim.SetInteger("State",(int)states);
    }
    public float GetVelocityY(){
        return _rb.velocity.y;
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
    public void OutDoubleJump(){
        _anim.SetBool("double_jump", false);
    }
    public bool JumpGetButton(){
        return _playerInput.Player.Jump.IsPressed();
    }
    public bool JumpGetButtonDown(){
        return _playerInput.Player.Jump.WasPressedThisFrame();
    }
     private void MoveGetAxisRaw(InputAction.CallbackContext ctx){
        _movePressed = ctx.action.IsPressed();
        _currentMoveInput =ctx.ReadValue<Vector2>().x;
        if(_currentMoveInput !=0)
            transform.localScale = _currentMoveInput > 0 ? new Vector2(1,1) : new Vector2(-1,1);
    }



    public void SetDoubleJump(bool x)
    {
        _doublejump = x;
    }
    public void SetSpeed(float x)
    {
        _speed = x;
    }
    public void SetJumpforce(float x)
    {
        _jumpforce = x;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && gameObject.tag =="Untagged")  
            StartCoroutine("KnockBack");
    }
    IEnumerator KnockBack()
    {
        _takedamage = true;
        _rb.velocity = Vector2.zero;
        // if (!dir.flipX) 
        if(_localScaleX == -1) _rb.AddForce(new Vector2(-200f, 200f));
        else _rb.AddForce(new Vector2(200f, 200f));
        yield return new WaitForSeconds(0.5f);
        _takedamage = false;
    }

}
