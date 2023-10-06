using UnityEngine;

public class CrossFade : MonoBehaviour
{
    public Animator transition;
    public PlayerAnimation playerAnimation;

    void Start()
    {
        Debug.Log(playerAnimation);
        Debug.Log(transition);

    }

    void Update()
    {
        // Check if the endSequence variable is true in the PlayerAnimation script
        if (playerAnimation != null && playerAnimation.endSequence == true)
        {
            // Set the "EndOfScene" parameter in the transition Animator to true
            transition.SetTrigger("Start");
            Debug.Log("Booga");
        }
    }
}