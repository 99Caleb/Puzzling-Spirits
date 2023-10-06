using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class WSAnimation : MonoBehaviour
{
    public connectionScript connection;
    public inControl inControl;
    private Animator _animator;
    private InputManager _input;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    public LayerMask whoOn;
    public GameObject stewart;
    private bool _destroyedAlready = false;
    private bool inZappy = false;
    public door door;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
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

        if (whoIsUp())
        {
            _animator.SetBool("whoIsUp", true);
        }
        else
        {
            _animator.SetBool("whoIsUp", false);
        }

        if (inZappy)
        {
            if (door.doorPowered)
            {
                _animator.SetBool("zappyzappy", true);
            }
        }
        else
        {
            _animator.SetBool("zappyzappy", false);
        }
    }
    private bool whoAmOn()
    { Vector2 size = new Vector2(0.4f, 0.1f);
        Vector2 castOrigin = transform.position - new Vector3(0f, 0.1f, 0f);
        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.down, 0f, whoOn);
        return hit.collider != null; }
    
    private bool whoIsUp()
    { Vector2 size = new Vector2(0.4f, 0.1f);
        Vector2 castOrigin = transform.position + new Vector3(0f, 1.05f, 0f);
        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.down, 0f, whoOn);
        return hit.collider != null; }
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("FireSpirit")  && !_destroyedAlready)
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            _rigidbody.isKinematic = true;
            _destroyedAlready = true;
            _animator.Play("Extuingished");
            inControl.numbersToSkip.Add(connection.controlledInt);
            StartCoroutine(DestroyFireAfterWait());
        }
        if (coll.gameObject.CompareTag("Bush")  && !_destroyedAlready)
        {
            _destroyedAlready = true;
            _animator.Play("Bush reaction");
            StartCoroutine(GoIntoPlant());
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("DoorTrigger"))
        {
            inZappy = true;
            _animator.SetBool("zappy", true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("DoorTrigger"))
        {
            inZappy = false;
            _animator.SetBool("zappy", false);
        }
    }

    IEnumerator DestroyFireAfterWait()
    {
        GameObject StewartClone = Instantiate(stewart);
        var position = transform.position;
        StewartClone.transform.position = new Vector3(position.x, position.y,position.z + 1);
        Debug.Log("America Explain");
        yield return new WaitForSeconds(.75f);
        Destroy(gameObject);
    }
    
    IEnumerator GoIntoPlant()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        _rigidbody.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(.75f);
        Destroy(gameObject);
    }

    
}