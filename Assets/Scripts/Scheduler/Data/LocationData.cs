using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Scheduler
{
    public class LocationData : MonoBehaviour
    {
        public Location location;
        public bool IsSelected => location != Location.None;
        public string Name => location switch
        {
            Location.Garden => "Огород",
            Location.Field => "Поле",
            Location.Greenhouses => "Теплицы",
            Location.None => "Не выбрано",
            _ => $"Неизвестная локация {(int)location}" 
        };

        private void ResetLocation() => location = Location.None;
        
        void Start()
        {
            ResetLocation();
        }
    }
}