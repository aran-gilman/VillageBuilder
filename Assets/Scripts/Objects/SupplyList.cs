using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SupplyList : ScriptableObject
{
    [SerializeField]
    private List<ItemStack> items;
    public IEnumerable<ItemStack> Items => items;
}
