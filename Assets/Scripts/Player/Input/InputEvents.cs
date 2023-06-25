using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputEvents : ScriptableObject
{
    [SerializeField]
    protected InputActionAsset asset;

    public abstract void Enable();
    public abstract void Disable();
}
