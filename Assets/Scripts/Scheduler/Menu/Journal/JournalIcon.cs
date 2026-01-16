using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scheduler.Menu.Journal
{
    public abstract class JournalIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private HintPlane hintPlanePrefab;
        [SerializeField] private MyUnityTimer timerPrefab;
        [SerializeField] private double showHintInSeconds = 0.5f;
    
        protected HintPlane HintPlane;
        protected MyUnityTimer Timer;

        private Transform previouseParent;
        private int previouseSiblingIndex;
        
        public Canvas ParentCanvas { get; set; }
    
        public Image Icon {get; private set;}

        protected virtual void Awake()
        {
            Icon = GetComponent<Image>();
            HintPlane = Instantiate(hintPlanePrefab, transform);
            Timer = Instantiate(timerPrefab, transform);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Timer.OnFinish.RemoveAllListeners();
            Timer.OnFinish.AddListener(() => ShowHint(eventData.position));
            Timer.StartTimer(showHintInSeconds);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Timer.IsRunning) Timer.StopTimer();
            else
            {
                transform.SetParent(previouseParent);
                transform.SetSiblingIndex(previouseSiblingIndex);
                HintPlane.Hide();    
            }
        }

        protected virtual void ShowHint(Vector2 pointerPosition)
        {
            previouseParent =  transform.parent;
            previouseSiblingIndex = transform.GetSiblingIndex();
            transform.SetParent(ParentCanvas.transform);
            transform.SetAsLastSibling();
        }
    }
}
