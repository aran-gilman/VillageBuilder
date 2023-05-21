using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class InputEvents : ScriptableObject
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

    private InputActionMap playerActionMap;
    private InputAction rotateCameraAction;
    private InputAction moveCameraAction;

    public void Enable()
    {
        playerActionMap.Enable();
        rotateCameraAction.performed += OnRotateCamera;
        rotateCameraAction.canceled += OnRotateCamera;

        moveCameraAction.performed += OnMoveCamera;
        moveCameraAction.canceled += OnMoveCamera;
    }

    public void Disable()
    {
        rotateCameraAction.performed -= OnRotateCamera;
        rotateCameraAction.canceled -= OnRotateCamera;

        moveCameraAction.performed -= OnMoveCamera;
        moveCameraAction.canceled -= OnMoveCamera;
    }

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

    private void OnEnable()
    {
        playerActionMap = asset.FindActionMap("Player");
        rotateCameraAction = playerActionMap.FindAction("Rotate");
        moveCameraAction = playerActionMap.FindAction("Move");
    }
}
