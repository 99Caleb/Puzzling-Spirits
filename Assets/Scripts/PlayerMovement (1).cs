using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 12f;
    private Vector2 _desiredVelocity;
    
    [Header("isGrounded")]
    public LayerMask whatIsGround;
    
    [Header("Components")]
    private Rigidbody2D _rigidbody2D;
    private InputManager _input;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputManager>();
    }

    private void Update()
    {
        _desiredVelocity = _rigidbody2D.velocity;
        
        if (_input.jumpPressed)
        {
            Jump();
        }
        
        if (_input.jumpReleased && _desiredVelocity.y > 0f)
        {
            //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.2f);
            _desiredVelocity.y *= 0.5f;
        }

        _rigidbody2D.velocity = _desiredVelocity;
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_input.moveDirection.x * moveSpeed, _rigidbody2D.velocity.y);
    }
    private bool IsPlayerGrounded()
    {
        // Define the size and other parameters of the box cast
        Vector2 size = new Vector2(0.9f, 0.1f); // Adjust the size as needed
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, size, 0f, Vector2.down, 1.1f, whatIsGround);

        // Check if the box cast hit something
        return hit.collider != null;
    }

    private void Jump()
    {
        if (IsPlayerGrounded())
        {
            //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
            _desiredVelocity.y = jumpSpeed;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}