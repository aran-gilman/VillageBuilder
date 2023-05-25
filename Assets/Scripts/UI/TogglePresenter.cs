using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TogglePresenter : MonoBehaviour
{
    [SerializeField]
    private Text labelDisplay;

    [SerializeField]
    private Image imageDisplay;

    [SerializeField]
    private GameObject toggleDisplay;

    [SerializeField]
    private UnityEvent<bool> onClick;
    public UnityEvent<bool> OnClick => onClick;

    public bool IsOn
    {
        get => isOn;
        set
        {
            if (isOn != value)
            {
                isOn = value;
                if (toggleDisplay != null)
                {
                    toggleDisplay.SetActive(isOn);
                }
            }
        }
    }

    // We use a button rather than a Unity Toggle to allow more flexibility, particularly for validating
    private Button button;
    private bool isOn;

    // Explicit setter for use with programmatically registering a listener with a UnityEvent
    public void SetValue(bool newValue)
    {
        IsOn = newValue;
    }

    public void SetLabel(string text)
    {
        labelDisplay.text = text;
    }

    public void SetImage(Sprite sprite)
    {
        imageDisplay.sprite = sprite;
        imageDisplay.gameObject.SetActive(sprite != null);
    }

    private void HandleClick()
    {
        onClick.Invoke(IsOn);
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void OnEnable()
    {
        if (imageDisplay.sprite == null)
        {
            imageDisplay.gameObject.SetActive(false);
        }
    }
}
