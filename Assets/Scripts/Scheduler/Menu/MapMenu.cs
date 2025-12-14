using UnityEngine;
using UnityEngine.UI;

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

        [Header("Locations visual")]
        [SerializeField] private GameObject selectorGargenLocation;
        [SerializeField] private GameObject selectorGreenHousesLocation;
        [SerializeField] private GameObject selectorFieldLocation;

        void Start()
        {
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
        }

        private void SelectLocation(Location location)
        {
            if (location == LocationData.Location)
            {
                LocationData.Location = Location.None;
            }
            else
            {
                UpdateLocationsVisual(location);
                LocationData.Location = location;
            }
        }

        private void UpdateLocationsVisual(Location location)
        {
            if (LocationData.IsSelected)
            {
                DisableLocationSelector(LocationData.Location);
            }
            var selector = GetSelector(location);
            selector.SetActive(true);

        }

        private void DisableLocationSelector(Location location) => GetSelector(location).SetActive(false);

        private GameObject GetSelector(Location location) => location switch
        {
            Location.Garden => selectorGargenLocation,
            Location.Field => selectorFieldLocation,
            Location.Greenhouses => selectorGreenHousesLocation,
            _ => null
        };
    }
}