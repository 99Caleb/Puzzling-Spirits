using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    public connectionScript connection;
    public inControl inControl;
    private Animator _animator;
    private InputManager _input;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    public LayerMask whoOn;
    public SceneController scene;
    public string nextLevelName;
    public door door;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        inControl = GameObject.Find("Player").GetComponent<inControl>();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (_input.reset)
        {
            CoinUI.tempCoin = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
        else if (_rigidbody.velocity.y > 0.1f)
        {
            _animator.SetInteger("jumpAnimation", 1);
        }
        else
        {
            _animator.SetInteger("jumpAnimation", 2);
        }

        if (inControl.controlled == connection.controlledInt)
        {
            _sprite.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _sprite.color = new Color(.8f, .8f, .8f, 1f);
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
    {
        Vector2 size = new Vector2(0.4f, 0.1f);
        Vector2 castOrigin = transform.position - new Vector3(0f, 0.1f, 0f);
        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.down, 0f, whoOn);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("FireSpirit"))
        {
            _animator.Play("Death");
            inControl.numbersToSkip.Add(connection.controlledInt);
            StartCoroutine(fireReset());
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Door") && door.doorPowered == true)
        {
            //_animator.Play("Death");
            StartCoroutine(doorEnter());
        }
    }
    
    IEnumerator fireReset()
    {
        yield return new WaitForSeconds(1f);
        CoinUI.tempCoin = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    IEnumerator doorEnter()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(nextLevelName);
        CoinUI.totalScore = CoinUI.totalScore + CoinUI.tempCoin;
        CoinUI.tempCoin = 0;
        SceneManager.LoadScene(nextLevelName);
    }
}