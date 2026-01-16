using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Scheduler;
using Scheduler.Data;
using Shared;
using TMPro;
using UnityEngine;

namespace Scheduler.Menu
{
    public class MapDataMapper : MonoBehaviour
    {
        [SerializeField] private TMP_Text locationName;
        [SerializeField] private TMP_Text locationTasks;
        
        [SerializeField] private Color commonColor;
        [SerializeField] private Color selectedColor;
    
        private LocationUI[] _locationUIElements;
        private List<Sprite> _loadedSprites =  new();
        
        public LocationUI CurrentChosenLocation { get; private set; } 
        public bool IsLocationSelected => CurrentChosenLocation;
        
        private void Awake()
        {
            _locationUIElements = GetComponentsInChildren<LocationUI>()
                .OrderBy(x => x.DataMappingKey)
                .ToArray();
        }

        void Start()
        {
            var sortedLocations = 
                DataContainer.CurrentActionArea.Locations
                    .OrderBy(x => x.MappingKey)
                    .ToArray();

            for (var i = 0; i < sortedLocations.Length; i++)
            {
                var buttonSprite = ImageServerMock.LoadImage(sortedLocations[i].PathToIcon);
                _loadedSprites.Add(buttonSprite);
                _locationUIElements[i].LocationImage.sprite = buttonSprite;
                _locationUIElements[i].LocationData = sortedLocations[i];
                var locationIndex = i;
                _locationUIElements[i].LocationButton.onClick.AddListener(() => 
                    OnLocationUIClicked(_locationUIElements[locationIndex], sortedLocations[locationIndex]));
                
            }
            UpdateChosenData();
        }

        void OnLocationUIClicked(LocationUI locationUI, Location locationData)
        {
            var isDeselect = false;
            if (CurrentChosenLocation is not null)
            {
                isDeselect = CurrentChosenLocation == locationUI;
                CurrentChosenLocation.IsSelected = false;
                var locationButtonColors = CurrentChosenLocation.LocationButton.colors;
                locationButtonColors.normalColor = commonColor;
                CurrentChosenLocation.LocationButton.colors = locationButtonColors;
                CurrentChosenLocation = null;
            }

            if (!isDeselect)
            {
                CurrentChosenLocation = locationUI;
                CurrentChosenLocation.IsSelected = true;
                var buttonColors=  CurrentChosenLocation.LocationButton.colors;
                buttonColors.normalColor = selectedColor;
                CurrentChosenLocation.LocationButton.colors = buttonColors;   
            }
            
            UpdateChosenData();
        }
        
        private static string BuildLocationTasksMessage(Location location)
        {
            var sb = new StringBuilder();
            sb.Append("Задачи:\n");
            foreach (var task in DataContainer.ChosenTasksByLocation[location.Id]) sb.Append($"--{task.Name}\n");
            sb.Append("\nРиски:\n");
            sb.Append($"--{location.Risk.Name}");
            return sb.ToString();
        }

        private void UpdateChosenData()
        {
            locationName.text = CurrentChosenLocation is null
                ? "Не выбрано"
                : CurrentChosenLocation.LocationData.Name;
            
            locationTasks.text = CurrentChosenLocation is null
                ? ""
                : BuildLocationTasksMessage(CurrentChosenLocation.LocationData);
        }
    
        // Update is called once per frame
        private void OnDestroy()
        {
            foreach (var sprite in _loadedSprites) ImageServerMock.UnloadImage(sprite);
        }
    }
}
