using Domain.Scheduler;
using Scheduler.Menu.Notebook;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scheduler.Menu
{
    public class NotebookMenu : MenuBase
    {
        [SerializeField] private MapDataMapper mapper;
        [SerializeField] private TimeFlowController timeFlowController;
        public TimeFlowController TimeFlowController => timeFlowController;

        public Location SelectedLocation => mapper.CurrentChosenLocation?.LocationData;
        public GameTask SelectedTask { get; set; } = null;
        public Subtask SelectedSubtask { get; set; } = null;

        private NotebookSubcanvas[] _childrens;

        private int state = 0;
        

        protected override void Awake()
        {
            base.Awake();
            _childrens = GetComponentsInChildren<NotebookSubcanvas>(true);
            foreach (var children in _childrens) children.ParentNotebook = this;
        }

        public override void Open()
        {
            if (!mapper.IsLocationSelected) return;
            base.Open();
            Init();
        }

        private void Init()
        {
            foreach (var children in _childrens) children.gameObject.SetActive(false);
            state = 0;
            _childrens[state].Enable(true);
        }

        public void Next()
        {
            if (state == _childrens.Length - 1) return;
            _childrens[state].Disable();
            _childrens[++state].Enable(true);
        }

        public void Previous()
        {
            if (state == 0) return;
            _childrens[state].Disable();
            _childrens[--state].Enable(false);
        }

        public void Done()
        {
            _childrens[state].Disable();
            state = 0;
            Close();
        }
    }
}