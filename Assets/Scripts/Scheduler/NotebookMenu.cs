using UnityEngine;
using UnityEngine.UI;

public class NotebookMenu : MonoBehaviour
{
    public GameObject openNotebook;
    public GameObject darkBackground;
    public GameObject closeButton;

    public void OpenNotebook()
    {
        if (openNotebook != null) { openNotebook.SetActive(true); }
        if (darkBackground != null) { darkBackground.SetActive(true); }
        if (closeButton != null) { closeButton.SetActive(true); }
    }

    public void CloseNotebook()
    {
        if (openNotebook != null) openNotebook.SetActive(false);
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
