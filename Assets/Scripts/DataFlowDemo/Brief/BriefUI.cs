using DataFlowDemo.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DataFlowDemo.Brief
{
    public class BriefUI : MonoBehaviour
    {
        [SerializeField] private Toggle potatoTask;
        [SerializeField] private Toggle carrotTask;
        [SerializeField] private Toggle pumpkinTask;
        [SerializeField] private Button toNavigator;

        void Start()
        {
            potatoTask.onValueChanged.AddListener(OnPotatoTaskToggled);
            carrotTask.onValueChanged.AddListener(OnCarrotTaskToggled);
            pumpkinTask.onValueChanged.AddListener(OnPumpkinTaskToggled);
            toNavigator.onClick.AddListener(OnToNavigatorClicked);
        }

        void OnDestroy()
        {
            potatoTask.onValueChanged.RemoveAllListeners();
            carrotTask.onValueChanged.RemoveAllListeners();
            pumpkinTask.onValueChanged.RemoveAllListeners();
            toNavigator.onClick.RemoveAllListeners();
        }
        

        void OnPotatoTaskToggled(bool newValue)
        {
            if (newValue) DataContainer.ChosenTasks.Add(StaticGameData.Tasks[(int)TaskId.PotatoTask]);
            else  DataContainer.ChosenTasks.Remove(StaticGameData.Tasks[(int)TaskId.PotatoTask]);
        }

        void OnCarrotTaskToggled(bool newValue)
        {
            if (newValue) DataContainer.ChosenTasks.Add(StaticGameData.Tasks[(int)TaskId.CarrotTask]);
            else DataContainer.ChosenTasks.Remove(StaticGameData.Tasks[(int)TaskId.CarrotTask]);
        }

        void OnPumpkinTaskToggled(bool newValue)
        {
            if (newValue) DataContainer.ChosenTasks.Add(StaticGameData.Tasks[(int)TaskId.PumpkinsTask]);
            else DataContainer.ChosenTasks.Remove(StaticGameData.Tasks[(int)TaskId.PumpkinsTask]);
        }

        void OnToNavigatorClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
