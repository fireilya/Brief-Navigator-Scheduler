using System;
using System.Collections.Generic;
using Domain.Scheduler;
using Scheduler.Wrappers;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scheduler.Menu.Calendar
{
    public class WorkerRow : MonoBehaviour
    {
        [SerializeField] private CalendarCell cellPrefab;
        [SerializeField] private ScheduledCellsMark scheduledCellsMarkPrefab;

        [HideInInspector] public UnityEvent<ScheduledCellsMark> onNewCellsScheduled;
        [HideInInspector] public UnityEvent<ScheduledCellsMark> onCellsUnscheduled;
        public TMP_Text WorkerEfficiencyText { get; private set; }
        
        public CalendarCell[] Cells {get; private set;}
        public CalendarWorker CalendarWorker {get; set;}

        public float WorkerEfficiency => CalendarWorker.WorkerData.EfficiencyCoeff;
        
        private HorizontalLayoutGroup _cellsRow;
        private ScheduledCellsMark _prescheduledCellsMark;
        private CalendarGrid _parent;

        private const float PreschedulePlaneAlpha = 0.5f;
        private const float CommonScheduledPlaneAlpha = 0.75f;
        private const float HoveredScheduledPlaneAlpha = 1.0f;

        public CalendarGrid Parent
        {
            get => _parent;

            set
            {
                if (_parent) throw new Exception("You cannot reassign parent");
                _parent = value;
            }
        }

        private void Awake()
        {
            WorkerEfficiencyText = GetComponentInChildren<TMP_Text>();
            _cellsRow = GetComponentInChildren<HorizontalLayoutGroup>();
            _prescheduledCellsMark = Instantiate(scheduledCellsMarkPrefab, transform);
            _prescheduledCellsMark.ScheduledCellImage.raycastTarget = false;
            var colorBuf = _prescheduledCellsMark.ScheduledCellImage.color;
            colorBuf.a = PreschedulePlaneAlpha;
            _prescheduledCellsMark.ScheduledCellImage.color = colorBuf;
            _prescheduledCellsMark.gameObject.SetActive(false);
        }

        public void CreateCellsRow(int cellsCount)
        {
            if (cellsCount == 0) return;
            Cells = new CalendarCell[cellsCount];
            for (var i = 0; i < cellsCount; i++)
            {
                var cell = Instantiate(cellPrefab, _cellsRow.transform);
                cell.Index = i;
                cell.onTaskPlaneEnter.AddListener(PrescheduledCell);
                cell.onTaskPlaneExit.AddListener(UnPrescheduleCell);
                cell.onTaskPlaneDropped.AddListener(ScheduleCell);
                Cells[i] = cell;
            }

            if (CalendarWorker) ConfigureWorker();
        }

        private void ConfigureWorker()
        {
            var workerCell = Cells[0];
            workerCell.enabled = false;
            CalendarWorker.transform.SetParent(workerCell.transform);
            CalendarWorker.RectTransform.sizeDelta = workerCell.RectTransform.sizeDelta;
            CalendarWorker.transform.position = workerCell.transform.position;
            WorkerEfficiencyText.SetText(CalendarWorker.WorkerData.EfficiencyCoeff.ToString("F1"));
        }

        private void PrescheduledCell(int cellIndex, TaskPlane plannableTask)
        {
            var cell = Cells[cellIndex];
            _prescheduledCellsMark.gameObject.SetActive(true);
            _prescheduledCellsMark.transform.position = cell.transform.position;
            var sizeBuf = cell.RectTransform.sizeDelta;
            sizeBuf.x *= plannableTask.ScheduledOnHours;
            _prescheduledCellsMark.RectTransform.sizeDelta = sizeBuf;
        }

        private void UnPrescheduleCell(int cellIndex, TaskPlane plannableTask)
        {
            _prescheduledCellsMark.gameObject.SetActive(false);
        }

        private void ScheduleCell(int cellIndex, TaskPlane schedulingTask)
        {
            _prescheduledCellsMark.gameObject.SetActive(false);
            var cellsToSchedule = new List<CalendarCell>();
            for (var i = 0; i < schedulingTask.ScheduledOnHours; i++)
            {
                if (cellIndex + i >= Cells.Length)
                {
                    Parent.ShowWarningWindow(
                        "Ваши работники не будут работать сверхурочно! " +
                        "Время выполнения планируемой задачи будет обрезано " +
                        $"с {schedulingTask.ScheduledOnHours} часов до {i}",
                        continueCallback: () => ScheduleCellsBuf(cellsToSchedule, schedulingTask));
                    return;
                }

                if (Cells[cellIndex + i].IsScheduled)
                {
                    Parent.ShowWarningWindow(
                        "Ваши работники не будут выполнять два дела одновременно! " +
                        $"Из-за пересечения с задачей \"{Cells[cellIndex + i].ScheduledSubtaskWrapper.SubtaskName}\" " +
                        "время выполнения планируемой задачи будет обрезано " +
                        $"с {schedulingTask.ScheduledOnHours} часов до {i}",
                        continueCallback:() => ScheduleCellsBuf(cellsToSchedule, schedulingTask));
                    return;
                }

                cellsToSchedule.Add(Cells[cellIndex + i]);
            }
            ScheduleCellsBuf(cellsToSchedule, schedulingTask);
        }

        private void ScheduleCellsBuf(List<CalendarCell> cellsToSchedule, TaskPlane schedulingTask)
        {
            if (cellsToSchedule.Count == 0) return;
            var scheduledCellsMark = Instantiate(scheduledCellsMarkPrefab, transform);
            scheduledCellsMark.CommonAlpha = CommonScheduledPlaneAlpha;
            scheduledCellsMark.HoveredAlpha = HoveredScheduledPlaneAlpha;
            scheduledCellsMark.MarkCellsScheduled(cellsToSchedule, this, schedulingTask);
            Parent.RegisterScheduledMark(scheduledCellsMark);
            scheduledCellsMark.onUnmarked.AddListener(mark => Parent.RemoveScheduledMark(mark));
            scheduledCellsMark.onUnmarked.AddListener(_ => onCellsUnscheduled.Invoke(scheduledCellsMark));
            onNewCellsScheduled.Invoke(scheduledCellsMark);
        }
    }
}