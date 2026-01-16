using Domain.Scheduler;
using Scheduler.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Notebook
{
    public class NotebookChooseTask : NotebookSubcanvas
    {
        [SerializeField] private TaskVariant taskVariantPrefab;
        [SerializeField] private Color expiredTaskColor;
        private VerticalLayoutGroup _verticalLayoutGroup;

        protected override void Awake()
        {
            _verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        }

        private protected override void Reinit()
        {
            Clear();
            gameObject.SetActive(true);
            foreach (var task in DataContainer.ChosenTasksByLocation[ParentNotebook.SelectedLocation.Id])
            {
                var taskVariant = Instantiate(taskVariantPrefab, _verticalLayoutGroup.transform);
                if (ParentNotebook.TimeFlowController.CurrentDay <= task.DayLimit) SetupActualTask(taskVariant, task);
                else SetupExpiredTaskVariant(taskVariant, task);
                CreatedObjects.Add(taskVariant.gameObject);
            }
        }

        private void SetupActualTask(TaskVariant taskVariant, GameTask task)
        {
            taskVariant.TaskName.text = task.Name;
            taskVariant.TaskButton.onClick.AddListener(() =>
            {
                ParentNotebook.SelectedTask = task;
                ParentNotebook.Next();
            });  
        }

        private void SetupExpiredTaskVariant(TaskVariant taskVariant, GameTask task)
        {
            taskVariant.TaskName.SetText($"{task.Name} (Дедлайн истёк)");
            taskVariant.TaskButton.interactable = false;
            taskVariant.TaskName.color = expiredTaskColor;
        }
        
    }
}
