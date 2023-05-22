using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField]
    private GameObject pilePrefab;

    public GameObject SpawnPile(int quantity, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = Instantiate(pilePrefab, position, rotation);
        Inventory inventory = gameObject.GetComponent<Inventory>();
        inventory.Add(new ItemStack
        {
            Item = this,
            Quantity = quantity
        });
        return gameObject;
    }
}
