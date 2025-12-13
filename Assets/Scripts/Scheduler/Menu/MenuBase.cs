using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Scheduler.Menu
{
    public abstract class MenuBase : MonoBehaviour
    {
        [Header("Default menu settings")]
        [SerializeField] protected GameObject openedMenuObject;
        [SerializeField] protected GameObject darkBackground;
        [SerializeField] protected GameObject closeButton;

        protected virtual bool CanBeOpened => true;
        protected virtual bool CanBeClosed => true;

        public void Open()
        {
            if (CanBeOpened)
            {
                openedMenuObject.SetActive(true);
                darkBackground.SetActive(true);
                closeButton.SetActive(true);
            }
        }

        public void Close()
        {
            if (CanBeClosed)
            {
                openedMenuObject.SetActive(false);
                darkBackground.SetActive(false);
                closeButton.SetActive(false);
            }
        }
    }
}