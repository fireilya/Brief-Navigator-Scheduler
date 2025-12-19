using System;
using Domain.Scheduler;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LocationUI : MonoBehaviour
{
    [SerializeField] private int dataMappingKey;
    public Location LocationData { get; set; }
    public Button LocationButton { get; private set; }
    public Image LocationImage { get; private set; }
    public bool IsSelected { get; set; }
    public int DataMappingKey => dataMappingKey;
    void Awake()
    {
        LocationButton = GetComponent<Button>();
        LocationImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
