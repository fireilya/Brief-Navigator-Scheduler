using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scheduler.Menu
{
    public class MapMenu : MonoBehaviour
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
            {
                gardenButton.onClick.AddListener(() => SelectLocation(Location.Garden));
                selectorGargenLocation.SetActive(true);
            }
            if (greenHousesButton != null)
            {
                greenHousesButton.onClick.AddListener(() => SelectLocation(Location.Greenhouses));
                selectorGreenHousesLocation.SetActive(true);
            }
            if (fieldButton != null)
            {
                fieldButton.onClick.AddListener(() => SelectLocation(Location.Field));
                selectorFieldLocation.SetActive(true);
            }
        }

        public void SelectLocation(Location location)
        {
            LocationData.location = location;
        }
    }
}