using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemStack> itemStacks;
    public IReadOnlyList<ItemStack> ItemStacks => itemStacks;

    [SerializeField]
    private UnityEvent<ItemStack> onAdd;
    public UnityEvent<ItemStack> OnAdd => onAdd;

    [SerializeField]
    private UnityEvent<ItemStack> onRemove;
    public UnityEvent<ItemStack> OnRemove => onRemove;

    [SerializeField]
    private UnityEvent onEmpty;
    public UnityEvent OnEmpty => onEmpty;

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
        bool foundStack = false;
        foreach (ItemStack stack in ItemStacks)
        {
            if (stack.Item == item)
            {
                stack.Quantity += quantity;
                foundStack = true;
                break;
            }
        }
        if (!foundStack)
        {
            itemStacks.Add(new ItemStack()
            {
                Item = item,
                Quantity = quantity
            });
        }
        onAdd.Invoke(new ItemStack()
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
                    remaining = 0;
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
                    onRemove.Invoke(new ItemStack()
                    {
                        Item = item,
                        Quantity = quantity
                    });
                    return quantity;
                }
            }
        }
        int removed = quantity - remaining;
        onRemove.Invoke(new ItemStack()
        {
            Item = item,
            Quantity = quantity
        });
        return removed;
    }
    public int Remove(ItemStack toRemove) => Remove(toRemove.Item, toRemove.Quantity);

    public int RemoveAll(Item item)
    {
        int quantity = 0;
        List<ItemStack> oldStacks = new List<ItemStack>(itemStacks);
        foreach (ItemStack stack in oldStacks)
        {
            if (stack.Item == item)
            {
                quantity += stack.Quantity;
                itemStacks.Remove(stack);
            }
        }
        onRemove.Invoke(new ItemStack()
        {
            Item = item,
            Quantity = quantity
        });
        if (itemStacks.Count == 0)
        {
            onEmpty.Invoke();
        }
        return quantity;
    }

    public void DropAll(Transform position)
    {
        foreach (ItemStack stack in itemStacks)
        {
            stack.Item.SpawnPile(stack.Quantity, position.position, position.rotation);
        }
        itemStacks.Clear();
    }

    public void Drop(Item item, Transform position)
    {
        int quantity = RemoveAll(item);
        if (quantity > 0)
        {
            item.SpawnPile(quantity, position.position, position.rotation);
        }
    }
}
