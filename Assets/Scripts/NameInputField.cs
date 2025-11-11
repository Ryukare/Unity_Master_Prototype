using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputField : MonoBehaviour
{
    private Image _background;
    private TMP_InputField _inputField;

    private void Awake()
    {
        _background = GetComponent<Image>();
        _inputField = GetComponent<TMP_InputField>();
        HideBackground("");
        _inputField.onSelect.AddListener(ShowBackground);
        _inputField.onDeselect.AddListener(HideBackground);
    }

    private void ShowBackground(string value)
    {
        var color = _background.color;
        color.a = 255;
        _background.color = color;
    }

    private void HideBackground(string value)
    {
        var color = _background.color;
        color.a = 0;
        _background.color = color;
    }
}
