using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtaskVariant : MonoBehaviour
{
    public Button TaskButton {get; private set;}
    public TMP_Text SubtaskName {get; private set;}
    
    public TMP_Text SubtaskProgress {get; private set;}
    void Awake()
    {
        TaskButton = GetComponentInChildren<Button>();
        SubtaskName = TaskButton.GetComponentInChildren<TMP_Text>();
        SubtaskProgress = transform.Find("SubtaskProgress").GetComponent<TMP_Text>();
    }
}
