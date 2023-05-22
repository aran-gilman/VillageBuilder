using UnityEngine;

/// <summary>
/// Wrapper for Object.Destroy for use with UnityEvent callbacks
/// </summary>
public class SelfDestruct : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
