using UnityEngine;

public class BasicInfoBox : MonoBehaviour, IInfoBoxModel
{
    [SerializeField]
    private string displayName;
    public string DisplayName => displayName;

    [SerializeField]
    private string shortDescription;
    public string ShortDescription => shortDescription;

}
