using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSconnectionScript : MonoBehaviour
{
    public bool playerEntity;
    public bool nextToPlayerEntity;
    public int controlledInt;
    public inControl _inControl;

    private void Start()
    {
        _inControl = GameObject.Find("Player").GetComponent<inControl>();
        if (controlledInt == 0)
        {
            Debug.Log("Peanut");
            _inControl.numberControlled++;
            controlledInt = _inControl.numberControlled - 1;
            _inControl.countingNumbers.Add(controlledInt);
            _inControl._current = _inControl.numberControlled;
            _inControl.controlled = _inControl.numberControlled - 1;
        }
    }

    private void Update()
    {
        if (_inControl.controlled == controlledInt)
        {
            playerEntity = true;
        }
        else
        {
            playerEntity = false;
        }
    }
}
