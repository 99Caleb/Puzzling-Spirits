using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    private AudioSource _audioSource;
    private bool isAfter;
    private bool isMain;

    private void Awake()
    {
        // Check if an instance of the MusicManager already exists
        if (_instance == null)
        {
            // If no instance exists, this is the first one
            _instance = this;

            // Make this object persistent between scenes
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // An instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main_menu")
        {
            _audioSource.volume = 0;
            isAfter = true;
            isMain = true;

        }
        else
        {
            _audioSource.volume = 1;
            isMain = false;
        }
        
        if(isAfter && !isMain)
        {
            ResetAudio();
            isAfter = false;
        }
    }

    // Function to reset the audio playback to the beginning
    public void ResetAudio()
    {
        _audioSource.Stop();
        _audioSource.time = 0f; // Set the playback time to the start
        _audioSource.Play();
    }
}