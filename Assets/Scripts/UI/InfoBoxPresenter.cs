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

    public void Show(GameObject toDisplay)
    {
        IInfoBoxModel model = toDisplay.GetComponent<IInfoBoxModel>();
        if (model == null)
        {
            return;
        }
        displayedObject = toDisplay;
        nameElement.text = model.DisplayName;
        shortDescriptionElement.text = model.ShortDescription;
        gameObject.SetActive(true);
    }

    private void OnObjectDestroyed(object sender, GameObject destroyedObject)
    {
        if (destroyedObject == displayedObject)
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
