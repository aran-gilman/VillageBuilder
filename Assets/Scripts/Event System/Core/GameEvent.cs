using System;
using UnityEngine;

public abstract class GameEvent<TEventArg> : ScriptableObject
{
    public event EventHandler<TEventArg> OnGameEvent;

    public void Raise(TEventArg arg)
    {
        EventHandler<TEventArg> handler = OnGameEvent;
        if (handler != null)
        {
            handler(this, arg);
        }
    }
}