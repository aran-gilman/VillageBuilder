using UnityEngine;

[CreateAssetMenu(menuName = "Game Event/Void")]
public class VoidGameEvent : GameEvent<object>
{
    public void Raise()
    {
        Raise(null);
    }
}
