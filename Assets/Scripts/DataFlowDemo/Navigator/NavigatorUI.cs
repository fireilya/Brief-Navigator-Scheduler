using System.Linq;
using System.Text;
using DataFlowDemo.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DataFlowDemo.Navigator
{
    public class NavigatorUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text chosenTasks;
    
        [SerializeField] private Toggle shovelToggle;
        [SerializeField] private Toggle brushToggle;
        [SerializeField] private Toggle bagToggle;
        [SerializeField] private Toggle knifeToggle;
        [SerializeField] private Toggle scissorsToggle;
        [SerializeField] private Toggle wheelBarrowToggle;

        [SerializeField] private Button toScheduler;

        void Start()
        {
            chosenTasks.text = BuildChosenTasksMessage();
        
            shovelToggle.onValueChanged.AddListener(OnShovelToggled);
            brushToggle.onValueChanged.AddListener(OnBrushToggled);
            bagToggle.onValueChanged.AddListener(OnBagToggled);
            knifeToggle.onValueChanged.AddListener(OnKnifeToggled);
            scissorsToggle.onValueChanged.AddListener(OnScissorsToggled);
            wheelBarrowToggle.onValueChanged.AddListener(OnWheelBarrowToggled);
        
            toScheduler.onClick.AddListener(OnToSchedulerClicked);
        
            TurnOnAvailableTools();
        }

        void OnDestroy()
        {
            shovelToggle.onValueChanged.RemoveAllListeners();
            brushToggle.onValueChanged.RemoveAllListeners();
            bagToggle.onValueChanged.RemoveAllListeners();
            knifeToggle.onValueChanged.RemoveAllListeners();
            scissorsToggle.onValueChanged.RemoveAllListeners();
            wheelBarrowToggle.onValueChanged.RemoveAllListeners();
            toScheduler.onClick.RemoveAllListeners();
        
            shovelToggle.gameObject.SetActive(false);
            brushToggle.gameObject.SetActive(false);
            bagToggle.gameObject.SetActive(false);
            knifeToggle.gameObject.SetActive(false);
            scissorsToggle.gameObject.SetActive(false);
            wheelBarrowToggle.gameObject.SetActive(false);
        }
    

        private string BuildChosenTasksMessage()
        {
            var sb = new StringBuilder();
            sb.Append("Задачи:\n");
            foreach (var task in DataContainer.ChosenTasks) sb.Append($"--{task.Name}\n");
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();   
        }

        private void TurnOnAvailableTools()
        {
            foreach (var subtask in DataContainer.ChosenTasks.SelectMany(task => task.Subtasks))
            {
                switch (subtask.Tool.Name)
                {
                    case "Лопата":
                        shovelToggle.gameObject.SetActive(true);
                        break;
                    case "Кисть":
                        brushToggle.gameObject.SetActive(true);
                        break;
                    case "Сумка":
                        bagToggle.gameObject.SetActive(true);
                        break;
                    case "Нож":
                        knifeToggle.gameObject.SetActive(true);
                        break;
                    case "Ножницы":
                        scissorsToggle.gameObject.SetActive(true);
                        break;
                    case "Садовая тачка":
                        wheelBarrowToggle.gameObject.SetActive(true);
                        break;
                }
            }
        }

        void OnShovelToggled(bool newValue)
        {
            if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Shovel]);
            else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Shovel]);
        }

        void OnBrushToggled(bool newValue)
        {
            if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Brush]);
            else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Brush]);
        }

        void OnBagToggled(bool newValue)
        {
            if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Bag]);
            else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Bag]);
        }

        void OnKnifeToggled(bool newValue)
        {
            if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Knife]);
            else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Knife]);
        }

        void OnScissorsToggled(bool newValue)
        {
            if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Scissors]);
            else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Scissors]);
        }

        void OnWheelBarrowToggled(bool newValue)
        {
            if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Wheelbarrow]);
            else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Wheelbarrow]); 
        }

        void OnToSchedulerClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    
    }
}