using UnityEngine;

[CreateAssetMenu]
public class Blueprint : ScriptableObject
{
    [SerializeField]
    private SupplyList supplyList;
    public SupplyList SupplyList => supplyList;

    [SerializeField]
    private GameObject constructionPrefab;
    public GameObject ConstructionPrefab => constructionPrefab;
}
