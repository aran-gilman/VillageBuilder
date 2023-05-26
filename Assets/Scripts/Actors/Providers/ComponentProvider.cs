using UnityEngine;

public class ComponentProvider<TIn, TOut> : IProvider<TOut> where TIn : MonoBehaviour where TOut : MonoBehaviour
{
    public IProvider<TIn> TargetObject { get; private set; }

    public ComponentProvider(IProvider<TIn> component)
    {
        TargetObject = component;
    }

    public TOut Get()
    {
        TIn input = TargetObject.Get();
        if (input == null)
        {
            return null;
        }
        return input.GetComponent<TOut>();
    }
}

public class ComponentProvider<TOut> : IProvider<TOut> where TOut : MonoBehaviour
{
    public IProvider<Transform> TargetObject { get; private set; }
    public ComponentProvider(IProvider<Transform> component)
    {
        TargetObject = component;
    }

    public TOut Get()
    {
        Transform input = TargetObject.Get();
        if (input == null)
        {
            return null;
        }
        return input.GetComponent<TOut>();
    }
}
