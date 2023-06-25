using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField]
    private CameraMoveEvents inputEvents;

    private void OnEnable()
    {
        inputEvents.Enable();
    }

    private void OnDisable()
    {
        inputEvents.Disable();
    }
}
