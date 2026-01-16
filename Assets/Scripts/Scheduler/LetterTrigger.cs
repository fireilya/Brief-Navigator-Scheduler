using System;
using Scheduler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class LetterTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private MyUnityTimer timerPrefab;
    [SerializeField] private double showLetterInSeconds = .25;
    [SerializeField] private LetterDataMapper Letter;
    
    private MyUnityTimer _timer;

    private void Awake()
    {
        _timer = Instantiate(timerPrefab, transform);
        _timer.OnFinish.AddListener(() =>
        {
            Letter.gameObject.SetActive(true);
            Letter.RemapData();
        });
    }

    private void Start()
    {
        Letter.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _timer.StartTimer(showLetterInSeconds);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_timer.IsRunning) _timer.StopTimer();
        else
        {
            Letter.Clear();
            Letter.gameObject.SetActive(false); 
        }
    }
}
