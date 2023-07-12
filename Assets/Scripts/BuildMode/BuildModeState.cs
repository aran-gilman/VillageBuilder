using UnityEngine;

[CreateAssetMenu(menuName = "Build Mode/Build Mode State")]
public class BuildModeState : ScriptableObject
{
    public Blueprint SelectedBlueprint { get; set; }

    public void PlaceSelectedBlueprint(Vector3 position, Quaternion rotation)
    {
        if (SelectedBlueprint == null)
        {
            return;
        }
        GameObject prefab = SelectedBlueprint.ConstructionPrefab == null ? SelectedBlueprint.FinishedPrefab : SelectedBlueprint.ConstructionPrefab;
        Instantiate(prefab, position, rotation);
    }
}
