using UnityEngine;
using UnityEngine.Events;

public class WorldObjectLifecycleManager : MonoBehaviour
{
    [SerializeField]
    private GameObjectGameEvent destructionEvent;

    [SerializeField]
    private UnityEvent onBeingDestroyed;

    public void DoDestroy()
    {
        Destroy(gameObject);
        onBeingDestroyed.Invoke();
    }

    private void OnDestroy()
    {
        if (destructionEvent != null)
        {
            destructionEvent.Raise(gameObject);
        }
    }
}
