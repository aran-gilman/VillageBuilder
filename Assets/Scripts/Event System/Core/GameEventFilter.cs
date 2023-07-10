using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventFilter<TEvent, TArg> : MonoBehaviour where TEvent: GameEvent<TArg>
{
    [SerializeField]
    private TEvent gameEvent;

    [SerializeField]
    private UnityEvent<TArg> callback;

    protected abstract bool AllowEvent(TArg arg);

    private void HandleEvent(object sender, TArg arg)
    {
        if (AllowEvent(arg))
        {
            callback.Invoke(arg);
        }
    }

    private void OnEnable()
    {
        gameEvent.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        gameEvent.OnGameEvent -= HandleEvent;
    }
}
