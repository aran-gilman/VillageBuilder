using UnityEngine;

public class GameObjectLayerFilter : GameObjectGameEventFilter
{
    [SerializeField]
    private LayerMask layers;

    protected override bool AllowEvent(GameObject go)
    {
        bool allowed = layers == (layers | (1 << go.layer));
        return allowed;
    }
}
