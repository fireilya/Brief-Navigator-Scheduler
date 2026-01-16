using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler
{
    public class ConvertableButton : MonoBehaviour
    {
        public Button Button { get; private set; }
        public TMP_Text ButtonText { get; private set; }
        void Awake()
        {
            Button = GetComponent<Button>();
            ButtonText = GetComponentInChildren<TMP_Text>();
        }
    }
}
