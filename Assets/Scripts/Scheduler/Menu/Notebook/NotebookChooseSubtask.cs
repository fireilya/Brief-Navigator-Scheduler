using System;
using System.Collections.Generic;
using Scheduler.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Notebook
{
    public class NotebookChooseSubtask : NotebookSubcanvas
    {
        [SerializeField] private SubtaskVariant subtaskVariantPrefab;
        [SerializeField] private Button backButton;
        private VerticalLayoutGroup _verticalLayoutGroup;

        private List<GameObject> _createdObjects;


        protected override void Awake()
        {
            _verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        }

        protected virtual void Start()
        {
            if (ParentNotebook) backButton.onClick.AddListener(ParentNotebook.Previous);
        }

        void OnDestroy()
        {
            backButton.onClick.RemoveAllListeners();
        }

        private protected override void Reinit()
        {
            Clear();
            foreach (var subtask in ParentNotebook.SelectedTask.Subtasks)
            {
                var subtaskVariant = Instantiate(subtaskVariantPrefab, _verticalLayoutGroup.transform);
                subtaskVariant.SubtaskName.text = subtask.Name;
                subtaskVariant.TaskButton.onClick.AddListener(() =>
                {
                    ParentNotebook.SelectedSubtask = subtask;
                    ParentNotebook.Next();
                });
                subtaskVariant.SubtaskProgress.text = $"{subtask.DoneProgress}/{subtask.GameTask!.Target}";
                CreatedObjects.Add(subtaskVariant.gameObject);
            }
        }
    }
}