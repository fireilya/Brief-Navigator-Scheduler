using System.Collections.Generic;
using NUnit.Framework;
using Scheduler.Data;
using Shared;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scheduler.DataFlowMock.BriefMock
{
    public class BriefUI : MonoBehaviour
    {
        [SerializeField] private ChooseElementToggle chooseElementTogglePrefab;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private Button toNavigator;

        private List<ChooseElementToggle> _chooseElements = new List<ChooseElementToggle>();

        void Awake()
        {
            if (!DBServerMock.IsInited) DBServerMock.Init();
        }

        void Start()
        {
            var questActionArea = DBServerMock.GetFirstActionArea();
            DataContainer.CurrentActionArea = questActionArea;
            foreach (var location in questActionArea.Locations)
            {
                foreach (var task in location.Tasks)
                {
                    var newChooseElementToggle = Instantiate(chooseElementTogglePrefab, gridLayoutGroup.transform);
                    newChooseElementToggle.Toggle.onValueChanged.AddListener(value =>
                        {
                            if (value) DataContainer.ChosenTasks.Add(task);
                            else DataContainer.ChosenTasks.Remove(task);
                        }
                    );
                    newChooseElementToggle.ChooseNote.text = task.Name;
                    _chooseElements.Add(newChooseElementToggle);
                }
            }

            toNavigator.onClick.AddListener(OnToNavigatorClicked);
        }

        void OnDestroy()
        {
            foreach (var el in _chooseElements) el.Toggle.onValueChanged.RemoveAllListeners();
        }

        void OnToNavigatorClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}