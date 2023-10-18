using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public inControl inControl;
        public connectionScript connectionScript;
        public arrowFollow arrowFollow;
    
        [Header("Movement")] public float moveSpeed = 5f;
        public float jumpSpeed = 5.1f;
        private Vector2 _desiredVelocity;
        [Header("isGrounded")] 
        public LayerMask whatIsGround;
        public LayerMask canStayOn;
        public LayerMask isWall;
        [Header("Components")] 
        private Rigidbody2D _rigidbody2D;
        private InputManager _input;
        
        private void Start()
        { _rigidbody2D = GetComponent<Rigidbody2D>();
            _input = GetComponent<InputManager>(); }
        private void Update()
        {
            if (inControl.controlled == connectionScript.controlledInt)
            {
                _rigidbody2D.mass = 50;
                _desiredVelocity = _rigidbody2D.velocity;
                inControl.playerSpeedX = _desiredVelocity.x /3.75f;
                arrowFollow.arrowPositionX = _rigidbody2D.position.x;
                arrowFollow.arrowPositionY = _rigidbody2D.position.y + 1.5f;
                if (_input.jumpPressed)
                {
                    Jump(); 
                }
                
                if (IsPlayerGrounded())
                {
                    inControl.canSwitch = true;
                }
                else
                {
                    inControl.canSwitch = false;
                }
                _rigidbody2D.velocity = _desiredVelocity;
            }
            else
            {
                _rigidbody2D.mass = 1;
            }

            if (StayOnCharacter() && inControl.controlled != connectionScript.controlledInt)
            {
                _rigidbody2D.velocity = new Vector2(inControl.playerSpeedX * moveSpeed, _rigidbody2D.velocity.y);
                if (_input.jumpPressed)
                {
                    Jump(); 
                }
            }

            if (StayOnCharacter())
            {
                connectionScript.nextToPlayerEntity = true;
            }
            else
            {
                connectionScript.nextToPlayerEntity = false;
            }
            
            if ((!(StayOnCharacter() && inControl.controlled != connectionScript.controlledInt) ||
                 (inControl.controlled != connectionScript.controlledInt)))
            {
                _rigidbody2D.velocityX *= .994f;
            }
        }

        private void FixedUpdate()
        {
            if (inControl.controlled == connectionScript.controlledInt && !DetectWall())
            {
                _rigidbody2D.velocity = new Vector2(_input.moveDirection.x * moveSpeed, _rigidbody2D.velocity.y);
            }
            else if (inControl.controlled == connectionScript.controlledInt)
            {
                if (transform.localScale.x > 0)
                {
                    _rigidbody2D.velocity = new Vector2(_input.left * moveSpeed, _rigidbody2D.velocity.y);
                }
                else
                {
                    _rigidbody2D.velocity = new Vector2(_input.right * moveSpeed, _rigidbody2D.velocity.y);
                }
            }
        }

        private bool IsPlayerGrounded()
        { Vector2 size = new Vector2(0.4f, 0.01f);
            Vector2 castOrigin = transform.position - new Vector3(0f, 0.2f, 0f);
            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.down, 0f, whatIsGround);
            return hit.collider != null; }

        private bool StayOnCharacter()
        {
            Vector2 size = new Vector2(0.2f, 0.01f);
            Vector2 castOrigin = transform.position - new Vector3(0f, 0.8f, 0f);
            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.down, 0f, canStayOn);
            if (hit.collider != null)
            {
                connectionScript connection = hit.collider.GetComponent<connectionScript>();
                if (connection != null)
                {
                    if (connection.playerEntity || connection.nextToPlayerEntity)
                    {
                        return true;
                    }

                    if(inControl.controlled != connectionScript.controlledInt)
                    {
                        Rigidbody2D otherRB = hit.collider.GetComponent<Rigidbody2D>();
                        _rigidbody2D.velocityX = otherRB.velocity.x;
                        return false;
                    }

                    return false;
                }

                RSconnectionScript RSconnection = hit.collider.GetComponent<RSconnectionScript>();
                if (RSconnection != null)
                {
                    if (RSconnection.playerEntity || RSconnection.nextToPlayerEntity)
                    {
                        return true;
                    }
                    if(inControl.controlled != connectionScript.controlledInt)
                    {
                        Rigidbody2D otherRB = hit.collider.GetComponent<Rigidbody2D>();
                        _rigidbody2D.velocityX = otherRB.velocity.x;
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool DetectWall()
        { Vector2 size = new Vector2(0.01f, 1.7f);
            Vector2 castOrigin = transform.position + new Vector3(transform.localScale.x * 0.55f, 0.9f, 0f);
            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.right, 0f, isWall);
        
            return hit.collider != null; }

        private void Jump()
        {
            if (IsPlayerGrounded())
            {
                _desiredVelocity.y = jumpSpeed;
            }
        }
    }
}