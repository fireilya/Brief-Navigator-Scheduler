using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Scheduler;
using Scheduler.Data;
using Scheduler.DataFlowMock.BriefMock;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DataFlowDemo.Navigator
{
    public class NavigatorUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text chosenTasks;
        [SerializeField] private ChooseElementToggle chooseElementPrefab;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private Button toScheduler;

        private List<ChooseElementToggle> _chooseElements = new();
        private HashSet<Guid> _processedTools = new();

        void Start()
        {
            chosenTasks.text = BuildChosenTasksMessage();

            foreach (var location in DataContainer.CurrentActionArea.Locations)
            {
                var newChooseElement = Instantiate(chooseElementPrefab, gridLayoutGroup.transform);
                newChooseElement.ChooseNote.text = location.Risk.Neutralizer.Name;
                newChooseElement.Toggle.onValueChanged.AddListener(value =>
                {
                    if (value) DataContainer.FoundRisksNeutralizers.Add(location.Risk.Neutralizer);
                    else DataContainer.FoundRisksNeutralizers.Remove(location.Risk.Neutralizer);
                });
                _chooseElements.Add(newChooseElement);

                foreach (var task in location.Tasks)
                {
                    if (!DataContainer.ChosenTasks.Contains(task)) continue;
                    foreach (var subtask in task.Subtasks)
                    {
                        if (subtask.IsUseCapacityTool)
                        {
                            var capacitySubtask = (CapacitySubtask)subtask;
                            foreach (var toolId in capacitySubtask.ToolsCapacity.Keys)
                            {
                                if (_processedTools.Contains(toolId)) continue;
                                newChooseElement = Instantiate(chooseElementPrefab, gridLayoutGroup.transform);
                                newChooseElement.ChooseNote.text = DBServerMock.GetTool(toolId).Name;
                                newChooseElement.Toggle.onValueChanged.AddListener(value =>
                                {
                                    if (value) DataContainer.FoundToolsIds.Add(toolId);
                                    else DataContainer.FoundToolsIds.Remove(toolId);
                                });
                                _chooseElements.Add(newChooseElement);
                                _processedTools.Add(toolId);
                            }
                            continue;
                        }

                        var processSubTask = (ProcessSubtask)subtask;
                        
                        if (processSubTask.NeededToolId == Guid.Empty ||
                            _processedTools.Contains(processSubTask.NeededToolId)) continue;
                        
                        newChooseElement = Instantiate(chooseElementPrefab, gridLayoutGroup.transform);
                        newChooseElement.ChooseNote.text = DBServerMock.GetTool(processSubTask.NeededToolId).Name;
                        newChooseElement.Toggle.onValueChanged.AddListener(value =>
                        {
                            if (value) DataContainer.FoundToolsIds.Add(processSubTask.NeededToolId);
                            else DataContainer.FoundToolsIds.Remove(processSubTask.NeededToolId);
                        });
                        _chooseElements.Add(newChooseElement);
                        _processedTools.Add(processSubTask.NeededToolId);
                    }
                }
            }

            toScheduler.onClick.AddListener(OnToSchedulerClicked);
        }

        //
        // void OnDestroy()
        // {
        //     shovelToggle.onValueChanged.RemoveAllListeners();
        //     brushToggle.onValueChanged.RemoveAllListeners();
        //     bagToggle.onValueChanged.RemoveAllListeners();
        //     knifeToggle.onValueChanged.RemoveAllListeners();
        //     scissorsToggle.onValueChanged.RemoveAllListeners();
        //     wheelBarrowToggle.onValueChanged.RemoveAllListeners();
        //     toScheduler.onClick.RemoveAllListeners();
        //
        //     shovelToggle.gameObject.SetActive(false);
        //     brushToggle.gameObject.SetActive(false);
        //     bagToggle.gameObject.SetActive(false);
        //     knifeToggle.gameObject.SetActive(false);
        //     scissorsToggle.gameObject.SetActive(false);
        //     wheelBarrowToggle.gameObject.SetActive(false);
        // }
        //
        //
        private static string BuildChosenTasksMessage()
        {
            var sb = new StringBuilder();
            sb.Append("Задачи:\n");
            foreach (var task in DataContainer.ChosenTasks) sb.Append($"--{task.Name}\n");
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        //
        // private void TurnOnAvailableTools()
        // {
        //     foreach (var subtask in DataContainer.ChosenTasks.SelectMany(task => task.Subtasks))
        //     {
        //         switch (subtask.Tool.Name)
        //         {
        //             case "Лопата":
        //                 shovelToggle.gameObject.SetActive(true);
        //                 break;
        //             case "Кисть":
        //                 brushToggle.gameObject.SetActive(true);
        //                 break;
        //             case "Сумка":
        //                 bagToggle.gameObject.SetActive(true);
        //                 break;
        //             case "Нож":
        //                 knifeToggle.gameObject.SetActive(true);
        //                 break;
        //             case "Ножницы":
        //                 scissorsToggle.gameObject.SetActive(true);
        //                 break;
        //             case "Садовая тачка":
        //                 wheelBarrowToggle.gameObject.SetActive(true);
        //                 break;
        //         }
        //     }
        // }
        //
        // void OnShovelToggled(bool newValue)
        // {
        //     if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Shovel]);
        //     else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Shovel]);
        // }
        //
        // void OnBrushToggled(bool newValue)
        // {
        //     if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Brush]);
        //     else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Brush]);
        // }
        //
        // void OnBagToggled(bool newValue)
        // {
        //     if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Bag]);
        //     else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Bag]);
        // }
        //
        // void OnKnifeToggled(bool newValue)
        // {
        //     if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Knife]);
        //     else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Knife]);
        // }
        //
        // void OnScissorsToggled(bool newValue)
        // {
        //     if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Scissors]);
        //     else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Scissors]);
        // }
        //
        // void OnWheelBarrowToggled(bool newValue)
        // {
        //     if (newValue) DataContainer.FoundTools.Add(StaticGameData.Tools[(int)ToolId.Wheelbarrow]);
        //     else DataContainer.FoundTools.Remove(StaticGameData.Tools[(int)ToolId.Wheelbarrow]); 
        // }

        void OnToSchedulerClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}