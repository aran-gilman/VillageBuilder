using System.Collections.Generic;
using UnityEngine;

public class RetrieveItemTarget : ActorTarget
{
    [SerializeField]
    private Inventory inventory;
    public Inventory Inventory => inventory;
}
