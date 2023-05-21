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

    private InputAction rotateCameraAction;

    private void OnRotateCamera(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        Debug.Log(value);
        rotateCameraEvent.Raise(value.x);
        pitchCameraEvent.Raise(value.y);
    }

    private void Awake()
    {
        asset.FindActionMap("Player").Enable();
        rotateCameraAction = asset.FindAction("Player/Rotate");
    }

    private void OnEnable()
    {
        rotateCameraAction.performed += OnRotateCamera;
        rotateCameraAction.canceled += OnRotateCamera;
    }

    private void OnDisable()
    {
        rotateCameraAction.performed -= OnRotateCamera;
        rotateCameraAction.canceled -= OnRotateCamera;
    }
}
