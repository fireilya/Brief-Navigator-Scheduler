using Scheduler.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Notebook
{
    public class NotebookChooseTask : NotebookSubcanvas
    {
        [SerializeField] private TaskVariant taskVariantPrefab;
        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;

        public override void Enable(bool isNeedInit)
        {
            gameObject.SetActive(true);
            if (isNeedInit) Init();
        }

        public override void Init()
        {
            Clear();
            gameObject.SetActive(true);
            foreach (var task in DataContainer.ChosenTasksByLocation[ParentNotebook.SelectedLocation.Id])
            {
                var taskVariant = Instantiate(taskVariantPrefab, verticalLayoutGroup.transform);
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
