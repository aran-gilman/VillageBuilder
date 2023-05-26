using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/Spawn Event")]
public class SpawnGameEvent : GameEvent<SpawnGameEvent.Args>
{
    public class Args
    {
        public GameObject ToSpawn;
        public Vector3 Position;
        public Quaternion Rotation;
    }

    private void OnEnable()
    {
        OnGameEvent += HandleSpawnEvent;
    }

    private void OnDisable()
    {
        OnGameEvent -= HandleSpawnEvent;
    }

    private void HandleSpawnEvent(object sender, Args args)
    {
        Instantiate(args.ToSpawn, args.Position, args.Rotation);
    }
}
