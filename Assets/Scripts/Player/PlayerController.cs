using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

// Mainly camera controls since the player is the camera
public class PlayerController : MonoBehaviour
{
    private PlayerControlsManager controls;
    private Camera cam;

    [SerializeField] float cameraPanSpeed = 1f;
    [SerializeField] float cameraZoomSpeed = 1f;
    [SerializeField] Vector2 cameraZoomRange;

    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerControlsManager.instance;

        Player player = GetComponent<Player>();
        cam = player.cam;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Camera Controls:
        // - Right mouse & middle mouse hold to move camera (DONE)
        // - Move mouse to edge of screen to move camera 
        // - WASD to move camera

        // ===== Pan camera ======
        // Pan camera using RMB and MMB
        Vector2 panCameraDelta = controls.PanCameraDelta * cameraPanSpeed;

        // Pan camera by moving mouse to edge of screen
        // Only use this if there are no other panning buttons pressed.
        if (panCameraDelta.sqrMagnitude == 0)
        {
            // Amount of pixels the mouse needs to be away from the edge
            const float sensitivity = 10; 

            // Check if the mouse is on the edges of screen
            // For X
            if (controls.MouseScreenPosition.x < sensitivity)
                panCameraDelta.x -= cameraPanSpeed;
            else if (controls.MouseScreenPosition.x > Screen.width - sensitivity)
                panCameraDelta.x += cameraPanSpeed;

            // For Y
            if (controls.MouseScreenPosition.y < sensitivity)
                panCameraDelta.y -= cameraPanSpeed;
            else if (controls.MouseScreenPosition.y > Screen.height - sensitivity)
                panCameraDelta.y += cameraPanSpeed;
        }

        // Increase move distance based on the zoom amount
        transform.position += (Vector3)panCameraDelta * cam.orthographicSize;

        // ===== Zoom Camera =====
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + controls.ZoomCameraDelta * cameraZoomSpeed, cameraZoomRange.x, cameraZoomRange.y); 

        // Not working that well so comment it out
        # region Smooth Zoom
        //// Speed of camera: Depends on the camera speed & how close the camera is to the min and max range.
        //float rawZoomCameraDelta = controls.ZoomCameraDelta;
        //float orthoSize = Player.instance.cam.orthographicSize;
        //// Calculate the zoom distance left to min / max in the direction that its moving towards.
        //float zoomRangeLeft;
        //if (rawZoomCameraDelta == 0)
        //    return;
        //// If zooming out,
        //else if (rawZoomCameraDelta < 0)
        //    zoomRangeLeft = orthoSize - cameraZoomRange.x;
        //else
        //    zoomRangeLeft = cameraZoomRange.y - orthoSize;
        //print("Zoom left: " + zoomRangeLeft);
        //// Normalise it
        //// Don't make it reduce the speed too much (0.25 of max speed)
        //float zoomRangePercentage = Mathf.Max(zoomRangeLeft / (cameraZoomRange.y - cameraZoomRange.x), 0.5f);
        //print(zoomRangePercentage * 100f + "%");
        //float zoomCameraDelta = rawZoomCameraDelta * Mathf.Pow(zoomRangePercentage, 4) * cameraSpeed;

        //Player.instance.cam.orthographicSize = Mathf.Clamp(orthoSize + zoomCameraDelta, cameraZoomRange.x, cameraZoomRange.y);
        #endregion
    }
}
