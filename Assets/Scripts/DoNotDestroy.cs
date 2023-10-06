using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Check if an instance of the MusicManager already exists
        if (instance == null)
        {
            // If no instance exists, this is the first one
            instance = this;

            // Make this object persistent between scenes
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // An instance already exists, destroy this duplicate
            Destroy(this.gameObject);
        }
    }

    // The rest of your music-related code goes here
    // ...
}