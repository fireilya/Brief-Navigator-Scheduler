using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
        [SerializeField] private Button selectorgardenButton;
        [SerializeField] private Button selectorgreenHousesButton;
        [SerializeField] private Button selectorfieldButton;
        [SerializeField] private Button selectorhouseButton;

        [Header("Locations visual")]
        [SerializeField] private GameObject selectorGargenLocation;
        [SerializeField] private GameObject selectorGreenHousesLocation;
        [SerializeField] private GameObject selectorFieldLocation;
        [SerializeField] private GameObject selectorHouseLocation;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI locationText;

        void Start()
        {
                DisableLocationSelector(Location.Garden);
                DisableLocationSelector(Location.Greenhouses);
                DisableLocationSelector(Location.Field);
                DisableLocationSelector(Location.House);

            if (gardenButton != null)
                gardenButton.onClick.AddListener(() =>
                {
                    SelectLocation(Location.Garden);
                });
            if (greenHousesButton != null)
            {
                greenHousesButton.onClick.AddListener(() =>
                {
                    SelectLocation(Location.Greenhouses);
                });
            }
            if (fieldButton != null)
            {
                fieldButton.onClick.AddListener(() =>
                {
                    SelectLocation(Location.Field);
                });
            }
            if (houseButton != null)
                houseButton.onClick.AddListener(() =>
                {
                    SelectLocation(Location.House);
                });
            if (selectorgardenButton != null)
                selectorgardenButton.onClick.AddListener(() =>
                {
                    SelectLocation(Location.Garden);
                });
            if (selectorgreenHousesButton != null)
            {
                selectorgreenHousesButton.onClick.AddListener(() =>
                {
                    SelectLocation(Location.Greenhouses);
                });
            }
            if (selectorfieldButton != null)
            {
                selectorfieldButton.onClick.AddListener(() =>
                {
                    SelectLocation(Location.Field);
                });
            }
            if (selectorhouseButton != null)
                selectorhouseButton.onClick.AddListener(() =>
                {
                    SelectLocation(Location.House);
                });

            UpdateLocationText();
        }

        private void SelectLocation(Location location)
        {
            if (location == LocationData.Location)
            {
                DisableLocationSelector(LocationData.Location);
                LocationData.Location = Location.None;
                UpdateLocationText();
            }
            else
            {
                UpdateLocationsVisual(location);
                LocationData.Location = location;
                UpdateLocationText();
            }
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
            Location.Garden => selectorGargenLocation,
            Location.Field => selectorFieldLocation,
            Location.Greenhouses => selectorGreenHousesLocation,
            Location.House => selectorHouseLocation,
            _ => null
        };
    }
}