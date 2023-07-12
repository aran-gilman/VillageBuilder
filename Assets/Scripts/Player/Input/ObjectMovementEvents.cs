using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Events/Object Movement")]
public class ObjectMovementEvents : InputEvents
{
    [SerializeField]
    private FloatGameEvent rotateObjectEvent;

    private InputActionMap playerActionMap;
    private InputAction rotateAction;

    public override void Enable()
    {
        playerActionMap.Enable();
        rotateAction.started += OnRotateObject;
        rotateAction.canceled += OnRotateObject;
    }

    public override void Disable()
    {
        rotateAction.started -= OnRotateObject;
        rotateAction.canceled -= OnRotateObject;
    }

    private void OnRotateObject(InputAction.CallbackContext context)
    {
        rotateObjectEvent.Raise(context.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerActionMap = asset.FindActionMap("Player");
        rotateAction = playerActionMap.FindAction("RotateObject");
    }
}
