using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskVariant : MonoBehaviour
{
    public Button TaskButton {get; private set;}
    public TMP_Text TaskName {get; private set;}
    void Awake()
    {
        TaskButton = GetComponentInChildren<Button>();
        TaskName = GetComponentInChildren<TMP_Text>();
    }
}
