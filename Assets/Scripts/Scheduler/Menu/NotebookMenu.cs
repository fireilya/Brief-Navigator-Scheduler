using Assets.Scripts.Scheduler;
using Assets.Scripts.Scheduler.Menu;
using UnityEngine;

public class NotebookMenu : MenuBase
{
    [Header("Data")]
    [SerializeField] private LocationData LocationData;
    protected override bool CanBeOpened => LocationData.IsSelected;
}