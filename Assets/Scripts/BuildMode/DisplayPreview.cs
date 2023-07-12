using System.Collections.Generic;
using UnityEngine;

public class DisplayPreview : MonoBehaviour
{
    [SerializeField]
    private BuildModeState buildModeState;

    [SerializeField]
    private Material placementAllowedMaterial;

    [SerializeField]
    private Material placementForbiddenMaterial;

    [SerializeField]
    private LayerMask blocksPlacementLayers;

    [SerializeField]
    private float rotationSpeedMultiplier = 10.0f;

    private GameObject placementAllowedPreview;
    private GameObject placementForbiddenPreview;

    private List<Collider> currentlyColliding = new List<Collider>();

    private float rotationAmount = 0;

    public void MaybePlaceObject(Vector3 position)
    {
        if (currentlyColliding.Count == 0)
        {
            buildModeState.PlaceSelectedBlueprint(position, transform.rotation);
        }
    }

    public void SetRotation(float rotation)
    {
        rotationAmount = rotation * rotationSpeedMultiplier;
    }

    private GameObject CreatePreviewObject(GameObject prefab, Material material)
    {
        GameObject go = Instantiate(prefab, transform);
        foreach (MeshRenderer renderer in go.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.materials = CreateMaterialArray(renderer.materials.Length, material);
        }
        return go;
    }

    private Material[] CreateMaterialArray(int size, Material material)
    {
        Material[] materials = new Material[size];
        for (int i = 0; i < size; i++)
        {
            materials[i] = material;
        }
        return materials;
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationAmount * Time.fixedDeltaTime, Space.World);
    }

    private void OnEnable()
    {
        // This should happen in OnDisable, but verify again in case of strangeness
        if (placementAllowedPreview != null)
        {
            Destroy(placementAllowedPreview);
        }
        if (placementForbiddenPreview != null)
        {
            Destroy(placementForbiddenPreview);
        }

        placementAllowedPreview = CreatePreviewObject(buildModeState.SelectedBlueprint.ConstructionPrefab, placementAllowedMaterial);
        placementForbiddenPreview = CreatePreviewObject(buildModeState.SelectedBlueprint.ConstructionPrefab, placementForbiddenMaterial);
        placementForbiddenPreview.SetActive(false);
    }

    private void OnDisable()
    {
        if (placementAllowedPreview != null)
        {
            Destroy(placementAllowedPreview);
        }
        if (placementForbiddenPreview != null)
        {
            Destroy(placementForbiddenPreview);
        }
        currentlyColliding.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool blocksPlacement = blocksPlacementLayers == (blocksPlacementLayers | (1 << other.gameObject.layer));
        if (blocksPlacement)
        {
            if (!currentlyColliding.Contains(other))
            {
                currentlyColliding.Add(other);
            }
            placementAllowedPreview.gameObject.SetActive(false);
            placementForbiddenPreview.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentlyColliding.Remove(other);
        if (currentlyColliding.Count == 0)
        {
            placementAllowedPreview.gameObject.SetActive(true);
            placementForbiddenPreview.gameObject.SetActive(false);
        }
    }
}
