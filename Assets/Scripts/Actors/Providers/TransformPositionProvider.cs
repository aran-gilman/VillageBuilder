using UnityEngine;

public class TransformPositionProvider<T> : IProvider<Vector3> where T : MonoBehaviour
{
    public IProvider<T> TargetObject { get; private set; }

    public TransformPositionProvider(IProvider<T> component)
    {
        TargetObject = component;
    }

    public Vector3 Get()
    {
        T component = TargetObject.Get();
        if (component == null)
        {
            return Vector3.zero;
        }
        return component.transform.position;
    }
}
