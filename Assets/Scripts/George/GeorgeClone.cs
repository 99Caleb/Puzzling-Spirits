using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeorgeClone : MonoBehaviour
{
    private Animator _animator;
    public LayerMask isBush;
    public GeorgeMechanic georgeMechanic;
    public inControl inControl;
    public string animationWS;
    public GameObject WaterBushReaction;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        georgeMechanic = GameObject.Find("George").GetComponent<GeorgeMechanic>();
    }

    private void Update()
    {
        if (georgeMechanic.startAnimation && (!isNotOnTop()))
        {
            _animator.Play("Grow");
        }
        if (georgeMechanic.isOnFire == true)
        {
            _animator.Play("Burn");
            StartCoroutine(DestroyBushAfterWait());
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        GeorgeMechanic spawnCopy = GameObject.Find("George").GetComponent<GeorgeMechanic>();
        inControl inControl = GameObject.Find("Player").GetComponent<inControl>(); 
        if (coll.gameObject.CompareTag("WaterSpirit") && spawnCopy.numberOfClones <= spawnCopy.maxClones && !spawnCopy.isWet && !georgeMechanic.isGrowing )
        {
            WaterBushReaction = Instantiate(WaterBushReaction);
            var position = coll.transform.position;
            WaterBushReaction.transform.position = new Vector3(position.x, position.y);
            Destroy(coll.gameObject);
            georgeMechanic.isGrowing = true;
            connectionScript connectionScript = coll.gameObject.GetComponent<connectionScript>();
            inControl.numbersToSkip.Add(connectionScript.controlledInt);
            inControl.controlled = 0;
            inControl._current = 0;
            spawnCopy.isWet = true;
            StartCoroutine(WaterAnimation());
        }
        else if (coll.gameObject.CompareTag("FireSpirit"))
        {
            georgeMechanic.isOnFire = true;
        }
    }
    private bool isNotOnTop()
    { Vector2 size = new Vector2(0.4f, 0.1f);
        Vector2 castOrigin = transform.position + new Vector3(0f, 1.2f, 0f);
        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.down, 0f, isBush);
        return hit.collider != null; }
    
    IEnumerator DestroyBushAfterWait()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator WaterAnimation()
    {
        yield return new WaitForSeconds (.6f);
        WaterBushReaction.transform.position = new Vector2(-100, -100);
    }
}
