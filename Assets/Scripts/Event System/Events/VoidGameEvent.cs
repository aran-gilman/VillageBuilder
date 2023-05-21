using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/Void")]
public class VoidGameEvent : GameEvent<object>
{
    public void Raise()
    {
        Raise(null);
    }
}
