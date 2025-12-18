using Assets.Scripts.Scheduler.Menu;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class JournalTabMenu : MonoBehaviour
{
    [SerializeField] private GameObject questTab;
    [SerializeField] private Button questTabButton;
    [SerializeField] private GameObject briefTab;
    [SerializeField] private Button briefTabButton;
    [SerializeField] private GameObject sourceTab;
    [SerializeField] private Button sourceTabButton;
    [SerializeField] private GameObject navigatorTab;
    [SerializeField] private Button navigatorTabButton;

    public void SetDefaultTabs()
    {
        questTab.gameObject.SetActive(false);
        briefTab.gameObject.SetActive(false);
        sourceTab.gameObject.SetActive(false);
        navigatorTab.gameObject.SetActive(false);
    }

    public void SetDefaultButtons()
    {
        questTabButton.interactable = true;
        briefTabButton.interactable = true;
        sourceTabButton.interactable = true;
        navigatorTabButton.interactable = true;
    }

    public void OpenQuestTab()
    {
        SetDefaultTabs();
        SetDefaultButtons();
        questTab.gameObject.SetActive(true);
        questTabButton.interactable = false;
    }

    public void OpenBriefTab()
    {
        SetDefaultTabs();
        SetDefaultButtons();
        briefTab.gameObject.SetActive(true);
        briefTabButton.interactable = false;
    }
    public void OpenSourceTab()
    {
        SetDefaultTabs();
        SetDefaultButtons();
        sourceTab.gameObject.SetActive(true);
        sourceTabButton.interactable = false;
    }
    public void OpenNavigatorTab()
    {
        SetDefaultTabs();
        SetDefaultButtons();
        navigatorTab.gameObject.SetActive(true);
        navigatorTabButton.interactable = false;
    }
}