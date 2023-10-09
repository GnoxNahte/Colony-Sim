using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    public PlayerControls playerControls;

    // Fields to easily get the inputs
    [field: SerializeField] public Vector2 PanCameraDelta { get; private set; }
    [field: SerializeField] public float ZoomCameraDelta { get; private set; }
    
    public Vector2 MouseScreenPosition { get { return Mouse.current.position.ReadValue(); } }
    public Vector2 MouseViewportPosition { get { return Mouse.current.position.ReadValue() / screenSize; } }

    // Input Actions
    private InputAction panCamera;
    private InputAction zoomCamera;

    private Vector2Int screenSize;

    public static PlayerControlsManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("More than 1 PlayerControlManager. Destroying this. Name: " + name);
            return;
        }

        playerControls = new PlayerControls();

        panCamera = playerControls.InGame.PanCamera;
        zoomCamera = playerControls.InGame.ZoomCamera;
    }

    private void OnEnable()
    {
        panCamera.Enable();
        zoomCamera.Enable();

        screenSize = new Vector2Int(Screen.width, Screen.height);
    }

    private void OnDisable()
    {
        panCamera.Disable();
        zoomCamera.Disable();
    }

    private void Update()
    {
        // TODO Settings for invert pan and zoom
        PanCameraDelta = panCamera.ReadValue<Vector2>();
        ZoomCameraDelta = -zoomCamera.ReadValue<float>();
    }
}
