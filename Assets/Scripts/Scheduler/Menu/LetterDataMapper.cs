using System;
using Scheduler.Data;
using UnityEngine;
using UnityEngine.UI;

public class LetterDataMapper : MonoBehaviour
{
    [SerializeField] private TaskDescription taskDescriptionPrefab;
    private VerticalLayoutGroup verticalLayoutGroup;

    private void Awake()
    {
        verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
    }

    void Start()
    {
        foreach (var task in DataContainer.ChosenTasks) 
        {
            var taskDescription = Instantiate(taskDescriptionPrefab,  verticalLayoutGroup.transform);
            taskDescription.TaskName.text =  task.Name;
            taskDescription.TaskDeadline.text = $"{task.DayLimit} дня";
            taskDescription.TaskProgress.text = $"{task.Progress}/{task.Target}";
        }
    }
}
