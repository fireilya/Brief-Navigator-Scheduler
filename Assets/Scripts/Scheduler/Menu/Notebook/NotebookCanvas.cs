using System;
using Domain.Scheduler;
using Scheduler.Menu;
using Scheduler.Menu.Notebook;
using UnityEngine;

public class NotebookCanvas : MonoBehaviour
{
    [SerializeField] private MapDataMapper mapper;
    public Location SelectedLocation => mapper.CurrentChosenLocation?.LocationData;

    private Guid previousLocationId =  Guid.Empty;

    public GameTask SelectedTask { get; set; } = null;
    public Subtask SelectedSubtask { get; set; } = null;

    private NotebookSubcanvas[] _childrens;

    private int state = 0;

    private bool isNeedInit = true;

    void Awake()
    {
        _childrens = GetComponentsInChildren<NotebookSubcanvas>(true);
        foreach (var children in _childrens) children.ParentNotebook = this;
    }

    public void Init()
    {
        isNeedInit = previousLocationId == Guid.Empty || previousLocationId != mapper.CurrentChosenLocation?.LocationData.Id;
        foreach (var children in _childrens) children.gameObject.SetActive(false);
        state = 0;
        _childrens[state].Enable(isNeedInit);
    }

    public void Next()
    {
        _childrens[state].gameObject.SetActive(false);
        _childrens[++state].Enable(isNeedInit);
    }

    public void Previous()
    {
        _childrens[state].gameObject.SetActive(false);
        _childrens[--state].Enable(isNeedInit);
    }
}
