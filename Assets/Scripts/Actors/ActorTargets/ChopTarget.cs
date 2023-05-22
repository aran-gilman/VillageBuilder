using UnityEngine;

public class ChopTarget : ActorTarget
{
    [SerializeField]
    private Inventory inventory;
    public Inventory Inventory => inventory;
}
