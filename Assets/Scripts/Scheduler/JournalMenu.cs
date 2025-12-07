using UnityEngine;
using UnityEngine.UI;

public class JournalMenu : MonoBehaviour 
{
    public GameObject openJournal;
    public GameObject darkBackground;
    public GameObject closeButton;

    public void OpenJournal()
    {
        if (openJournal != null) { openJournal.SetActive(true); }
        if (darkBackground != null) { darkBackground.SetActive(true); }
        if (closeButton != null) { closeButton.SetActive(true); }
    }

    public void CloseJournal()
    {
        if (openJournal != null) openJournal.SetActive(false);
        if (darkBackground != null) darkBackground.SetActive(false);
        if (closeButton != null) closeButton.SetActive(false);
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
