using Unity.AI.Navigation;
using UnityEngine;

public class TerrainBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultObject;

    [SerializeField]
    private Vector3Int mapSize;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private NavMeshSurface navMeshSurface;

    public void UpdateNavMesh()
    {
        if (navMeshSurface.navMeshData == null)
        {
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            //navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            navMeshSurface.BuildNavMesh();
        }
    }

    private void Awake()
    {
        BuildTerrain();
    }

    private void BuildTerrain()
    {
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int z = 0; z < mapSize.z; z++)
            {
                for (int x = 0; x < mapSize.x; x++)
                {
                    GameObject go = Instantiate(defaultObject, new Vector3(x * grid.cellSize.x, y * grid.cellSize.y, z * grid.cellSize.z), Quaternion.identity);
                    go.transform.parent = transform;
                }
            }
        }
        UpdateNavMesh();
    }
}
