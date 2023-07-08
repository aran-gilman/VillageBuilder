using UnityEngine;

[CreateAssetMenu(menuName = "Services/Immigration")]
public class ImmigrationService : ScriptableObject
{
    [SerializeField]
    private IntGameEvent villagersAvailableEvent;

    public void GenerateNewAvailableVillagers()
    {
        villagersAvailableEvent.Raise(Random.Range(0, 5));
    }
}
