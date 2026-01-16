using System;
using System.Linq;
using Scheduler.Prefabs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserInfoWindow : MonoBehaviour
{
    [SerializeField] private ButtonPrefab buttonPrefab;
    public TMP_Text InfoMessage { get; private set; }
    public HorizontalLayoutGroup ButtonsSpace { get; private set; }

    public bool IsDestroyOnUserInput { get; set; } = false;

    private void Awake()
    {
        InfoMessage = GetComponentInChildren<TMP_Text>();
        ButtonsSpace = GetComponentInChildren<HorizontalLayoutGroup>();
    }

    public void AddNewButton(string buttonText, params UnityAction[] callbacks)
    {
        var newButton = Instantiate(buttonPrefab, ButtonsSpace.transform);
        newButton.Text.SetText(buttonText);
        foreach (var callback in callbacks) 
            newButton.Button.onClick.AddListener(callback);
        if (IsDestroyOnUserInput) newButton.Button.onClick.AddListener(() => Destroy(gameObject));
    }
}