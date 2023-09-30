using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private InputManager _input;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (_input.moveDirection.x == 0)
        {
            _animator.SetBool("isWalking", false);
        }
        else
        {
            transform.localScale = new Vector3(_input.moveDirection.x, 1, 1);
            _animator.SetBool("isWalking", true);
        }
        
        if (-0.1f <= _rigidbody.velocity.y && _rigidbody.velocity.y <= 0.1f)
        {
            _animator.SetInteger("jumpAnimation", 0);
        }
        else if(_rigidbody.velocity.y > 0.1f)
        {
            _animator.SetInteger("jumpAnimation", 1);
        }
        else
        {
            _animator.SetInteger("jumpAnimation", 2);
        }
            
    }
}