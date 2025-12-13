using Assets.Scripts.Scheduler;
using Assets.Scripts.Scheduler.Menu;

public class NotebookMenu : MenuBase
{
    public LocationData LocationData;
    protected override bool CanBeOpened => LocationData.IsSelected;
}
