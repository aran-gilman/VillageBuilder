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
    private UnityEvent onTrue;
    public UnityEvent OnTrue => onTrue;

    [SerializeField]
    private UnityEvent onFalse;
    public UnityEvent OnFalse => onFalse;

    // We don't call OnTrue or OnFalse automatically because the main use case for TogglePresenter
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

    public void SetLabel(string text)
    {
        labelDisplay.text = text;
    }

    public void SetImage(Sprite sprite)
    {
        imageDisplay.sprite = sprite;
        imageDisplay.gameObject.SetActive(sprite != null);
    }

    private void OnClick()
    {
        IsOn = !IsOn;
        if (IsOn)
        {
            onTrue.Invoke();
        }
        else
        {
            onFalse.Invoke();
        }
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        if (imageDisplay.sprite == null)
        {
            imageDisplay.gameObject.SetActive(false);
        }
    }
}
