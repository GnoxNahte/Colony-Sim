using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerControlsManager controls;

    [SerializeField] float cameraSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerControlsManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Camera Controls:
        // - Right mouse & middle mouse hold to move camera
        // - Move mouse to edge of screen to move camera
        // - WASD to move camera

        transform.position = transform.position + (Vector3)(controls.PanCameraDelta * cameraSpeed);
    }
}
