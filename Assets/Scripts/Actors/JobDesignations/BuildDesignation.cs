using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDesignation : JobDesignation
{
    [SerializeField]
    private Inventory itemSource;

    [SerializeField]
    private SupplyDesignation supplyDesignation;

    [SerializeField]
    private GameObject finishedBuildingPrefab;

    public override bool CanCreateJobs()
    {
        // TODO: check inventory
        return false;
    }

    protected override List<Job> CreateJobs()
    {
        return new List<Job>();
    }
}
