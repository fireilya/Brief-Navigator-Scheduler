using System;
using System.Collections.Generic;
using System.Text;
using Scheduler;
using Scheduler.Bases;
using Scheduler.Interfaces;
using Scheduler.Menu.Calendar;
using Scheduler.Wrappers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScheduledCellsMark : DragableBase, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public UnityEvent<ScheduledCellsMark> onUnmarked = new();

    [SerializeField] private HintPlane hintPlanePrefab;
    [SerializeField] private MyUnityTimer timerPrefab;
    [SerializeField] private double showHintInSeconds = 0.5f;

    private HintPlane _hintPlane;
    private MyUnityTimer _timer;
    public Image ScheduledCellImage { get; private set; }
    public float CommonAlpha { get; set; }
    public float HoveredAlpha { get; set; }

    public WorkerRow ScheduledAtRow { get; private set; }
    public List<CalendarCell> ScheduledCells { get; private set; } = new();

    public RiskInfluenceMark RiskMark { get; private set; } = new();

    public int PrecalculatedProgress => SubtaskWrapper.PrecalculatedProgress;
    public CalendarCell StartCell => ScheduledCells.Count > 0 ? ScheduledCells[0] : null;
    public int StartsFromIndex => StartCell ? StartCell.Index : -1;
    public bool IsHasScheduledCells => ScheduledCells.Count > 0;
    public int SubtaskOrder => TaskPlane.SubtaskWrapper.SubtaskOrder;
    public int ScheduledOnHours => ScheduledCells.Count;

    private float MarkEfficiencyCoeff => ScheduledAtRow.WorkerEfficiency * RiskMark.InfluenceCoeff;
    public bool IsLastScheduledCell(int cellIndex) => ScheduledCells[^1].Index == cellIndex;
    public SubtaskWrapper SubtaskWrapper => TaskPlane.SubtaskWrapper;

    private TMP_Text _scheduledTaskInfoText;
    private TaskPlane TaskPlane { get; set; }

    private bool _isRiskMarkReset;

    protected override void Awake()
    {
        base.Awake();
        ScheduledCellImage = GetComponent<Image>();
        RectTransform = GetComponent<RectTransform>();
        _hintPlane = Instantiate(hintPlanePrefab, transform);
        _timer = Instantiate(timerPrefab, transform);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        UnmarkScheduledCells(true);
    }

    public void MarkCellsScheduled(List<CalendarCell> cells, WorkerRow scheduledAtRow, TaskPlane taskPlane)
    {
        if (cells.Count == 0) return;
        TaskPlane = taskPlane;
        var referenceCell = cells[0];
        transform.position = referenceCell.transform.position;
        var sizeBuf = referenceCell.RectTransform.sizeDelta;
        sizeBuf.x *= cells.Count;
        RectTransform.sizeDelta = sizeBuf;
        foreach (var cell in cells) cell.ScheduledByMark = this;
        ScheduledCells = cells;
        ScheduledAtRow = scheduledAtRow;
        taskPlane.transform.SetParent(transform);
        taskPlane.gameObject.SetActive(false);
    }

    public int CalculateReferenceProgress()
    {
        return (int)(TaskPlane.SubtaskWrapper.CalculateReferenceProgress(ScheduledOnHours) * MarkEfficiencyCoeff);
    }

    public void PrecalculateCellProgress(CalendarCell cell)
    {
        if (!ScheduledCells.Contains(cell)) return;
        
        SubtaskWrapper.PrecalculateSubtaskProgress(
            ScheduledAtRow.WorkerEfficiency * RiskMark.InfluenceCoeff, 
            IsLastScheduledCell(cell.Index));
        
        if (IsLastScheduledCell(cell.Index) && _isRiskMarkReset) RiskMark.Reset();
    }
    
    public void ResetRiskMark() => RiskMark.Reset();

    public void ResetPrecalculatedProgress() => SubtaskWrapper.ResetPrecalculatedProgress();

    public void ApplyRiskInfluence(bool autoReset = true)
    {
        RiskMark = SubtaskWrapper.ApplyRiskInfluence();
        _isRiskMarkReset = autoReset;
    }

    public void UnmarkScheduledCells(bool withTaskReturn)
    {
        ScheduledCells ??= new List<CalendarCell>();
        foreach (var cell in ScheduledCells) cell.ResetScheduling();
        if (withTaskReturn)
        {
            TaskPlane.gameObject.SetActive(true);
            TaskPlane.Return();
        }
        onUnmarked.Invoke(this);
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        var colorBuf = ScheduledCellImage.color;
        colorBuf.a = HoveredAlpha;
        ScheduledCellImage.color = colorBuf;
        _timer.OnFinish.RemoveAllListeners();
        _timer.OnFinish.AddListener(() => ShowHint(eventData.position));
        _timer.StartTimer(showHintInSeconds);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var colorBuf = ScheduledCellImage.color;
        colorBuf.a = CommonAlpha;
        ScheduledCellImage.color = colorBuf;
        if (_timer.IsRunning) _timer.StopTimer();
        else _hintPlane.Hide();
    }

    private void ShowHint(Vector2 pointerPosition)
    {
        var sb = new StringBuilder();
        var referenceProgress = CalculateReferenceProgress();
        sb.AppendLine(SubtaskWrapper.SubtaskName);
        sb.AppendLine($"Ожидаемый результат: {referenceProgress}");
        sb.AppendLine($"Фактический результат: {PrecalculatedProgress}");
        if (referenceProgress > PrecalculatedProgress)
            sb.Append("(Производительность ограничена родительской задачей)");
        _hintPlane.Show(sb.ToString(), pointerPosition);
    }
}