using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.DataFlowMock.BriefMock
{
    public class ChooseElementToggle : MonoBehaviour
    {
        public Toggle Toggle { get; private set; }
        public TMP_Text ChooseNote { get; private set; }
        void Awake()
        {
            Toggle = GetComponent<Toggle>();
            if (Toggle is null) throw new NullReferenceException("Ti ahuel blyat");
            ChooseNote = GetComponentInChildren<TMP_Text>();
        }
    }
}
