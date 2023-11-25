
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
public class PlayerStateManager : MonoBehaviour
{
//===================================================================================
    
    private bool _isTouchingWall,_isWallSliding, _isWallJump;
    [Header("WallJump")]
    public float _wallSlideSpeed;
    [SerializeField] Vector2 WallJumpForce;
    [SerializeField] Transform GroundCheckPoint;
    [SerializeField] Vector2 GroundCheckSize;
    [SerializeField] Transform WallCheckPoint;
    [SerializeField] Vector2 WallCheckSize;
//===================================================================================
    
    private Rigidbody2D _rb;
    private Animator _anim;
    
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpforce;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private AudioSource _jump_sound;
    public float _input, _currentMoveInput;
    public bool _doublejump, _takedamage, _movePressed, _jumpPressed, _jumpPressing ;
    public enum MovementStates { idle, walk, jump, fall, wallslide };
    public MovementStates _animState;
    //private MovementStates _state;
    PlayerBaseState _currentState;
    PlayerStateFactory _states;
    
    private Control _playerInput;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsTouchingWall { get{return _isTouchingWall;}}
    public bool IsWallSliding { set{ _isWallSliding = value;}}
    public bool IsWallJump { set{_isWallJump = value;}}

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
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
        HandleAnimation(_animState);
        //Debug.Log("current state: " + _currentState);
    }
    private void FixedUpdate(){
        if(!_takedamage)
            OnMovement();
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
    
    private void OnMovement(){
        if (_isWallSliding && _currentMoveInput != 0 )
            _rb.velocity = new Vector2(_rb.velocity.x, -_wallSlideSpeed);
        if (_isWallJump){
            _rb.velocity = new Vector2(-_input*WallJumpForce.x, WallJumpForce.y);
            Invoke("StopWallJump",0.1f);
        }
        else 
            _rb.velocity = new Vector2(_speed*_input, _rb.velocity.y);
    }
    public void HandleAnimation(MovementStates states){
        _anim.SetInteger("State",(int)states);
    }
    public float GetVelocityY(){
        return _rb.velocity.y;
    }
    public bool Grounded()
    {
        return Physics2D.OverlapBox(GroundCheckPoint.position,GroundCheckSize, 0, _whatIsGround);
    }
    public bool Walled(){
        return Physics2D.OverlapBox(WallCheckPoint.position, WallCheckSize,0, _whatIsGround);
    }    
    public void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce);
    }
    void StopWallJump(){
        _isWallJump = false;
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
        return _playerInput.Player.Jump.WasPerformedThisFrame();
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
        if(transform.localScale.x == -1) _rb.AddForce(new Vector2(-200f, 200f));
        else _rb.AddForce(new Vector2(200f, 200f));
        yield return new WaitForSeconds(0.5f);
        _takedamage = false;
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GroundCheckPoint.position, GroundCheckSize);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(WallCheckPoint.position, WallCheckSize);
    }
}
