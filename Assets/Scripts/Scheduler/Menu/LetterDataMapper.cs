using Scheduler.Data;
using UnityEngine;
using UnityEngine.UI;

public class LetterDataMapper : MonoBehaviour
{
    [SerializeField] private TaskDescription taskDescriptionPrefab;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
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
