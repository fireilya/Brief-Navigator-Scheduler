    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    namespace Assets.Scripts.Scheduler.Menu
    {
        public abstract class MenuBase : MonoBehaviour
        {
            [Header("Default menu settings")]
            [SerializeField] protected Canvas openedMenuObject;
            [SerializeField] protected Graphic darkBackground;
            [SerializeField] protected Button closeButton;

            protected virtual bool CanBeOpened => true;
            protected virtual bool CanBeClosed => true;

            public void Open()
            {
                if (CanBeOpened)
                {
                    openedMenuObject.gameObject.SetActive(true);
                    darkBackground.gameObject.SetActive(true);
                    closeButton.gameObject.SetActive(true);
                }
            }

            public void Close()
            {
                if (CanBeClosed)
                {
                    openedMenuObject.gameObject.SetActive(false);
                    darkBackground.gameObject.SetActive(false);
                    closeButton.gameObject.SetActive(false);
                }
            }
        }
    }