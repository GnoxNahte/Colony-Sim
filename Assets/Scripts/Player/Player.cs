using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player is mainly just the camera controller
// This scripts handles and helps with all interactions between all player scripts
public class Player : MonoBehaviour
{
    public Camera cam { get; private set; }

    private void Awake()
    {
        #if UNITY_EDITOR
        if (FindObjectsByType<Player>(FindObjectsSortMode.None).Length > 1)
            Debug.LogError("More than 1 player");
        #endif
        cam = Camera.main;
    }
}
