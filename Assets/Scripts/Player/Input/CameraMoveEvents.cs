using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

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

    [SerializeField]
    private GameObjectGameEvent selectWorldObjectEvent;

    private Camera mainCamera;
    private InputSystemUIInputModule uiInputModule;

    private InputActionMap playerActionMap;
    private InputAction rotateCameraAction;
    private InputAction moveCameraAction;
    private InputAction clickAction;

    private InputAction pointAction;

    public override void Enable()
    {
        mainCamera = Camera.main;

        playerActionMap.Enable();

        rotateCameraAction.performed += OnRotateCamera;
        rotateCameraAction.canceled += OnRotateCamera;

        moveCameraAction.performed += OnMoveCamera;
        moveCameraAction.canceled += OnMoveCamera;

        clickAction.performed += OnPlayerClick;
    }

    public override void Disable()
    {
        rotateCameraAction.performed -= OnRotateCamera;
        rotateCameraAction.canceled -= OnRotateCamera;

        moveCameraAction.performed -= OnMoveCamera;
        moveCameraAction.canceled -= OnMoveCamera;

        clickAction.performed -= OnPlayerClick;
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

    private void OnPlayerClick(InputAction.CallbackContext ctx)
    {
        if (IsUIClick())
        {
            return;
        }
        Vector2 screenPos = pointAction.ReadValue<Vector2>();
        if (Physics.Raycast(mainCamera.ScreenPointToRay(screenPos), out RaycastHit hit, Mathf.Infinity, -1, QueryTriggerInteraction.Collide))
        {
            selectWorldObjectEvent.Raise(hit.collider.gameObject);
        }
    }

    private bool IsUIClick()
    {
        // We can't fetch this in Enable() because it may not be fully initialized yet
        if (uiInputModule == null)
        {
            EventSystem eventSystem = EventSystem.current;
            uiInputModule = (InputSystemUIInputModule)eventSystem.currentInputModule;
        }
        RaycastResult result = uiInputModule.GetLastRaycastResult(Mouse.current.deviceId);
        return result.gameObject != null;
    }

    private void OnEnable()
    {
        playerActionMap = asset.FindActionMap("Player");
        rotateCameraAction = playerActionMap.FindAction("Rotate");
        moveCameraAction = playerActionMap.FindAction("Move");
        clickAction = playerActionMap.FindAction("Click");
        pointAction = playerActionMap.FindAction("Point");
    }
}
