using Scheduler.Data;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Domain.Scheduler;


namespace Assets.Scripts.Scheduler.Menu
{
    public class MapMenu : MenuBase
    {
        [Header("Data")]
        [SerializeField] private LocationData LocationData;

        [Header("Location Buttons")]
        [SerializeField] private Button gardenButton;
        [SerializeField] private Button greenHousesButton;
        [SerializeField] private Button fieldButton;
        [SerializeField] private Button houseButton;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI locationText;

        void Start()
        {
            SetUpLocationUI(LocationType.Garden, gardenButton);
            SetUpLocationUI(LocationType.Greenhouses, greenHousesButton);
            SetUpLocationUI(LocationType.Field, fieldButton);
            SetUpLocationUI(LocationType.House, houseButton);

            UpdateLocationText();
        }

        private void SelectLocation(LocationType location)
        {

            LocationData.LocationType = location;
            UpdateLocationText();
        }

        private void UpdateLocationText()
        {
            locationText.text = LocationData.Name;
        }

        private void SetUpLocationUI(LocationType location, Button locationButton)
        {
            if (locationButton != null)
                locationButton.onClick.AddListener(() =>
                {
                    SelectLocation(location);
                });
        }

        void OnDestroy()
        {
            gardenButton.onClick.RemoveAllListeners();
            greenHousesButton.onClick.RemoveAllListeners();
            fieldButton.onClick.RemoveAllListeners();
            houseButton.onClick.RemoveAllListeners();
        }
    }
}