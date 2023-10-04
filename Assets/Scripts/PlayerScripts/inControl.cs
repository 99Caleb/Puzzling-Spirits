using UnityEngine;
using System.Collections.Generic;
public class inControl : MonoBehaviour
{
    public int controlled;
    public int numberControlled;
    public List<int> countingNumbers = new List<int>();
    public List<int> numbersToSkip;
    private InputManager _input;
    public int _current = 0;

    private void Start()
    {
        _input = GetComponent<InputManager>();
        GenerateCountingNumbers();
    }

    private void Update()
    {
        if (!_input.interactPressed) return;
        for (int i = 0; i < numbersToSkip.Count; i++)
        {
            skipTester();
        }
        if (_current >= numberControlled - 1)
        {
            _current = 0;
        }
        else
        {        
            _current++;
        }
        controlled = countingNumbers[_current];
    }

    private void GenerateCountingNumbers()
    {
        for (int i = 0; i < numberControlled; i++)
        {
            countingNumbers.Add(i);
        }
    }

    private void skipTester()
    {
        for (int i = 0; i < numbersToSkip.Count; i++)
        {
            if (_current == numbersToSkip[i] -1)
            {
                if (_current >= numberControlled - 1)
                {
                    _current = 0;
                    skipTester();
                }
                else
                {        
                    _current++;
                }
            }
        }
    }

}
