using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Bases
{
    public abstract class MenuForDragableItemsBase : MonoBehaviour
    {
        private LayoutGroup _menuLayout;
        private readonly Dictionary<DraggableMenuItemBase, int> _itemsSiblingIndex = new();

        public DraggableMenuItemBase[] Items => GetComponentsInChildren<DraggableMenuItemBase>();

        protected virtual void Awake()
        {
            _menuLayout = GetComponentInChildren<LayoutGroup>();
        }

        public virtual void AddItem(DraggableMenuItemBase item)
        {
            if (!item) return;
            item.transform.SetParent(_menuLayout.transform);
            _itemsSiblingIndex.Add(item, _itemsSiblingIndex.Count);
        }

        public virtual void ReturnItem(DraggableMenuItemBase item)
        {
            if (!item) return;
            if (!_itemsSiblingIndex.ContainsKey(item))
            {
                AddItem(item);
                return;
            }

            item.transform.SetParent(_menuLayout.transform);
            item.transform.SetSiblingIndex(_itemsSiblingIndex[item]);
        }

        public void Clear()
        {
            foreach (var item in Items) Destroy(item.gameObject);
            _itemsSiblingIndex.Clear();
        }
    }
}