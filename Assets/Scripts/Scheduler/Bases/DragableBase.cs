using Scheduler.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scheduler.Bases
{
    public abstract class DragableBase : MonoBehaviour, IDragable
    {
        public Canvas Canvas { get; protected set; }
        public CanvasGroup CanvasGroup { get; protected set; }
        public RectTransform RectTransform { get; protected set; }


        protected virtual void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
            RectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Start()
        {
            Canvas = GetComponentInParent<Canvas>();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            CanvasGroup.blocksRaycasts = false;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            RectTransform.anchoredPosition += eventData.delta * Canvas.scaleFactor;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            CanvasGroup.blocksRaycasts = true;
        }
    }
}