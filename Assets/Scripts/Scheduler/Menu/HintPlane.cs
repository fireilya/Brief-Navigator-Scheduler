using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HintPlane : MonoBehaviour
{
    [SerializeField] private TMP_Text hintTextField;
    private Vector2 defaultSizeDelta;
    private RectTransform rectTransform;
    private HorizontalOrVerticalLayoutGroup layoutGroup;
    private RectTransform hintTextRectTransform;
    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        layoutGroup = GetComponent<VerticalLayoutGroup>();
        hintTextRectTransform = hintTextField.GetComponent<RectTransform>();
        gameObject.SetActive(false);
        defaultSizeDelta = rectTransform.sizeDelta;
    }

    public void Show(string hintText, Vector2 pointerPosition)
    {
        gameObject.SetActive(true);
        rectTransform.sizeDelta = defaultSizeDelta;
        hintTextField.SetText(hintText);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        rectTransform.sizeDelta = new Vector2(
            hintTextRectTransform.sizeDelta.x + layoutGroup.padding.right + layoutGroup.padding.left, 
            hintTextRectTransform.sizeDelta.y + layoutGroup.padding.top + layoutGroup.padding.right);
        var pivotPosition = rectTransform.pivot;
        if (pointerPosition.x + rectTransform.sizeDelta.x > Screen.width) pivotPosition.x = 1;
        if (pointerPosition.y + rectTransform.sizeDelta.y > Screen.height) pivotPosition.y = 1;
        rectTransform.pivot = pivotPosition;
        rectTransform.position = pointerPosition;
    }
    
    public void Hide() => gameObject.SetActive(false);
}
