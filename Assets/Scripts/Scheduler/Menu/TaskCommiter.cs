using Scheduler.Menu.Calendar;
using Scheduler.Wrappers;
using UnityEngine;

public class TaskCommiter : MonoBehaviour
{
    [SerializeField] CalendarTaskMenu calendarTaskMenu;
    [SerializeField] private TaskPlane taskPlanePrefab;
    [SerializeField] private GameObject taskNotePrefab;
    // private Vector3 transformOffset = Vector3.zero;
    // [SerializeField] private Vector3 transformOffset;

    public void CommitSubtask(SubtaskWrapper subtaskWrapper, int totalHours)
    {
        var newTaskPlane = Instantiate(taskPlanePrefab, calendarTaskMenu.transform);
        newTaskPlane.ScheduledOnHours = totalHours;
        newTaskPlane.SubtaskWrapper = subtaskWrapper;
    }
    
}
