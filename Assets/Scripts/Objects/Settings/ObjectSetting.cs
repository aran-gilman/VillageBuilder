using UnityEngine;

public abstract class ObjectSetting : MonoBehaviour
{
    [SerializeField]
    protected VoidGameEvent refreshSettings;

    public abstract void InstantiateControl(Transform parent);
    public virtual bool IsEnabled() => true;
}
