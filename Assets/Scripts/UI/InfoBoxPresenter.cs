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
        private set
        {
            if (value == null || !value.TryGetComponent<IInfoBoxModel>(out var model))
            {
                displayedObject = null;
                gameObject.SetActive(false);
                return;
            }
            displayedObject = value;
            nameElement.text = model.DisplayName;
            shortDescriptionElement.text = model.ShortDescription;
            gameObject.SetActive(true);
        }
    }

    public void ShowOrToggle(GameObject toDisplay)
    {
        if (toDisplay == DisplayedObject)
        {
            DisplayedObject = null;
        }
        else
        {
            DisplayedObject = toDisplay;
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
