using TMPro;
using UnityEngine;

public class TaskDescription : MonoBehaviour
{
    public TMP_Text TaskName {get; private set;}
    public TMP_Text TaskDeadline {get; private set;}
    public TMP_Text TaskProgress {get; private set;}
    void Awake()
    {
        TaskName = transform.Find("TaskName").GetComponent<TMP_Text>();
        TaskDeadline = transform.Find("TaskDeadline").GetComponent<TMP_Text>();
        TaskProgress = transform.Find("TaskProgress").GetComponent<TMP_Text>();
    }
}
