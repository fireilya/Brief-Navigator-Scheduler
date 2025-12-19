using System.Collections.Generic;
using UnityEngine;

namespace Scheduler.Menu.Notebook
{
    public abstract class NotebookSubcanvas : MonoBehaviour
    {
        public NotebookCanvas ParentNotebook {get; set;}

        public List<GameObject> CreatedObjects = new();
        public abstract void Enable(bool isNeedInit);

        protected void Clear()
        {
            foreach (var createdObject in CreatedObjects) Destroy(createdObject);
            CreatedObjects.Clear();
        }

        public abstract void Init();
    }
}
