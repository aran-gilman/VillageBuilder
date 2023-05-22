using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemStack> itemStacks;
    public IEnumerable<ItemStack> ItemStacks => itemStacks;

    [SerializeField]
    private UnityEvent onEmpty;

    public int Count(Item item)
    {
        int sum = 0;
        foreach(ItemStack stack in itemStacks)
        {
            if (stack.Item == item)
            {
                sum += stack.Quantity;
            }
        }
        return sum;
    }

    /// <summary>
    /// Adds the specified items to the inventory
    /// </summary>
    /// <returns>The actual number of items added</returns>
    public int Add(Item item, int quantity)
    {
        itemStacks.Add(new ItemStack()
        {
            Item = item,
            Quantity = quantity
        });
        return quantity;
    }

    public int Add(ItemStack toAdd) => Add(toAdd.Item, toAdd.Quantity);

    /// <summary>
    /// Removes the specified items from the inventory
    /// </summary>
    /// <returns>The actual number of items removed</returns>
    public int Remove(Item item, int quantity)
    {
        int remaining = quantity;
        List<ItemStack> oldStacks = new List<ItemStack>(itemStacks);
        foreach (ItemStack stack in oldStacks)
        {
            if (stack.Item == item)
            {
                if (stack.Quantity > remaining)
                {
                    stack.Quantity -= remaining;
                }
                else
                {
                    remaining -= stack.Quantity;
                    itemStacks.Remove(stack);
                    if (itemStacks.Count == 0)
                    {
                        onEmpty.Invoke();
                    }
                }

                if (remaining == 0)
                {
                    return quantity;
                }
            }
        }
        return quantity - remaining;
    }
    public int Remove(ItemStack toRemove) => Remove(toRemove.Item, toRemove.Quantity);

    public List<GameObject> DropAll(Vector3 position)
    {
        List<GameObject> itemPiles = new List<GameObject>();
        foreach (ItemStack stack in itemStacks)
        {
            itemPiles.Add(stack.Item.SpawnPile(stack.Quantity, position, transform.rotation));
        }
        itemStacks.Clear();
        return itemPiles;
    }
}
