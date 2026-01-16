using UnityEngine;
using UnityEngine.EventSystems;

namespace Scheduler.Interfaces
{
    public interface IShowHintHandler : IPointerEnterHandler, IPointerExitHandler
    {
        protected void ShowHint(Vector2 pointerPosition);
    }
}