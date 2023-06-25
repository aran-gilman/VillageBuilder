using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[CreateAssetMenu(menuName = "Input Events/World Object Select")]
public class WorldObjectSelectEvents : InputEvents
{
    [SerializeField]
    private GameObjectGameEvent selectWorldObjectEvent;

    private InputAction clickAction;
    private InputAction pointAction;

    private InputActionMap playerActionMap;
    private Camera mainCamera;
    private InputSystemUIInputModule uiInputModule;

    public override void Enable()
    {
        mainCamera = Camera.main;
        playerActionMap.Enable();
        clickAction.performed += OnPlayerClick;
    }

    public override void Disable()
    {
        clickAction.performed -= OnPlayerClick;
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
        clickAction = playerActionMap.FindAction("Click");
        pointAction = playerActionMap.FindAction("Point");
    }
}
