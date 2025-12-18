using Assets.Scripts.Scheduler;
using UnityEngine;

namespace Scheduler.Data
{
    public class LocationData : MonoBehaviour
    {
        public LocationType LocationType;
        public bool IsSelected => LocationType != LocationType.None;
        public string Name => LocationType switch
        {
            LocationType.Garden => "Огород",
            LocationType.Field => "Поле",
            LocationType.Greenhouses => "Теплицы",
            LocationType.House => "Дом",
            LocationType.None => "Не выбрано",
            _ => $"Неизвестная локация {(int)LocationType}" 
        };

        private void ResetLocation() => LocationType = LocationType.None;
        
        void Start()
        {
            ResetLocation();
        }
    }
}