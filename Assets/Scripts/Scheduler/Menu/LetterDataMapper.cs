using System;
using Scheduler.Data;
using UnityEngine;
using UnityEngine.UI;

public class LetterDataMapper : MonoBehaviour
{
    [SerializeField] private TaskDescription taskDescriptionPrefab;
    private VerticalLayoutGroup verticalLayoutGroup;
    
    private TaskDescription[] instantiatedTaskDescriptions;

    private void Awake()
    {
        verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
    }

    public void RemapData()
    {
        instantiatedTaskDescriptions = new TaskDescription[DataContainer.ChosenTasks.Count];
        for (var i = 0; i < DataContainer.ChosenTasks.Count; i++)
        {
            var task = DataContainer.ChosenTasks[i];
            var taskDescription = Instantiate(taskDescriptionPrefab, verticalLayoutGroup.transform);
            taskDescription.TaskName.text = task.Name;
            taskDescription.TaskDeadline.text = $"{task.DayLimit} дня";
            taskDescription.TaskProgress.text = $"{task.Progress}/{task.Target}";
            instantiatedTaskDescriptions[i] = taskDescription;
        }
    }

    public void Clear()
    {
        foreach (var taskDescription in instantiatedTaskDescriptions)
            Destroy(taskDescription.gameObject);
    }
}
