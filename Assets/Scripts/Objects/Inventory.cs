using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemStack> itemStacks;
    public IEnumerable<ItemStack> ItemStacks => itemStacks;

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
    /// <param name="toAdd"></param>
    /// <returns>The actual number of items added</returns>
    public int Add(ItemStack toAdd)
    {
        itemStacks.Add(new ItemStack()
        {
            Item = toAdd.Item,
            Quantity = toAdd.Quantity
        });
        return toAdd.Quantity;
    }

    /// <summary>
    /// Removes the specified items from the inventory
    /// </summary>
    /// <param name="toRemove"></param>
    /// <returns>The actual number of items removed</returns>
    public int Remove(ItemStack toRemove)
    {
        int remaining = toRemove.Quantity;
        List<ItemStack> oldStacks = new List<ItemStack>(itemStacks);
        foreach (ItemStack stack in oldStacks)
        {
            if (stack.Item == toRemove.Item)
            {
                if (stack.Quantity > remaining)
                {
                    stack.Quantity -= remaining;
                }
                else
                {
                    remaining -= stack.Quantity;
                    itemStacks.Remove(stack);
                }

                if (remaining == 0)
                {
                    return toRemove.Quantity;
                }
            }
        }
        return toRemove.Quantity - remaining;
    }

    public void DropAll(Vector3 position)
    {
        foreach (ItemStack stack in itemStacks)
        {
            stack.Item.SpawnPile(stack.Quantity, position, transform.rotation);
        }
        itemStacks.Clear();
    }
}
