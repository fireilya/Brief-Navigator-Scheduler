using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CalendarGrid : MonoBehaviour
    {
        [SerializeField] private WorkerRow workerRowPrefab;
        [SerializeField] private TMP_Text timeLabelPrefab;
        [SerializeField] private int hoursCount;
        [SerializeField] private int startHour;
        [SerializeField] private bool isWithTimeLabels;
        [SerializeField] private UserInfoWindow userInfoWindowPrefab;
        [SerializeField] private CalendarWorker calendarWorkerPrefab;
        [SerializeField] private TimeFlowController timeFlowController;
        [SerializeField] private Canvas calendarCanvas;
        
        public WorkerRow[] WorkerRows { get; private set; }
        private HorizontalLayoutGroup _timeLabels;
        private CalendarMenu _calendarMenu;
        private readonly int ShowingHourThreshold = 24;
        public readonly int SchedulableCellsOffset = 1;
        private Dictionary<Subtask, HashSet<ScheduledCellsMark>> ScheduledSubtasksMarks { get; set; } = new();
        public Subtask[] ScheduledSubtasks => ScheduledSubtasksMarks.Keys.ToArray();

        public int CellsInRowCount => hoursCount + SchedulableCellsOffset;

        void Awake()
        {
            _timeLabels = GetComponentInChildren<HorizontalLayoutGroup>();
        }

        void Start()
        {
            _calendarMenu = GetComponentInParent<CalendarMenu>();
            var workers = DBServerMock.GetAllWorkers();
            WorkerRows = new WorkerRow[workers.Length];
            for (var i = 0; i < workers.Length; i++)
            {
                var worker = workers[i];
                var calendarWorker = Instantiate(calendarWorkerPrefab);
                calendarWorker.WorkerData = worker;
                calendarWorker.ParentCanvas = calendarCanvas;
                var currentWorkerRow = Instantiate(workerRowPrefab, transform);
                currentWorkerRow.Parent = this;
                currentWorkerRow.CalendarWorker = calendarWorker;
                currentWorkerRow.CreateCellsRow(hoursCount + SchedulableCellsOffset);
                WorkerRows[i] = currentWorkerRow;
                currentWorkerRow.onNewCellsScheduled.AddListener(_ => timeFlowController.RecalculateCellsDayProgress());
                currentWorkerRow.onCellsUnscheduled.AddListener(_ => timeFlowController.RecalculateCellsDayProgress());
            }

            if (!isWithTimeLabels) return;
            _timeLabels.gameObject.SetActive(true);
            var currentHour = startHour;
            for (var i = 0; i < hoursCount + 1; i++)
            {
                var newTimeLabel = Instantiate(timeLabelPrefab, _timeLabels.transform);
                newTimeLabel.text = $"{currentHour++}:00";
                currentHour %= ShowingHourThreshold;
            }

            _timeLabels.transform.SetAsLastSibling();
            timeFlowController.CalendarGrid = this;
        }

        public ScheduledCellsMark[] GetAllScheduledSubtaskMarks()
        {
            var answer = new LinkedList<ScheduledCellsMark>();
            foreach (var mark in ScheduledSubtasksMarks.Values.SelectMany(m => m))
                answer.AddLast(mark);
            return answer.ToArray();
        }
            
        
        public void ShowWarningWindow(
            string warningMessage, 
            UnityAction continueCallback = null, 
            UnityAction cancelCallback = null)
        {
            const string cancelText = "Отмена";
            const string continueText = "Продолжить";
            
            var warningWindow = Instantiate(userInfoWindowPrefab, _calendarMenu.transform);
            warningWindow.IsDestroyOnUserInput = true;
            warningWindow.InfoMessage.SetText(warningMessage);
            
            if (cancelCallback == null) warningWindow.AddNewButton(cancelText);
            else warningWindow.AddNewButton(cancelText, cancelCallback);
            
            if (continueCallback == null) warningWindow.AddNewButton(continueText);
            else warningWindow.AddNewButton(continueText, continueCallback);
        }

        public void ShowErrorWindow(string errorMessage, UnityAction callback = null)
        {
            const string buttonText = "Ок";
            
            var errorWindow = Instantiate(userInfoWindowPrefab, _calendarMenu.transform);
            errorWindow.IsDestroyOnUserInput = true;
            errorWindow.InfoMessage.SetText(errorMessage);
            
            if (callback == null) errorWindow.AddNewButton(buttonText);
            else errorWindow.AddNewButton(buttonText, callback);
        }

        public void RegisterScheduledMark(ScheduledCellsMark mark)
        {
            if (!ScheduledSubtasksMarks.ContainsKey(mark.SubtaskWrapper.Subtask))
                ScheduledSubtasksMarks.Add(mark.SubtaskWrapper.Subtask, new HashSet<ScheduledCellsMark> { mark });
            else ScheduledSubtasksMarks[mark.SubtaskWrapper.Subtask].Add(mark);
        }

        public void RemoveScheduledMark(ScheduledCellsMark mark)
        {
            if (!ScheduledSubtasksMarks.ContainsKey(mark.SubtaskWrapper.Subtask)) return;
            ScheduledSubtasksMarks[mark.SubtaskWrapper.Subtask].Remove(mark);
            if (ScheduledSubtasksMarks[mark.SubtaskWrapper.Subtask].Count != 0) return;
            mark.SubtaskWrapper.Subtask.ResetDayProgress();
            ScheduledSubtasksMarks.Remove(mark.SubtaskWrapper.Subtask);
        }

        public void ResetAllPrecalculatedProgress()
        {
            foreach (var subtask in ScheduledSubtasksMarks.Keys)
            {
                foreach (var mark in ScheduledSubtasksMarks[subtask]) mark.ResetPrecalculatedProgress();
                subtask.ResetDayProgress();
            }
        }
        
        public void ClearAllScheduledSubtaskMarks()
        {
            foreach (var mark in GetAllScheduledSubtaskMarks())
            {
                mark.onUnmarked.RemoveAllListeners();
                mark.UnmarkScheduledCells(false);
            }
            ScheduledSubtasksMarks.Clear();
        }
    }
}