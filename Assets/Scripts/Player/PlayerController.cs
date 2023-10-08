using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerControlsManager controls;

    [SerializeField] float cameraSpeed = 5f;
    [SerializeField] Vector2 cameraZoomRange;

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

        // Pan camera
        transform.position = transform.position + (Vector3)(controls.PanCameraDelta * cameraSpeed);

        // Calculate zoom camera delta
        float zoomRangeLeft = Mathf.Min(transform.position.z - cameraZoomRange.x, cameraZoomRange.y - transform.position.z);
        // Multiply the delta based on camera speed and how close the camera is to the range.
        // The close it is to the min and max range, slow the camera more
        float zoomCameraDelta = -controls.ZoomCameraDelta * cameraSpeed * Mathf.Pow(zoomRangeLeft, 2);
        transform.position = new Vector3(transform.position.x, transform.position.y,
            Mathf.Clamp(transform.position.z + zoomCameraDelta, cameraZoomRange.x, cameraZoomRange.y));
        print($"A: {controls.ZoomCameraDelta} B: {zoomCameraDelta} C: {Mathf.Clamp(transform.position.z + zoomCameraDelta, cameraZoomRange.x, cameraZoomRange.y)}");
    }
}
