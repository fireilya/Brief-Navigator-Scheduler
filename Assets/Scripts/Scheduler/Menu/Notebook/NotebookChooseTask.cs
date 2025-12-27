using Scheduler.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Notebook
{
    public class NotebookChooseTask : NotebookSubcanvas
    {
        [SerializeField] private TaskVariant taskVariantPrefab;
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
                taskVariant.TaskName.text = task.Name;
                taskVariant.TaskButton.onClick.AddListener(() =>
                {
                    ParentNotebook.SelectedTask = task;
                    ParentNotebook.Next();
                });
                CreatedObjects.Add(taskVariant.gameObject);
            }
        }
    }
}
