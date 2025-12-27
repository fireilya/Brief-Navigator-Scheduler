using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CalendarWarning : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TMP_Text warningMessageText;
    
    public UnityEvent OnContinueButtonClick => continueButton.onClick;
    public UnityEvent OnCancelButtonClick => cancelButton.onClick;
    
    private bool isContinue = false;

    private void Awake()
    {
        continueButton.onClick.AddListener(() => gameObject.SetActive(false));
        cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void ShowWarning(string warningMessage)
    {
        gameObject.SetActive(true);
        warningMessageText.text = warningMessage;
    }
}
