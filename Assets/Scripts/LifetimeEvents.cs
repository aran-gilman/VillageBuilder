using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifetimeEvents : MonoBehaviour
{
    // We use a different naming scheme for public vs private because 'OnEnable' etc are already taken
    // We can't just rename the private because 'enabled' is also taken
    // This is a compromise that keeps the names close to their Unity message equivalents

    [SerializeField]
    private UnityEvent onStart;
    public UnityEvent Started => onStart;

    [SerializeField]
    private UnityEvent onDestroy;
    public UnityEvent Destroyed => onDestroy;

    [SerializeField]
    private UnityEvent onEnable;
    public UnityEvent Enabled => onEnable;

    [SerializeField]
    private UnityEvent onDisable;
    public UnityEvent Disabled => onDisable;

    private void Start()
    {
        onStart.Invoke();
    }

    private void OnDestroy()
    {
        onDestroy.Invoke();
    }

    private void OnEnable()
    {
        onEnable.Invoke();
    }

    private void OnDisable()
    {
        onDisable.Invoke();
    }
}
