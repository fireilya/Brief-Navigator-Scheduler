using System.Linq;
using Scheduler.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resultMessage;
    [SerializeField] private Button Repeat;
    [SerializeField] private Button Exit;

    void Start()
    {
        Repeat.onClick.AddListener(DataContainer.Clear);
        Repeat.onClick.AddListener(() => SceneManager.LoadScene(0));
        Exit.onClick.AddListener(Application.Quit);
        CreateResultMessage();
    }

    private void CreateResultMessage()
    {
        var donePercent = DataContainer.CurrentActionArea.GetAllTasks()
            .Where(x => x.IsTrue)
            .Select(t => t.DoneFraction)
            .Average() * 100;
        
        resultMessage.SetText($"Квест \"Фермер\" выполнен на {donePercent:F2}%");
    }
}
