using Scheduler.Menu.Calendar;
using Scheduler.Wrappers;
using UnityEngine;
using UnityEngine.Serialization;

public class TaskCommiter : MonoBehaviour
{
    [SerializeField] CalendarGrid calendarGrid;
    [SerializeField] CalendarTaskMenu calendarTaskMenu;
    [SerializeField] private TaskPlane taskPlanePrefab;
    [SerializeField] private GameObject taskNotePrefab;

    public void CommitSubtask(SubtaskWrapper subtaskWrapper, int totalHours)
    {
        var newTaskPlane = Instantiate(taskPlanePrefab, calendarTaskMenu.transform);
        newTaskPlane.ScheduledOnHours = totalHours;
        newTaskPlane.SubtaskWrapper = subtaskWrapper;
        newTaskPlane.CalendarGrid = calendarGrid;
    }
    
}
