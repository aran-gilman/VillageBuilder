using UnityEngine;
using UnityEngine.UI;

public class InfoBoxPresenter : MonoBehaviour
{
    [SerializeField]
    private Text nameElement;

    [SerializeField]
    private Text shortDescriptionElement;

    [SerializeField]
    private GameObjectGameEvent destroyEvent;

    private GameObject displayedObject;

    public GameObject DisplayedObject
    { 
        get => displayedObject;
        set
        {
            if (!value.TryGetComponent<IInfoBoxModel>(out var model))
            {
                displayedObject = null;
                return;
            }
            displayedObject = value;
            nameElement.text = model.DisplayName;
            shortDescriptionElement.text = model.ShortDescription;
        }
    }

    private void OnObjectDestroyed(object sender, GameObject destroyedObject)
    {
        if (destroyedObject == DisplayedObject)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        destroyEvent.OnGameEvent += OnObjectDestroyed;
    }

    private void OnDisable()
    {
        destroyEvent.OnGameEvent -= OnObjectDestroyed;
    }
}
