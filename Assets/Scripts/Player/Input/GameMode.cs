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
            if (current != null)
            {
                for (int i = 0; i < current.inputEvents.Length; i++)
                {
                    current.inputEvents[i].Disable();
                }
            }
            current = value;
            if (current != null)
            {
                for (int i = 0; i < current.inputEvents.Length; i++)
                {
                    current.inputEvents[i].Enable();
                }
            }
        }
    }

    [SerializeField]
    private InputEvents[] inputEvents;
}
