using Domain.Scheduler;
using Shared;
using UnityEngine;

namespace Scheduler.Menu.Journal
{
    public class RiskJournalIcon : JournalIcon
    {
        private Risk _risk;

        public Risk Risk
        {
            get => _risk;
            set
            {
                _risk = value;
                Icon.sprite = _risk == null ? null : ImageServerMock.LoadImage(_risk.PathToIcon);
            }
        }

        protected override void ShowHint(Vector2 pointerPosition)
        {
            base.ShowHint(pointerPosition);
            HintPlane.Show(_risk.Description, pointerPosition);
        }
    }
}