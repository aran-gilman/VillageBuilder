using UnityEngine;

public class NearestItemWithProperty<TPropertyTable, TValue> : IProvider<Inventory> where TPropertyTable : PropertyTable<Item, TValue>
{
    public TPropertyTable PropertyTable { get; private set; }
    public IProvider<Transform> Position { get; private set; }

    private VariableProvider<Item> nearestItem = new VariableProvider<Item>();
    public IProvider<Item> NearestItem => nearestItem;

    private VariableProvider<TValue> nearestItemValue = new VariableProvider<TValue>();
    public IProvider<TValue> NearestItemValue => nearestItemValue;

    public NearestItemWithProperty(IProvider<Transform> position, TPropertyTable propertyTable)
    {
        PropertyTable = propertyTable;
        Position = position;
    }

    public Inventory Get()
    {
        Transform transform = Position.Get();
        if (transform == null)
        {
            return null;
        }
        RetrieveItemTarget[] allTargets = Object.FindObjectsOfType<RetrieveItemTarget>();
        RetrieveItemTarget nearest = null;
        float nearestDistanceSqr = Mathf.Infinity;
        foreach (RetrieveItemTarget target in allTargets)
        {
            foreach (Item item in PropertyTable.Entries.Keys)
            {
                if (target.Inventory.Count(item) > 0)
                {
                    Vector3 diff = target.transform.position - transform.position;
                    float distanceSqr = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
                    if (distanceSqr < nearestDistanceSqr)
                    {
                        nearestDistanceSqr = distanceSqr;
                        nearest = target;
                        nearestItem.Value = item;
                        nearestItemValue.Value = PropertyTable.Get(item);
                    }
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
