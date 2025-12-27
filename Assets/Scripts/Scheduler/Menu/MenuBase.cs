using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu
    {
        public abstract class MenuBase : MonoBehaviour
        {
            [SerializeField] protected Button closeButton;

            protected virtual void Awake()
            {
                closeButton.onClick.AddListener(Close);
            }
            
            public virtual void Open()
            {
                gameObject.SetActive(true);
            }

            public virtual void Close()
            {
                gameObject.SetActive(false);
            }
        }
    }