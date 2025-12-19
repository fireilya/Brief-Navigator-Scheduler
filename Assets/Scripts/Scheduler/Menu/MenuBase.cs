using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu
    {
        public abstract class MenuBase : MonoBehaviour
        {
            [Header("Default menu settings")]
            [SerializeField] protected Canvas openedMenuObject;
            [SerializeField] protected Graphic darkBackground;
            [SerializeField] protected Button closeButton;

            protected virtual bool CanBeOpened => true;
            protected virtual bool CanBeClosed => true;

            void Start()
            {
                GetComponent<Button>().onClick.AddListener(Open);
                closeButton.onClick.AddListener(Close);
            }

            void OnDestroy()
            {
                GetComponent<Button>().onClick.RemoveAllListeners();
                closeButton.onClick.RemoveAllListeners();
            }

            public virtual void Open()
            {
                if (CanBeOpened)
                {
                    openedMenuObject.gameObject.SetActive(true);
                    darkBackground.gameObject.SetActive(true);
                    closeButton.gameObject.SetActive(true);
                }
            }

            public virtual void Close()
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