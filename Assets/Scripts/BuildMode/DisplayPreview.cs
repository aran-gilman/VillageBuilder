using UnityEngine;

public class DisplayPreview : MonoBehaviour
{
    [SerializeField]
    private BuildModeState buildModeState;

    private GameObject previewObject;

    private void OnEnable()
    {
        // This should happen in OnDisable, but verify again in case of strangeness
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
        previewObject = Instantiate(buildModeState.SelectedBlueprint.PreviewModel, transform);
    }

    private void OnDisable()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
    }
}
