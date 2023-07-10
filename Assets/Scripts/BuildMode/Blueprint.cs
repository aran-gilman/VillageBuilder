using UnityEngine;

[CreateAssetMenu(menuName = "Build Mode/Blueprint")]
public class Blueprint : ScriptableObject
{
    [SerializeField]
    private SupplyList supplyList;
    public SupplyList SupplyList => supplyList;

    [SerializeField]
    private GameObject previewModel;
    public GameObject PreviewModel => previewModel;

    [SerializeField]
    private GameObject constructionPrefab;
    public GameObject ConstructionPrefab => constructionPrefab;

    [SerializeField]
    private GameObject finishedPrefab;
    public GameObject FinishedPrefab => finishedPrefab;
}
