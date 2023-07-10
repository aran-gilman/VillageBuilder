using UnityEngine;

public class DisplayPreview : MonoBehaviour
{
    [SerializeField]
    private BuildModeState buildModeState;

    [SerializeField]
    private Material previewMaterial;

    private GameObject previewObject;

    private void OnEnable()
    {
        // This should happen in OnDisable, but verify again in case of strangeness
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
        previewObject = Instantiate(buildModeState.SelectedBlueprint.PreviewModel, transform);
        foreach (MeshRenderer renderer in previewObject.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.materials = CreateMaterialArray(renderer.materials.Length);
        }
    }

    private void OnDisable()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
    }

    private Material[] CreateMaterialArray(int size)
    {
        Material[] materials = new Material[size];
        for (int i = 0; i < size; i++)
        {
            materials[i] = previewMaterial;
        }
        return materials;
    }
}
