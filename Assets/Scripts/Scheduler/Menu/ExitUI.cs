using Scheduler.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitUI : MonoBehaviour
{
    [SerializeField] private Button Repeat;
    [SerializeField] private Button Exit;

    void Start()
    {
        Repeat.onClick.AddListener(DataContainer.Clear);
        Repeat.onClick.AddListener(() => SceneManager.LoadScene(0));
        
        Exit.onClick.AddListener(() => Application.Quit());
    }

    void OnDestroy()
    {
        Repeat.onClick.RemoveAllListeners();
        Exit.onClick.RemoveAllListeners();
    }
}
