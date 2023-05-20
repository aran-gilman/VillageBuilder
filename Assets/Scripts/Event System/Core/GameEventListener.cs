using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListener<TGameEvent, TArg> : MonoBehaviour where TGameEvent : GameEvent<TArg>
{
    [SerializeField]
    private TGameEvent gameEvent;

    [SerializeField]
    private UnityEvent<TArg> handleGameEvent;

    private void HandleGameEvent(object sender, TArg arg)
    {
        handleGameEvent.Invoke(arg);
    }

    private void OnEnable()
    {
        gameEvent.OnGameEvent += HandleGameEvent;
    }

    private void OnDisable()
    {
        gameEvent.OnGameEvent -= HandleGameEvent;
    }
}
