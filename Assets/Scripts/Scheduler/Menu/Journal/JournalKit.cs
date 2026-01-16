using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Journal
{
    public class JournalKit : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    
        public Image[] KitCells { get; private set; }
        void Awake()
        {
            KitCells = GetComponentsInChildren<Image>();
        }
    }
}
