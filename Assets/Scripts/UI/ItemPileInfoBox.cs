using UnityEngine;

public class ItemPileInfoBox : MonoBehaviour, IInfoBoxModel
{
    [SerializeField]
    private Inventory inventory;

    public string DisplayName
    {
        get
        {
            if (inventory.ItemStacks.Count == 1)
            {
                return "Pile of " + inventory.ItemStacks[0].Item.name;
            }
            return "Pile of Items";
        }
    }

    public string ShortDescription => "A pile of one or more items.";
}
