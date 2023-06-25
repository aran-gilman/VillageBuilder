using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField]
    private InputEvents inputEvents;

    private void OnEnable()
    {
        inputEvents.Enable();
    }

    private void OnDisable()
    {
        inputEvents.Disable();
    }
}
