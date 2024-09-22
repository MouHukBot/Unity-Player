
using System.Collections;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private const float RunSpeed = 7.5f;
    private float _movespeed = 5f;
    public float jumpForce = 7.5f;
    private float _currentSpeed;
    public Vector2 FlyVector;
    public int Jumps = 1;
    private bool canJump = true;
    private bool _isFacingRight = true;
    private TouchingDirrections touchingDirrections;
    public bool IsMoving = false;
    public bool IsRunning = false;
    private Vector2 moveInput;
    private AnimatorContoller animatorContoller;
    
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1,1);
            }
            _isFacingRight = value;
        }
    }
    private Vector2 movementDirection = Vector2.zero;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        touchingDirrections = GetComponent<TouchingDirrections>();
        animatorContoller = GetComponent<AnimatorContoller>();
        _currentSpeed = _movespeed;
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(movementDirection.x * _currentSpeed, rigidbody.velocity.y);
        rigidbody.AddForce(rigidbody.velocity);
        FlyVector.y = rigidbody.velocity.y;
        //Debug.Log(IsRunning);
        //Debug.Log(IsMoving);
        //Debug.Log(touchingDirrections.IsPlatform);
            }
    public void OnMove(InputAction.CallbackContext context)
    {
        Move(context);
        SetFacingDirrection(movementDirection);
        /*if(movementDirection.x!=0)
        {
            animatorContoller.Flip(movementDirection.x);
        }*/
        if(movementDirection != Vector2.zero)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }
    private void Move(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (touchingDirrections.IsPlatform)
        {
            StartCoroutine(DisableJumps(0.25f));
            Jumps = 1;
            //rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
        else if (!touchingDirrections.IsPlatform && Jumps > 0 && canJump)
        {
            StartCoroutine(DisableJumps(0.25f));
            //rigidbody.AddForce(2 * Vector2.up * jumpForce, ForceMode2D.Impulse);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            Jumps--;
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        IsRunning = context.ReadValueAsButton();
        if (IsRunning == true)
        {
            Run();
        }
        else if(IsRunning == false)
        {
            _currentSpeed = _movespeed;
        }
    }
    private void Run()
    {
        _currentSpeed = RunSpeed;
    }
    private IEnumerator DisableJumps(float time)
    {
        canJump = false;
        yield return new WaitForSeconds(time);
        canJump = true;
    }
    private void SetFacingDirrection(Vector2 dirrection)
    {
        if(dirrection.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (dirrection.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
}
