using UnityEngine;

public class NearestItemSource : IProvider<Inventory>
{
    public IProvider<Vector3> Position { get; private set; }
    public IProvider<Item> Item { get; private set; }

    public NearestItemSource(IProvider<Vector3> position, IProvider<Item> item)
    {
        Position = position;
        Item = item;
    }

    public Inventory Get()
    {
        Item item = Item.Get();
        Vector3 position = Position.Get();
        if (item == null)
        {
            return null;
        }
        RetrieveItemTarget[] allTargets = Object.FindObjectsOfType<RetrieveItemTarget>();
        RetrieveItemTarget nearest = null;
        float nearestDistanceSqr = Mathf.Infinity;
        foreach (RetrieveItemTarget target in allTargets)
        {
            if (target.Inventory.Count(item) > 0)
            {
                Vector3 diff = target.transform.position - position;
                float distanceSqr = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
                if (distanceSqr < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distanceSqr;
                    nearest = target;
                }
            }
        }
        if (nearest != null)
        {
            return nearest.Inventory;
        }
        return null;
    }
}
