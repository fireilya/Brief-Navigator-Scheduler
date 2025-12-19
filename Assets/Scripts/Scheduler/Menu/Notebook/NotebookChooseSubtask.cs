using System.Collections.Generic;
using Scheduler.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Notebook
{
    public class NotebookChooseSubtask : NotebookSubcanvas
    {
        [SerializeField] private SubtaskVariant subtaskVariantPrefab;
        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;

        private List<GameObject> _createdObjects;


        public override void Enable(bool isNeedInit)
        {
            gameObject.SetActive(true);
            if (isNeedInit) Init();
        }

        public override void Init()
        {
            Clear();
            foreach (var subtask in ParentNotebook.SelectedTask.Subtasks)
            {
                var subtaskVariant = Instantiate(subtaskVariantPrefab, verticalLayoutGroup.transform);
                subtaskVariant.SubtaskName.text = subtask.Name;
                subtaskVariant.TaskButton.onClick.AddListener(() =>
                {
                    ParentNotebook.SelectedSubtask = subtask;
                    ParentNotebook.Next();
                });
                subtaskVariant.SubtaskProgress.text = $"{subtask.Progress}/{subtask.Parent!.Target}";
                CreatedObjects.Add(subtaskVariant.gameObject);
            }
        }
    }
}