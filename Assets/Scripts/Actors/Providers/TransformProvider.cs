using UnityEngine;

public class TransformProvider<T> : IProvider<Transform> where T : MonoBehaviour
{
    public IProvider<T> TargetObject { get; private set; }

    public TransformProvider(IProvider<T> component)
    {
        TargetObject = component;
    }

    public Transform Get()
    {
        T component = TargetObject.Get();
        if (component == null)
        {
            return null;
        }
        return component.transform;
    }
}
