using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerBlock : MonoBehaviour
{
    public door door;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door.doorPowered == true)
        {
            _animator.SetBool("powered", true);
        }
        else
        {
            _animator.SetBool("powered", false);
        }
    }
}
