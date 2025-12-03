using System.Text;
using DataFlowDemo.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DataFlowDemo.Scheduler
{
    public class SchedulerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text tasksAndTools;
        [SerializeField] private TMP_Text schedulerInfo;
        [SerializeField] private Button toHome; 
        void Start()
        {
            tasksAndTools.text = BuildTasksAndToolsMessage();
            schedulerInfo.text = BuildSchedulerInfoMessage();
            toHome.onClick.AddListener(OnToHomeClicked);
        }

        void OnDestroy()
        {
            toHome.onClick.RemoveAllListeners();
        }
        string BuildTasksAndToolsMessage()
        {
            var sb = new StringBuilder();
            sb.Append("Задачи:\n");
            foreach (var task in DataContainer.ChosenTasks) sb.Append($"--{task.Name}\n");
            sb.Append("\nНайденные инструменты:\n");
            foreach (var tool in DataContainer.FoundTools) sb.Append($"--{tool.Name}\n");
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        string BuildSchedulerInfoMessage()
        {
            var sb = new StringBuilder();
            sb.Append("Состояние планировщика:\n\n");
            foreach (var task in DataContainer.ChosenTasks)
            {
                sb.Append($"Задача:\n");
                sb.Append($"--{task.Name}\n");
                sb.Append($"--Подзадачи:\n");
                foreach (var subtask in task.Subtasks)
                {
                    sb.Append($"----{subtask.Name}\n");
                    sb.Append($"----Найден инструмент({subtask.Tool.Name}): ");
                    sb.Append(DataContainer.FoundTools.Contains(subtask.Tool) ? "Да\n\n" : "Нет\n\n");
                }
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        void OnToHomeClicked()
        {
            DataContainer.Clear();
            SceneManager.LoadScene("Brief");
        }
    }
}
