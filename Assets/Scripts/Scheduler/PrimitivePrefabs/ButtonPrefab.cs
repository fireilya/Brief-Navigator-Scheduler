using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Prefabs
{
    public class ButtonPrefab : MonoBehaviour
    {
        public Button Button { get; private set; }
        public TMP_Text Text { get; private set; }
        public Image Image { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
            Image = GetComponent<Image>();
            Text = GetComponentInChildren<TMP_Text>();
        }
    }
}
