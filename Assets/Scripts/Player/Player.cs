using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player is mainly just the camera controller
// This scripts handles and helps with all interactions between all player scripts
public class Player : MonoBehaviour
{
    public Camera cam { get; private set; }

    // Singleton
    public static Player instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("More than 1 Player. Destroying this. Name: " + name);
            return;
        }

        cam = Camera.main;

        print(Camera.allCameras);
    }

    private void Update()
    {
        cam = Camera.main;

    }
}
