using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SupplyDesignation))]
public class BuildDesignation : JobDesignation
{
    [SerializeField]
    private Inventory itemSource;

    [SerializeField]
    private Blueprint blueprint;

    [SerializeField]
    private SpawnGameEvent spawnBuildingEvent;

    private SupplyDesignation supplyDesignation;

    public override bool CanCreateJobs()
    {
        foreach (ItemStack stack in blueprint.SupplyList.Items)
        {
            if (itemSource.Count(stack.Item) < stack.Quantity)
            {
                return false;
            }
        }
        return true;
    }

    protected override List<Job> CreateJobs()
    {
        return new List<Job>()
        {
            new BuildJob(this, blueprint.FinishedPrefab, spawnBuildingEvent)
        };
    }

    private void Awake()
    {
        supplyDesignation = GetComponent<SupplyDesignation>();
    }

    private void OnEnable()
    {
        supplyDesignation.OnAllJobsCompleted.AddListener(DispatchJob);
    }

    private void OnDisable()
    {
        supplyDesignation.OnAllJobsCompleted.RemoveListener(DispatchJob);
    }
}
