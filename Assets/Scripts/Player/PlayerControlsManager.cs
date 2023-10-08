using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    public PlayerControls playerControls;

    // Input Actions
    private InputAction panCamera;

    // Fields to easily get the inputs
    [field: SerializeField] public Vector2 PanCameraDelta { get; private set; }

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
    }

    private void OnEnable()
    {
        panCamera.Enable();
    }

    private void OnDisable()
    {
        panCamera.Disable();
    }

    private void Update()
    {
        PanCameraDelta = panCamera.ReadValue<Vector2>();
    }
}
