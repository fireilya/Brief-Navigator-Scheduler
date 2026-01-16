using Domain.Scheduler;
using Scheduler.Wrappers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NotebookSubtaskAmountControl : MonoBehaviour
{
    [SerializeField] private TMP_Text subtaskEfficiencyText;
    [SerializeField] private TMP_Text workHoursAmountText;
    [SerializeField] private TMP_Text totalAmountText;

    [SerializeField] protected Button moreButton;
    [SerializeField] protected Button lessButton;
    
    [HideInInspector]
    public UnityEvent<int> onWorkHoursChanged = new();

    private int _workHours;

    public SubtaskWrapper SubtaskWrapper { get; set; }

    public int WorkHours
    {
        get => _workHours;
        protected set
        {
            if (_workHours == value) return;
            _workHours = value;
            onWorkHoursChanged.Invoke(_workHours);
        }
    }

    public virtual int TotalHours => WorkHours;

    protected virtual void Awake()
    {
        ConfigureButtons();
    }

    protected virtual void ConfigureButtons()
    {
        moreButton.onClick.AddListener(() => OnAmountButtonClicked(true));
        lessButton.onClick.AddListener(() => OnAmountButtonClicked(false));
        onWorkHoursChanged.AddListener(newHour => lessButton.interactable = newHour != 0);
        lessButton.interactable = false;
    }

    protected virtual void OnAmountButtonClicked(bool isIncreaseButton)
    {
        WorkHours += isIncreaseButton ? 1 : -1;
        UpdateWorkInfoLabels();
    }

    public virtual void UpdateWorkInfoLabels()
    {
        if (SubtaskWrapper == null) return;
        subtaskEfficiencyText.text = $"{SubtaskWrapper.SubtaskEfficiency} шт/час";
        workHoursAmountText.text = WorkHours.ToString();
        totalAmountText.text = (SubtaskWrapper.SubtaskEfficiency * WorkHours).ToString();
    }

    public virtual void Clear()
    {
        SubtaskWrapper = null;
        WorkHours = 0;
    }
}