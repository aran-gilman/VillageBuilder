using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildJob : Job
{
    public SpawnGameEvent BuildEvent { get; private set; }
    public GameObject Prefab { get; private set; }

    public BuildJob(BuildDesignation source, GameObject prefab, SpawnGameEvent buildEvent)
    {
        Owner = source;
        BuildEvent = buildEvent;
        Prefab = prefab;
        DisplayName = $"Build {source.name}";
    }

    public override bool CanPerformWith(JobRunner actor)
    {
        return true;
    }

    public override CompositeCommand CreateCommand(JobRunner actor)
    {
        IEnumerable<ICommand> commands = new List<ICommand>()
        {
            new ApproachCommand(actor.NavMeshAgent, new TransformProvider<JobDesignation>(new ConstProvider<JobDesignation>(Owner))),
            new EventCommand<SpawnGameEvent, SpawnGameEvent.Args>(BuildEvent, new SpawnGameEvent.Args()
            {
                ToSpawn = Prefab,
                Position = Owner.transform.position,
                Rotation = Owner.transform.rotation
            })
        };
        return new CompositeCommand(commands);
    }

    public override ValidationResult IsValid()
    {
        return Owner == null ? ValidationResult.Impossible : ValidationResult.Valid;
    }
}
