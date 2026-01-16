using Domain.Scheduler;
using Shared;
using UnityEngine;

namespace Scheduler.Menu.Journal
{
    public class EquipmentJournalIcon : JournalIcon
    {
        private Neutralizer _equipment;

        public Neutralizer Equipment
        {
            get => _equipment;
            set
            {
                _equipment = value;
                Icon.sprite = _equipment == null ? null : ImageServerMock.LoadImage(_equipment.PathToIcon);
            }
        }

        protected override void ShowHint(Vector2 pointerPosition)
        {
            base.ShowHint(pointerPosition);
            HintPlane.Show(_equipment.Name, pointerPosition);
        }
    }
}