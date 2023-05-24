using UnityEngine;

public class RetrieveItemTarget : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    public Inventory Inventory => inventory;
}
