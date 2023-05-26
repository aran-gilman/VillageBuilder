using UnityEngine;

public class GameObjectPositionProvider : IProvider<Vector3>
{
    public IProvider<GameObject> TargetObject { get; private set; }

    public GameObjectPositionProvider(IProvider<GameObject> gameObject)
    {
        TargetObject = gameObject;
    }

    public Vector3 Get()
    {
        GameObject go = TargetObject.Get();
        if (go == null)
        {
            return Vector3.zero;
        }
        return go.transform.position;
    }
}
