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
        var allTasks = DataContainer.CurrentActionArea.GetAllTasks();
        var trueTasks = allTasks.Where(x => x.IsTrue).ToArray();
        var donePercent = (int)((float)trueTasks.Count(x => x.IsDone) / trueTasks.Length * 100);
        resultMessage.SetText($"Квест \"Фермер\" выполнен на {donePercent}%");
    }
}
