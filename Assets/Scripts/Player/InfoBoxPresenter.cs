using UnityEngine;
using UnityEngine.UI;

public class InfoBoxPresenter : MonoBehaviour
{
    [SerializeField]
    private Text nameElement;

    [SerializeField]
    private Text shortDescriptionElement;

    public void Show(GameObject toDisplay)
    {
        IInfoBoxModel model = toDisplay.GetComponent<IInfoBoxModel>();
        if (model == null)
        {
            return;
        }
        nameElement.text = model.DisplayName;
        shortDescriptionElement.text = model.ShortDescription;
        gameObject.SetActive(true);
    }
}
