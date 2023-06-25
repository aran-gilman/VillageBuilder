using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Events/Camera Move")]
public class CameraMoveEvents : InputEvents
{
    [SerializeField]
    private FloatGameEvent rotateCameraEvent;
    [SerializeField]
    private float cameraRotateSensitivity = 50.0f;

    [SerializeField]
    private FloatGameEvent pitchCameraEvent;
    [SerializeField]
    private float cameraPitchSensitivity = 50.0f;

    [SerializeField]
    private Vector2GameEvent moveCameraEvent;
    [SerializeField]
    private float cameraMoveSensitivity = 1.0f;

    private InputActionMap playerActionMap;
    private InputAction rotateCameraAction;
    private InputAction moveCameraAction;

    public override void Enable()
    {
        playerActionMap.Enable();

        rotateCameraAction.performed += OnRotateCamera;
        rotateCameraAction.canceled += OnRotateCamera;

        moveCameraAction.performed += OnMoveCamera;
        moveCameraAction.canceled += OnMoveCamera;
    }

    public override void Disable()
    {
        rotateCameraAction.performed -= OnRotateCamera;
        rotateCameraAction.canceled -= OnRotateCamera;

        moveCameraAction.performed -= OnMoveCamera;
        moveCameraAction.canceled -= OnMoveCamera;
    }

    private void OnRotateCamera(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        rotateCameraEvent.Raise(value.x * cameraRotateSensitivity);
        pitchCameraEvent.Raise(value.y * cameraPitchSensitivity);
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
