using Scheduler;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Test : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private HintPlane hintPlanePrefab;
    [SerializeField] private MyUnityTimer timerPrefab;
    [SerializeField] private double showHintInSeconds = 0.5f;

    private HintPlane _hintPlane;
    private MyUnityTimer _timer;

    void Awake()
    {
        _hintPlane = Instantiate(hintPlanePrefab, transform);
        _timer = Instantiate(timerPrefab, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _timer.OnFinish.RemoveAllListeners();
        _timer.OnFinish.AddListener(() => ShowHint(eventData.position));
        _timer.StartTimer(showHintInSeconds);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _timer.StopTimer();
        _hintPlane.Hide();
    }

    private void ShowHint(Vector2 pointerPosition)
    {
        _hintPlane.Show("Это тестовый объект. Я надеюсь, он будет корректно работать", pointerPosition);
    }
}
