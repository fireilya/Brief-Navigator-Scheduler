using UnityEngine;
using TMPro;
using Assets.Scripts.Scheduler.Menu.UI;
using UnityEngine.UI;


namespace Assets.Scripts.Scheduler.Menu
{
    public class MapMenu : MenuBase
    {
        [Header("Data")]
        [SerializeField] private LocationData LocationData;

        [Header("Location Buttons")]
        [SerializeField] private LocationUI gardenUI;
        [SerializeField] private LocationUI greenHousesUI;
        [SerializeField] private LocationUI fieldUI;
        [SerializeField] private LocationUI houseUI;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI locationText;

        void Start()
        {
            DisableLocationSelector(Location.Garden);
            DisableLocationSelector(Location.Greenhouses);
            DisableLocationSelector(Location.Field);
            DisableLocationSelector(Location.House);

            SetUpLocationUI(Location.Garden, gardenUI);
            SetUpLocationUI(Location.Greenhouses, greenHousesUI);
            SetUpLocationUI(Location.Field, fieldUI);
            SetUpLocationUI(Location.House, houseUI);

            UpdateLocationText();
        }

        private void SelectLocation(Location location)
        {
            if (location == LocationData.Location)
            {
                DisableLocationSelector(LocationData.Location);
                LocationData.Location = Location.None;
            }
            else
            {
                UpdateLocationsVisual(location);
                LocationData.Location = location;
            }
            UpdateLocationText();
        }

        private void UpdateLocationsVisual(Location location)
        {
            if (LocationData.IsSelected)
            {
                DisableLocationSelector(LocationData.Location);
            }
            ActivateLocationSelector(location);
        }

        private void UpdateLocationText()
        {
            locationText.text = LocationData.Name.ToString();
        }

        private void DisableLocationSelector(Location location) => GetSelector(location).SetActive(false);
        private void ActivateLocationSelector(Location location) => GetSelector(location).SetActive(true);

        private GameObject GetSelector(Location location) => location switch
        {
            Location.Garden => gardenUI.selectedLocationVisual,
            Location.Field => fieldUI.selectedLocationVisual,
            Location.Greenhouses => greenHousesUI.selectedLocationVisual,
            Location.House => houseUI.selectedLocationVisual,
            _ => null
        };

        private void SetUpLocationUI(Location location, LocationUI locationUI)
        {
            if (locationUI.NonSelectedLocationButton != null)
                locationUI.NonSelectedLocationButton.onClick.AddListener(() =>
                {
                    SelectLocation(location);
                });
            if (locationUI.SelectedLocationButton != null)
            {
                locationUI.SelectedLocationButton.onClick.AddListener(() =>
                {
                    SelectLocation(location);
                });
            }
        }
    }
}