using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectionScript : MonoBehaviour
{
    public bool playerEntity;
    public bool nextToPlayerEntity;
    public int controlledInt;
    public inControl _inControl;
    
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
