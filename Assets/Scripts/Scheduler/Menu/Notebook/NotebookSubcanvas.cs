using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Notebook
{
    public abstract class NotebookSubcanvas : MonoBehaviour
    {
        public NotebookMenu ParentNotebook { get; set; }

        public List<GameObject> CreatedObjects = new();

        protected abstract void Awake();

        public virtual void Enable(bool withReinit)
        {
            gameObject.SetActive(true);
            if (withReinit) Reinit();
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }

        protected virtual void Clear()
        {
            foreach (var createdObject in CreatedObjects) Destroy(createdObject);
            CreatedObjects.Clear();
        }

        private protected abstract void Reinit();
    }
}