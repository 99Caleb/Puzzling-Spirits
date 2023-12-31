using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class FSAnimation : MonoBehaviour
{
    public connectionScript connection;
    public inControl inControl;
    private Animator _animator;
    private InputManager _input;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    public LayerMask whoOn;
    private Collider2D _collider;
    private bool _destroyedAlready = false;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
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
            if (inControl.controlled == connection.controlledInt)
            {
                transform.localScale = new Vector3(_input.moveDirection.x, 1, 1);
                _animator.SetBool("isWalking", true);
            }
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

        if (inControl.controlled == connection.controlledInt)
        {
            _sprite.color = new Color (1, 1, 1, 1);
        }
        else
        {
            _sprite.color = new Color (.8f, .8f,.8f, 1f);
        }

        if (whoAmOn())
        {
            _animator.SetBool("onEntity", true);
        }
        else
        {
            _animator.SetBool("onEntity", false);
        }
        
    }
    private bool whoAmOn()
    { Vector2 size = new Vector2(0.4f, 0.1f);
        Vector2 castOrigin = transform.position - new Vector3(0f, 0.1f, 0f);
        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.down, 0f, whoOn);
        return hit.collider != null; }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if ((coll.gameObject.CompareTag("Bush") || coll.gameObject.CompareTag("Character")) && !_destroyedAlready)
        {
            _destroyedAlready = true;
            _animator.Play("Extinguished");
            inControl.numbersToSkip.Add(connection.controlledInt);
            StartCoroutine(DestroyFireAfterWait());
        }
        if ((coll.gameObject.CompareTag("WaterSpirit") && !_destroyedAlready))
        {
            _destroyedAlready = true;
            _animator.Play("Extinguished");
            inControl.numbersToSkip.Add(connection.controlledInt);
            StartCoroutine(DestroyFireAfterWaitWater());
        }
    }
    IEnumerator DestroyFireAfterWait()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        _rigidbody.isKinematic = true;
        inControl.controlled = 0;
        inControl._current = 0;
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);
    }
    
    IEnumerator DestroyFireAfterWaitWater()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        _rigidbody.isKinematic = true;
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);
    }
}