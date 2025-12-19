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

        private LocationUI _currentChosenLocationUI = null;
        
        public LocationUI CurrentChosenLocation => _currentChosenLocationUI; 
        
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
            if (_currentChosenLocationUI is not null)
            {
                isDeselect = _currentChosenLocationUI == locationUI;
                _currentChosenLocationUI.IsSelected = false;
                var locationButtonColors = _currentChosenLocationUI.LocationButton.colors;
                locationButtonColors.normalColor = commonColor;
                _currentChosenLocationUI.LocationButton.colors = locationButtonColors;
                _currentChosenLocationUI = null;
            }

            if (!isDeselect)
            {
                _currentChosenLocationUI = locationUI;
                _currentChosenLocationUI.IsSelected = true;
                var buttonColors=  _currentChosenLocationUI.LocationButton.colors;
                buttonColors.normalColor = selectedColor;
                _currentChosenLocationUI.LocationButton.colors = buttonColors;   
            }
            
            UpdateChosenData();
        }
        
        private static string BuildLocationTasksMessage(Location location)
        {
            var sb = new StringBuilder();
            foreach (var task in DataContainer.ChosenTasksByLocation[location.Id]) sb.Append($"--{task.Name}\n");
            return sb.ToString();
        }

        private void UpdateChosenData()
        {
            locationName.text = _currentChosenLocationUI is null
                ? "Не выбрано"
                : _currentChosenLocationUI.LocationData.Name;
            
            locationTasks.text = _currentChosenLocationUI is null
                ? ""
                : BuildLocationTasksMessage(_currentChosenLocationUI.LocationData);
        }
    
        // Update is called once per frame
        private void OnDestroy()
        {
            foreach (var sprite in _loadedSprites) ImageServerMock.UnloadImage(sprite);
        }
    }
}
