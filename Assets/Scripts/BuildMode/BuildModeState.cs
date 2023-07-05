using UnityEngine;

[CreateAssetMenu(menuName = "Build Mode/Build Mode State")]
public class BuildModeState : ScriptableObject
{
    public Blueprint SelectedBlueprint { get; set; }
}
