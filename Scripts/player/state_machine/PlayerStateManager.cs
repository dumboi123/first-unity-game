
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
public class PlayerStateManager : MonoBehaviour
{
//===================================================================================
    
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
    private Control _playerInput;
    private bool _isTouchingWall,_isWallSliding, _isWallJump;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpforce;
    [SerializeField] private LayerMask _checkBelow;
    [SerializeField] private LayerMask _checkSide;
    [SerializeField] private AudioSource _jump_sound;
    [NonSerialized] public float _input, _currentMoveInput;
    [NonSerialized] public bool _doublejump, _takedamage, _movePressed, _jumpPressed, _jumpPressing ;
    public enum MovementStates { idle, walk, jump, fall, wallslide };
    [NonSerialized] public MovementStates _animState;

    PlayerBaseState _currentState;
    PlayerStateFactory _states;

   //=================================================================================== 
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
    }
    private void FixedUpdate(){
        if(!_takedamage)
            OnMovement();
    }
    
    private void OnMovement(){
        if (_isWallSliding && _movePressed )
            _rb.velocity = new Vector2(_rb.velocity.x, -_wallSlideSpeed);
        if (_isWallJump){
            _rb.velocity = new Vector2(-_input*WallJumpForce.x, WallJumpForce.y);
            Invoke("StopWallJump",0.1f);
        }
        else 
            _rb.velocity = new Vector2(_speed*_input, _rb.velocity.y);
    }
    public void HandleAnimation(MovementStates states) => _anim.SetInteger("State",(int)states);



    public bool Grounded() => Physics2D.OverlapBox(GroundCheckPoint.position, GroundCheckSize, 0, _checkSide);
    public bool Walled() => Physics2D.OverlapBox(WallCheckPoint.position, WallCheckSize,0, _checkSide);
    public void Jump() => _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce);
    public void StopWallJump() => _isWallJump = false;
    public void DoubleJump()
    {
        _anim.SetTrigger("double_jump");
        _doublejump = false;
    }
    public bool JumpGetButton() => _playerInput.Player.Jump.IsPressed();
    public bool JumpGetButtonDown() => _playerInput.Player.Jump.WasPressedThisFrame();

    public float GetVelocityY() => _rb.velocity.y;
    private void MoveGetAxisRaw(InputAction.CallbackContext ctx){
        _movePressed = ctx.action.IsPressed();
        _currentMoveInput =ctx.ReadValue<Vector2>().x;
        if(_currentMoveInput !=0)
            transform.localScale = _currentMoveInput > 0 ? new Vector2(1,1) : new Vector2(-1,1);
    }



    public void SetDoubleJump(bool x) => _doublejump = x;
    public void SetSpeed(float x) => _speed = x;
    public void SetJumpforce(float x) =>_jumpforce = x;
    public void SetWallJumpForce(float x,float y)
    {
        WallJumpForce.x = x;
        WallJumpForce.y = y; 
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {   if(gameObject.tag == "Player")
        {
            GetComponent<Life>().Damaged(1); 
            StartCoroutine("KnockBack");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal;
        if (collision.gameObject.tag == "Enemy" && gameObject.tag == "Player")
        {
            if (DotTest(collisionNormal))
            {
                Jump();
                _doublejump = true;
                _currentState = _states.InSpace();
                _currentState.EnterState();
            }         
            else
            {
               GetComponent<Life>().Damaged(1); 
               StartCoroutine("KnockBack");
            }                
        }
    }    
    private bool DotTest(Vector2 collisionNor)
    {
        return Vector2.Dot(collisionNor, Vector2.up) > 0.25f;
    } 
    IEnumerator KnockBack()
    {
        _takedamage = true;
        _rb.AddForce(new Vector2(-transform.localScale.x*4, 5), ForceMode2D.Impulse);
        _doublejump = false;
        yield return new WaitForSeconds(0.5f);
        _rb.velocity = new Vector2(_rb.velocity.x,_rb.velocity.y);
        _takedamage = false;
        _doublejump = true;
    }


    // private void OnDrawGizmosSelected(){
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawCube(GroundCheckPoint.position, GroundCheckSize);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(WallCheckPoint.position, WallCheckSize);
    // }
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
