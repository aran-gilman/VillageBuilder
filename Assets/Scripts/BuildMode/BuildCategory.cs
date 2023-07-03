using UnityEngine;

[CreateAssetMenu(menuName = "Build Mode/Category")]
public class BuildCategory : ScriptableObject
{
    [SerializeField]
    private string displayName;
    public string DisplayName => displayName;
}
