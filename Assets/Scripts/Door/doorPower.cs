using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class doorPower : MonoBehaviour
{
    public int waterSpirits = 0;
    public int waterSpiritsNeeded = 1;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("WaterSpirit"))
        {
            waterSpirits++;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("WaterSpirit"))
        {
            waterSpirits--;
        }
    }
}
