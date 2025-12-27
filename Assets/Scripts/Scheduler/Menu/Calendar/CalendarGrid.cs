using System.Threading.Tasks;
using Domain.Scheduler;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scheduler.Menu.Calendar
{
    public class CalendarGrid : MonoBehaviour
    {
        [SerializeField] private WorkerRow workerRowPrefab;
        [SerializeField] private TMP_Text timeLabelPrefab;
        [SerializeField] private int workersCount;
        [SerializeField] private int hoursCount;
        [SerializeField] private int startHour;
        [SerializeField] private bool isWithTimeLabels;
        [SerializeField] private CalendarWarning calendarWarning;
        [SerializeField] private CalendarWorker calendarWorkerPrefab;
        private HorizontalLayoutGroup _timeLabels;
        private const int ShowingHourThreshold = 24;

        void Awake()
        {
            if (!DBServerMock.IsInited) DBServerMock.Init();
            _timeLabels = GetComponentInChildren<HorizontalLayoutGroup>();
        }

        void Start()
        {
            var workers = DBServerMock.GetAllWorkers();
            foreach (var worker in workers)
            {
                var calendarWorker = Instantiate(calendarWorkerPrefab);
                calendarWorker.WorkerData = worker;
                var currentWorkerRow = Instantiate(workerRowPrefab, transform);
                currentWorkerRow.Parent = this;
                currentWorkerRow.CalendarWorker = calendarWorker;
                currentWorkerRow.CreateCellsRow(hoursCount + 1);
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
        }

        public void ShowWarning(string warningMessage, UnityAction callback)
        {
            calendarWarning.OnContinueButtonClick.AddListener(callback);
            calendarWarning.OnContinueButtonClick.AddListener(ClearContinueButtonListener);
            
            calendarWarning.OnCancelButtonClick.AddListener(ClearContinueButtonListener);
            calendarWarning.OnCancelButtonClick.AddListener(() =>
                    calendarWarning.OnCancelButtonClick.RemoveListener(ClearContinueButtonListener));
            
            calendarWarning.ShowWarning(warningMessage);
            return;

            void ClearContinueButtonListener() => calendarWarning.OnContinueButtonClick.RemoveListener(callback);
        }
    }
}