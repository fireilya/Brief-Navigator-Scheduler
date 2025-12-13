using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Scheduler
{
    public class LocationData : MonoBehaviour
    {
        public Location Location;
        public bool IsSelected => Location != Location.None;
        public string Name => Location switch
        {
            Location.Garden => "Огород",
            Location.Field => "Поле",
            Location.Greenhouses => "Теплицы",
            Location.None => "Не выбрано",
            _ => $"Неизвестная локация {(int)Location}" 
        };

        private void ResetLocation() => Location = Location.None;
        
        void Start()
        {
            ResetLocation();
        }
    }
}