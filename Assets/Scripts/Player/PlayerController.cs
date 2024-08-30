
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody; 
    public float movespeed = 5f;
    public float jumpForce = 2f;
    private bool _isFacingRight = true;
    private TouchingDirrections touchingDirrections;
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
    // Start is called before the first frame update
    /*void Start()
    {
        Debug.Log(transform.position);
        Debug.LogError(transform.rotation);
    }*/

    // Update is called once per frame
    private Vector2 movementDirection = Vector2.zero;
    /*private void Update()
    {
        float inputx = Input.GetAxis("Horizontal");
        Vector2 movementDirection = new Vector2(inputx, 0);
        transform.Translate(movementDirection * movespeed * Time.deltaTime);
        Debug.Log(inputx);

    }*/
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        touchingDirrections = GetComponent<TouchingDirrections>();
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(movementDirection.x * movespeed, rigidbody.velocity.y);
        //rigidbody.AddForce(rigidbody.velocity);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
        SetFacingDirrection(movementDirection);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (touchingDirrections.IsPlatform)
        {
            //rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
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
