using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    public void Spawn()
    {
        Spawn(1);
    }

    public void Spawn(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
