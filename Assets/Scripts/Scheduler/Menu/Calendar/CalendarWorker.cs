using Domain.Scheduler;
using Scheduler;
using Shared;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CalendarWorker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private HintPlane hintPlanePrefab;
    [SerializeField] private MyUnityTimer timerPrefab;
    [SerializeField] private double showHintInSeconds = 0.5;

    private HintPlane _hintPlane;
    private MyUnityTimer _timer;

    private Transform previouseParentTransform;
    private int previouseSiblingIndex;

    private Worker _workerData;

    public Image WorkerImage { get; private set; }
    public Canvas ParentCanvas { get; set; }

    public Worker WorkerData
    {
        get => _workerData;
        set
        {
            _workerData = value;
            WorkerImage.sprite = _workerData == null ? null : ImageServerMock.LoadImage(_workerData.PathToIcon);
        }
    }

    public RectTransform RectTransform { get; set; }

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        WorkerImage = GetComponent<Image>();
        _hintPlane = Instantiate(hintPlanePrefab, transform);
        _timer = Instantiate(timerPrefab, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        _timer.OnFinish.RemoveAllListeners();
        _timer.OnFinish.AddListener(() => ShowHint(eventData.position));
        _timer.StartTimer(showHintInSeconds);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_timer.IsRunning) _timer.StopTimer();
        else
        {
            transform.SetParent(previouseParentTransform);
            transform.SetSiblingIndex(previouseSiblingIndex);
            _hintPlane.Hide();
        }
    }

    private void ShowHint(Vector2 pointerPosition)
    {
        previouseParentTransform = transform.parent;
        previouseSiblingIndex = transform.GetSiblingIndex();
        transform.SetParent(ParentCanvas.transform);
        transform.SetAsLastSibling();

        _hintPlane.Show(
            $"{WorkerData.Name}\nЭффективность: {WorkerData.EfficiencyCoeff:F1}",
            pointerPosition);
    }
}