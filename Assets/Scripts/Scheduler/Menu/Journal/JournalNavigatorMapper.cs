using System.Collections.Generic;
using Scheduler.Data;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Scheduler.Menu.Journal
{
    public class JournalNavigatorMapper : MonoBehaviour
    {
        [SerializeField] private Canvas mainCanvas;
        
        [SerializeField] private JournalKit toolKit;
        [SerializeField] private JournalKit equipmentKit;
        [SerializeField] private JournalKit riskKit;

        [SerializeField] private ToolJournalIcon toolJournalIconPrefab;
        [SerializeField] private EquipmentJournalIcon equipmentJournalIconPrefab;
        [SerializeField] private RiskJournalIcon riskJournalIconPrefab;

        void Start()
        {
            var slotIndex = 0;
            foreach (var id in DataContainer.FoundToolsIds)
            {
                var cellGameObject = toolKit.KitCells[slotIndex++].gameObject;
                var toolIcon = Instantiate(toolJournalIconPrefab, cellGameObject.transform);
                toolIcon.Tool = DBServerMock.GetTool(id);
                toolIcon.ParentCanvas = mainCanvas;
            }

            slotIndex = 0;

            foreach (var neutralizerId in DataContainer.FoundRisksNeutralizerIds)
            {
                var cellGameObject = equipmentKit.KitCells[slotIndex++].gameObject;
                var equipmentIcon = Instantiate(equipmentJournalIconPrefab, cellGameObject.transform);
                equipmentIcon.Equipment = DBServerMock.GetNeutralizer(neutralizerId);
                equipmentIcon.ParentCanvas = mainCanvas;
            }

            slotIndex = 0;

            foreach (var location in DataContainer.CurrentActionArea.Locations)
            {
                var cellGameObject = riskKit.KitCells[slotIndex++].gameObject;
                var riskIcon = Instantiate(riskJournalIconPrefab, cellGameObject.transform);
                riskIcon.Risk = location.Risk;
                riskIcon.ParentCanvas = mainCanvas;
            }
        }
        
    }
}