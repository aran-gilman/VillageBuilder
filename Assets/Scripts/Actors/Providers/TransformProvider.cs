using UnityEngine;

// We can't use the ComponentProvider because Transform doesn't inherit from MonoBehaviour
public class TransformProvider<TIn> : IProvider<Transform> where TIn : MonoBehaviour
{
    public IProvider<TIn> TargetObject { get; private set; }

    public TransformProvider(IProvider<TIn> component)
    {
        TargetObject = component;
    }

    public Transform Get()
    {
        TIn input = TargetObject.Get();
        if (input == null)
        {
            return null;
        }
        return input.transform;
    }
}
