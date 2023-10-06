using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private InputManager _input;
    
    void Start()
    {
        _input = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.interactPressed)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
