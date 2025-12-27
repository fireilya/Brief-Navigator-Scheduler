using System;
using System.Collections.Generic;
using Domain.Scheduler;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scheduler.Menu.Calendar
{
    public class WorkerRow : MonoBehaviour
    {
        [SerializeField] private CalendarCell cellPrefab;
        [SerializeField] private ScheduledCellPlane scheduledCellPlanePrefab;
        public TMP_Text WorkerEfficiency { get; set; }
        public CalendarCell[] Cells {get; private set;}
        public CalendarWorker CalendarWorker {get; set;}
        
        private HorizontalLayoutGroup _cellsRow;
        private ScheduledCellPlane prescheduledCellPlane;
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
            WorkerEfficiency = GetComponentInChildren<TMP_Text>();
            _cellsRow = GetComponentInChildren<HorizontalLayoutGroup>();
            prescheduledCellPlane = Instantiate(scheduledCellPlanePrefab, transform);
            prescheduledCellPlane.ScheduledCellImage.raycastTarget = false;
            var colorBuf = prescheduledCellPlane.ScheduledCellImage.color;
            colorBuf.a = PreschedulePlaneAlpha;
            prescheduledCellPlane.ScheduledCellImage.color = colorBuf;
            prescheduledCellPlane.gameObject.SetActive(false);
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
            CalendarWorker.WorkerImage.sprite = ImageServerMock.LoadImage(CalendarWorker.WorkerData.PathToIcon);
            CalendarWorker.transform.SetParent(workerCell.transform);
            CalendarWorker.RectTransform.sizeDelta = workerCell.RectTransform.sizeDelta;
            CalendarWorker.transform.position = workerCell.transform.position;
            WorkerEfficiency.SetText(CalendarWorker.WorkerData.EfficiencyCoeff.ToString("F1"));
        }

        private void PrescheduledCell(int cellIndex, TaskPlane plannableTask)
        {
            var cell = Cells[cellIndex];
            prescheduledCellPlane.gameObject.SetActive(true);
            prescheduledCellPlane.transform.position = cell.transform.position;
            var sizeBuf = cell.RectTransform.sizeDelta;
            sizeBuf.x *= plannableTask.ScheduledOnHours;
            prescheduledCellPlane.RectTransform.sizeDelta = sizeBuf;
        }

        private void UnPrescheduleCell(int cellIndex, TaskPlane plannableTask)
        {
            prescheduledCellPlane.gameObject.SetActive(false);
        }

        private void ScheduleCell(int cellIndex, TaskPlane schedulingTask)
        {
            prescheduledCellPlane.gameObject.SetActive(false);
            var cellsToSchedule = new List<CalendarCell>();
            for (var i = 0; i < schedulingTask.ScheduledOnHours; i++)
            {
                if (cellIndex + i >= Cells.Length)
                {
                    Parent.ShowWarning(
                        $"Ваши работники не будут работать сверхурочно! " +
                        $"Время выполнения планируемой задачи будет обрезано " +
                        $"с {schedulingTask.ScheduledOnHours} часов до {i}",
                        () => ScheduleCellsBuf(cellsToSchedule, schedulingTask));
                    return;
                }

                if (Cells[cellIndex + i].IsScheduled)
                {
                    Parent.ShowWarning(
                        $"Ваши работники не будут выполнять два дела одновременно! " +
                        $"Из-за пересечения с задачей \"{Cells[cellIndex + i].ScheduledSubtask.SubtaskName}\" " +
                        $"время выполнения планируемой задачи будет обрезано " +
                        $"с {schedulingTask.ScheduledOnHours} часов до {i}",
                        () => ScheduleCellsBuf(cellsToSchedule, schedulingTask));
                    return;
                }

                cellsToSchedule.Add(Cells[cellIndex + i]);
            }
            ScheduleCellsBuf(cellsToSchedule, schedulingTask);
        }

        private void ScheduleCellsBuf(List<CalendarCell> cellsToSchedule, TaskPlane schedulingTask)
        {
            if (cellsToSchedule.Count == 0) return;
            var scheduledCellPlain = Instantiate(scheduledCellPlanePrefab, transform);
            scheduledCellPlain.CommonAlpha = CommonScheduledPlaneAlpha;
            scheduledCellPlain.HoveredAlpha = HoveredScheduledPlaneAlpha;
            scheduledCellPlain.ScheduleCells(cellsToSchedule, schedulingTask);
        }
    }
}