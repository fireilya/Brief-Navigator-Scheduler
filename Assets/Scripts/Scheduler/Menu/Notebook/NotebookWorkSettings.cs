using System;
using System.Linq;
using Scheduler.Data;
using Scheduler.Factory;
using Scheduler.Wrappers;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Notebook
{
    public class NotebookWorkSettings : NotebookSubcanvas
    {
        [SerializeField] private TMP_Text subtaskHeader;

        [SerializeField] private NotebookSubtaskAmountControl processSubtaskAmountControl;
        [SerializeField] private NotebookSubtaskAmountControl capacitySubtaskAmountControl;

        [SerializeField] private TaskCommiter taskCommiter;

        [SerializeField] private Button backButton;
        [SerializeField] private Button doneButton;

        private ToolsMenu _toolsMenu;
        private EquipmentsMenu _equipmentMenu;
        private ToolSlot _toolSlot;
        private EquipmentSlot _equipmentSlot;
        private NotebookSubtaskAmountControl _currentSubtaskAmountControl;


        protected override void Awake()
        {
            _toolsMenu = GetComponentInChildren<ToolsMenu>();
            _equipmentMenu = GetComponentInChildren<EquipmentsMenu>();
            _toolSlot = GetComponentInChildren<ToolSlot>();
            _equipmentSlot = GetComponentInChildren<EquipmentSlot>();

            processSubtaskAmountControl.gameObject.SetActive(false);
            capacitySubtaskAmountControl.gameObject.SetActive(false);

            ConfigureButtons();
            ConfigureSlots();
        }

        private void ConfigureButtons()
        {
            doneButton.interactable = false;
            doneButton.onClick.AddListener(() =>
            {
                taskCommiter.CommitSubtask(
                    _currentSubtaskAmountControl.SubtaskWrapper,
                    _currentSubtaskAmountControl.TotalHours);
                ParentNotebook.Done();
            });
            backButton.onClick.AddListener(ParentNotebook.Previous);
        }

        private void ConfigureSlots()
        {
            _toolSlot.onToolAttached.AddListener(tool =>
            {
                _currentSubtaskAmountControl.SubtaskWrapper.ChosenToolId = tool.Tool.Id;
                _toolSlot.SetSlotLabel(_currentSubtaskAmountControl.SubtaskWrapper.SubtaskToolInfo);
                _currentSubtaskAmountControl.UpdateWorkInfoLabels();
            });

            _toolSlot.OnDetached.AddListener(_ =>
            {
                _currentSubtaskAmountControl.SubtaskWrapper.ResetChosenTool();
                _toolSlot.SetSlotLabel(_currentSubtaskAmountControl.SubtaskWrapper.SubtaskToolInfo);
                _currentSubtaskAmountControl.UpdateWorkInfoLabels();
            });

            _equipmentSlot.OnEquipmentAttached.AddListener(equipment =>
            {
                _currentSubtaskAmountControl.SubtaskWrapper.RiskNeutralizer = equipment.Equipment;
                _equipmentSlot.SetSlotLabel(equipment.Equipment.Name);
            });

            _equipmentSlot.OnDetached.AddListener(_ =>
            {
                _currentSubtaskAmountControl.SubtaskWrapper.RiskNeutralizer = null;
                _equipmentSlot.ClearSlotLabel();
            });
        }

        public override void Enable(bool withReinit)
        {
            base.Enable(withReinit);
            _toolSlot.SetSlotLabel(_currentSubtaskAmountControl.SubtaskWrapper.SubtaskToolInfo);
        }

        public override void Disable()
        {
            ClearSlotsAndAmountController();
            _currentSubtaskAmountControl.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void ClearSlotsAndAmountController()
        {
            if (_toolSlot.HasAttach) _toolsMenu.ReturnItem(_toolSlot.Detach());
            if (_equipmentSlot.HasAttach) _equipmentMenu.ReturnItem(_equipmentSlot.Detach());
            _currentSubtaskAmountControl?.Clear();
        }

        protected override void Clear()
        {
            base.Clear();
            ClearSlotsAndAmountController();
            _toolsMenu.Clear();
            _equipmentMenu.Clear();
        }

        private protected override void Reinit()
        {
            Clear();

            processSubtaskAmountControl.gameObject.SetActive(false);
            capacitySubtaskAmountControl.gameObject.SetActive(false);
            
            _currentSubtaskAmountControl = ParentNotebook.SelectedSubtask.IsUseCapacityTool
                ? capacitySubtaskAmountControl
                : processSubtaskAmountControl;

            _currentSubtaskAmountControl.gameObject.SetActive(true);

            _currentSubtaskAmountControl.SubtaskWrapper =
                SubtaskWrapperFactory.CreateSubtaskWrapper(ParentNotebook.SelectedSubtask);

            _currentSubtaskAmountControl.onWorkHoursChanged.AddListener(newHours =>
                doneButton.interactable = newHours != 0);

            subtaskHeader.text = ParentNotebook.SelectedSubtask.Name;
            _currentSubtaskAmountControl.UpdateWorkInfoLabels();
            
            var relevantTools = ParentNotebook.SelectedSubtask.IsUseCapacityTool
                ? DataContainer.FoundToolsIds.Where(x => DBServerMock.GetAllCapacityToolId.Contains(x))
                : DataContainer.FoundToolsIds.Where(x => !DBServerMock.GetAllCapacityToolId.Contains(x));
            
            foreach (var toolId in relevantTools) _toolsMenu.CreateItem(DBServerMock.GetTool(toolId));
            foreach (var neutralizer in DataContainer.FoundRisksNeutralizers) _equipmentMenu.CreateItem(neutralizer);
        }
    }
}