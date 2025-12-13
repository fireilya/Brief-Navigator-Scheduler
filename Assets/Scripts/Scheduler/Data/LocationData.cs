using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Scheduler
{
    public class LocationData : MonoBehaviour
    {
        public Location Location;
        public bool IsSelected => Location != Location.None;
        public string Name { get
            {
                return Location.ToString();
            }
        }

        private void ResetLocation() => Location = Location.None;

        void Start()
        {
            ResetLocation();
        }
    }
}