using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private Animator _animator;
    public doorPower doorPower;
    public bool doorPowered;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (doorPower.waterSpirits >= doorPower.waterSpiritsNeeded)
        {
            doorPowered = true;
            _animator.SetBool("powered", true);
        }
        else
        {
            doorPowered = false;
            _animator.SetBool("powered", false);
        }
    }
}
