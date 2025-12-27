using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class LetterTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas Letter;

    private void Start()
    {
        Letter.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Letter.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        Letter.gameObject.SetActive(false);
    }
}
