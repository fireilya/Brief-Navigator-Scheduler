using Domain.Scheduler;
using Scheduler.Bases;
using UnityEngine;

namespace Scheduler.Menu.Notebook
{
    public class EquipmentsMenu : MenuForDragableItemsBase
    {
        [SerializeField] private EquipmentVariant equipmentVariantPrefab;
        public void CreateItem(Neutralizer equipment)
        {
            var newEquipmentVariant = Instantiate(equipmentVariantPrefab);
            AddItem(newEquipmentVariant);
            newEquipmentVariant.Equipment = equipment;
        }
    }
}
