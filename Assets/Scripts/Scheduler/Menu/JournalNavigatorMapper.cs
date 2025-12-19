using System.Collections.Generic;
using Scheduler.Data;
using Shared;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JournalNavigatorMapper : MonoBehaviour
{
    [SerializeField] private JournalKit toolKit;
    [SerializeField] private JournalKit equipmentKit;
    [SerializeField] private JournalKit riskKit;

    private List<Sprite> _loadedSprites = new();
    private bool _isInfoMapped;

    void Start()
    {
        if (_isInfoMapped) return;
        var slotIndex = 0;
        foreach (var id in DataContainer.FoundToolsIds)
        {
            var cellGameObject = toolKit.KitCells[slotIndex++].gameObject;
            var toolImage = Instantiate(cellGameObject, cellGameObject.transform).GetComponent<Image>();
            toolImage.sprite = ImageServerMock.LoadImage(DBServerMock.GetTool(id).PathToIcon);
            _loadedSprites.Add(toolImage.sprite);
        }

        slotIndex = 0;

        foreach (var neutralizer in DataContainer.FoundRisksNeutralizes)
        {
            var cellGameObject = equipmentKit.KitCells[slotIndex++].gameObject;
            var equipment = Instantiate(cellGameObject, cellGameObject.transform).GetComponent<Image>();
            equipment.sprite = ImageServerMock.LoadImage(neutralizer.PathToIcon);
            _loadedSprites.Add(equipment.sprite);
        }

        slotIndex = 0;

        foreach (var location in DataContainer.CurrentActionArea.Locations)
        {
            var cellGameObject = riskKit.KitCells[slotIndex++].gameObject;
            var riskImage = Instantiate(cellGameObject, cellGameObject.transform).GetComponent<Image>();
            riskImage.sprite = ImageServerMock.LoadImage(location.Risk.PathToIcon);
            _loadedSprites.Add(riskImage.sprite);
        }

        _isInfoMapped = true;
    }

    void OnDestroy()
    {
        foreach (var sprite in _loadedSprites) ImageServerMock.UnloadImage(sprite);
    }
}