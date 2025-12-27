using System;
using System.Collections.Generic;
using Scheduler.Bases;
using Scheduler.Menu.Calendar;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScheduledCellPlane : DragableBase, IPointerEnterHandler, IPointerExitHandler
{
    public Image ScheduledCellImage { get; set; }
    public float CommonAlpha { get; set; }
    public float HoveredAlpha { get; set; }
    
    private TMP_Text _scheduledTaskInfoText;
    private TaskPlane ScheduledTaskPlane { get; set; } = null;
    private List<CalendarCell> _scheduledCells;

    protected override void Awake()
    {
        base.Awake();
        ScheduledCellImage = GetComponent<Image>();
        RectTransform = GetComponent<RectTransform>();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        UnscheduleCells();
    }

    public void ScheduleCells(List<CalendarCell> cells, TaskPlane taskPlane)
    {
        if (cells.Count == 0) return;
        ScheduledTaskPlane = taskPlane;
        var referenceCell = cells[0];
        transform.position = referenceCell.transform.position;
        var sizeBuf = referenceCell.RectTransform.sizeDelta;
        sizeBuf.x *= cells.Count;
        RectTransform.sizeDelta = sizeBuf;
        foreach (var cell in cells) cell.ScheduledSubtask = taskPlane.SubtaskWrapper;
        _scheduledCells = cells;
        taskPlane.transform.SetParent(transform);
        taskPlane.gameObject.SetActive(false);
    }

    private void UnscheduleCells()
    {
        foreach (var cell in _scheduledCells) cell.ScheduledSubtask = null;
        _scheduledCells.Clear();
        ScheduledTaskPlane.gameObject.SetActive(true);
        ScheduledTaskPlane.Return();
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        var colorBuf = ScheduledCellImage.color;
        colorBuf.a = HoveredAlpha;
        ScheduledCellImage.color = colorBuf;
        Debug.Log($"{ScheduledTaskPlane.SubtaskWrapper.SubtaskName};");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var colorBuf = ScheduledCellImage.color;
        colorBuf.a = CommonAlpha;
        ScheduledCellImage.color = colorBuf;
    }
}
