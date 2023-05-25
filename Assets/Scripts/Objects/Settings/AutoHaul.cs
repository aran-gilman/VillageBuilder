using UnityEngine;

[RequireComponent(typeof(HaulDesignation))]
public class AutoHaul : ObjectSetting
{
    private HaulDesignation haulDesignation;

    private void OnInventoryAdd(Item item, int quantity)
    {
        haulDesignation.DispatchJob();
    }

    private void Awake()
    {
        haulDesignation = GetComponent<HaulDesignation>();
    }

    private void OnEnable()
    {
        var source = haulDesignation.Source;
        var inv = source.Inventory;
        inv.OnAdd.AddListener(OnInventoryAdd);
        haulDesignation.DispatchJob();
    }

    private void OnDisable()
    {
        haulDesignation.Source.Inventory.OnAdd.RemoveListener(OnInventoryAdd);
    }
}
