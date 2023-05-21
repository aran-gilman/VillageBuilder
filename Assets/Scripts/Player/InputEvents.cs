using UnityEngine;
using UnityEngine.InputSystem;

public class InputEvents : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset asset;

    [SerializeField]
    private FloatGameEvent rotateCameraEvent;
    [SerializeField]
    private FloatGameEvent pitchCameraEvent;

    [SerializeField]
    private Vector2GameEvent moveCameraEvent;
    [SerializeField]
    private float cameraMoveSensitivity = 1.0f;

    private InputAction rotateCameraAction;
    private InputAction moveCameraAction;

    private void OnRotateCamera(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        rotateCameraEvent.Raise(value.x);
        pitchCameraEvent.Raise(value.y);
    }

    private void OnMoveCamera(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        moveCameraEvent.Raise(value * cameraMoveSensitivity);
    }

    private void Awake()
    {
        InputActionMap playerActionMap = asset.FindActionMap("Player");
        playerActionMap.Enable();
        rotateCameraAction = playerActionMap.FindAction("Rotate");
        moveCameraAction = playerActionMap.FindAction("Move");
    }

    private void OnEnable()
    {
        rotateCameraAction.performed += OnRotateCamera;
        rotateCameraAction.canceled += OnRotateCamera;

        moveCameraAction.performed += OnMoveCamera;
        moveCameraAction.canceled += OnMoveCamera;
    }

    private void OnDisable()
    {
        rotateCameraAction.performed -= OnRotateCamera;
        rotateCameraAction.canceled -= OnRotateCamera;

        moveCameraAction.performed -= OnMoveCamera;
        moveCameraAction.canceled -= OnMoveCamera;
    }
}
