using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scheduler.Menu.Calendar;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scheduler
{
    public class TimeFlowController : MonoBehaviour
    {
        private CalendarGrid _calendarGrid;

        [SerializeField] private int questDaysCount = 4;
        [SerializeField] private ConvertableButton nextDayButton;
        [SerializeField] private TMP_Text daysInfoLabel;
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private UserInfoWindow userInfoWindowPrefab;
        public int CurrentDay { get; private set; } = 1;

        void Start()
        {
            nextDayButton.Button.onClick.AddListener(NextDay);
            nextDayButton.ButtonText.SetText("Закончить день");
            UpdateDayInfoLabel();
        }
        
        public CalendarGrid CalendarGrid
        {
            get => _calendarGrid;
            set
            {
                if (_calendarGrid) throw new Exception("You cannot reassign calendar grid.");
                _calendarGrid = value;
            }
        }

        private void NextDay()
        {
            if (CalendarGrid) StartCoroutine(ProcessCalendarGrid(false));
            ++CurrentDay;
            UpdateDayInfoLabel();
            if (CurrentDay != questDaysCount) return;
            ConvertNextDayButtonToFinishButton();
        }

        private void ConvertNextDayButtonToFinishButton()
        {
            nextDayButton.Button.onClick.RemoveAllListeners();
            
            nextDayButton.Button.onClick.AddListener(() =>
            {
                if (CalendarGrid) StartCoroutine(ProcessCalendarGrid(true));
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });
            
            nextDayButton.ButtonText.SetText("Завершить квест");
        }

        private IEnumerator ProcessCalendarGrid(bool isLoadNextSceneAfterProcess)
        {
            foreach (var mark in CalendarGrid.GetAllScheduledSubtaskMarks()
                         .OrderBy(x=>x.StartsFromIndex)
                         .ThenBy(x=>x.SubtaskOrder))
            {
                mark.ApplyRiskInfluence();
                if (mark.RiskMark.IsHappened) yield return ShowRiskInfoWindow(mark);
            }
            RecalculateCellsDayProgress();
            foreach (var scheduledSubtask in CalendarGrid.ScheduledSubtasks) scheduledSubtask.ApplyDayProgress();
            CalendarGrid.ClearAllScheduledSubtaskMarks();
            if (isLoadNextSceneAfterProcess) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        private IEnumerator ShowRiskInfoWindow(ScheduledCellsMark mark)
        {
            var infoWindow = Instantiate(userInfoWindowPrefab, mainCanvas.transform);
            var isOkPressed = false;
            infoWindow.IsDestroyOnUserInput = true;
            infoWindow.InfoMessage.SetText(mark.RiskMark.RiskMessage);
            infoWindow.AddNewButton("Ок", () => isOkPressed = true);
            yield return new WaitUntil(() => isOkPressed);
        }

        private void UpdateDayInfoLabel()
        {
            daysInfoLabel.SetText($"День {CurrentDay}/{questDaysCount}");
        }
        

        public void RecalculateCellsDayProgress()
        {
            CalendarGrid.ResetAllPrecalculatedProgress();
            for (var i = CalendarGrid.SchedulableCellsOffset; i < CalendarGrid.CellsInRowCount; i++)
            {
                var hourCellsBuf = new LinkedList<CalendarCell>();
                foreach (var workerRow in CalendarGrid.WorkerRows)
                {
                    if (workerRow.Cells[i].IsScheduled) hourCellsBuf.AddLast(workerRow.Cells[i]);
                }

                foreach (var cell in hourCellsBuf.OrderBy(c => c.ScheduledByMark.SubtaskOrder)) 
                    cell.ScheduledByMark.PrecalculateCellProgress(cell);
            }
            VerifyScheduledMarks();
        }

        private void VerifyScheduledMarks()
        {
            var x = CalendarGrid.GetAllScheduledSubtaskMarks();
            foreach (var mark in CalendarGrid.GetAllScheduledSubtaskMarks())
            {
                if (mark.PrecalculatedProgress == 0)
                {
                    CalendarGrid.ShowErrorWindow(
                        $"Подзадача \"{mark.SubtaskWrapper.SubtaskName}\" не может быть выполнена " +
                        "вследствие отсутствия доступных заготовок, производимых в родительской задаче. " +
                        $"Задача будет распланирована.",
                        () => mark.UnmarkScheduledCells(true));
                }
            }
        }
    }
}
