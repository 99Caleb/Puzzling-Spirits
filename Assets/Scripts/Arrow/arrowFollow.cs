using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowFollow : MonoBehaviour
{
    public float arrowPositionX;
    public float arrowPositionY;
    private Transform _transform;
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = new Vector3(arrowPositionX, arrowPositionY, -5);
    }
}
