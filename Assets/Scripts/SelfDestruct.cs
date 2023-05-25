using UnityEngine;
using UnityEngine.Events;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private GameObjectGameEvent destroyEvent;

    [SerializeField]
    private UnityEvent onBeingDestroyed;

    private void HandleDestroyEvent(object sender, GameObject toDestroy)
    {
        if (toDestroy == gameObject)
        {
            onBeingDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        destroyEvent.OnGameEvent += HandleDestroyEvent;
    }

    private void OnDestroy()
    {
        destroyEvent.OnGameEvent -= HandleDestroyEvent;
    }
}
