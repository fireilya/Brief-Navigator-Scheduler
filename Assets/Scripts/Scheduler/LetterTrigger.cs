using UnityEngine;
using UnityEngine.EventSystems;

public class LetterTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject task; 

    void Start()
    {
        if (task != null)
        {
            task.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (task != null)
        {
            task.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (task != null)
        {
            task.SetActive(false);
        }
    }
}
