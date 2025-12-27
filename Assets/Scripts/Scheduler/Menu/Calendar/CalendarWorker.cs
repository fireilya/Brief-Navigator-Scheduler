using Domain.Scheduler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalendarWorker : MonoBehaviour, IPointerEnterHandler
{
    public Image WorkerImage { get; set; }
    public Worker WorkerData { get; set; }
    
    public RectTransform RectTransform { get; set; }

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        WorkerImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        Debug.Log($"Работник: {WorkerData.Name}\nЭффективность: {WorkerData.EfficiencyCoeff:F1}");
    }
}
