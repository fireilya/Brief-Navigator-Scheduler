using Domain.Scheduler;
using Shared;

namespace Scheduler.Menu.Notebook
{
    public class EquipmentVariant : ItemVariant
    {
        private Neutralizer _equipment;

        public Neutralizer Equipment
        {
            get => _equipment;
            set
            {
                _equipment = value;
                Icon.sprite = ImageServerMock.LoadImage(_equipment?.PathToIcon);
            }
        }
    }
}