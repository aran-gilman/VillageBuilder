using UnityEngine;

[CreateAssetMenu]
public class GameMode : ScriptableObject
{
    private static GameMode current;
    public static GameMode Current
    {
        get => current;
        set
        {
            if (current == value)
            {
                return;
            }
            if (current != null)
            {
                for (int i = 0; i < current.inputEvents.Length; i++)
                {
                    current.inputEvents[i].Disable();
                }
                if (current.onExitMode != null)
                {
                    current.onExitMode.Raise();
                }
            }
            current = value;
            if (current != null)
            {
                for (int i = 0; i < current.inputEvents.Length; i++)
                {
                    current.inputEvents[i].Enable();
                }
                if (current.onEnterMode != null)
                {
                    current.onEnterMode.Raise();
                }
            }
        }
    }

    [SerializeField]
    private InputEvents[] inputEvents;

    [SerializeField]
    private VoidGameEvent onEnterMode;
    [SerializeField]
    private VoidGameEvent onExitMode;

    public void SetAsCurrent()
    {
        Current = this;
    }
}
