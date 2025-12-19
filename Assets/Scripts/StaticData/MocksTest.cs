using System;
using System.Linq;
using Shared;
using UnityEngine;
using UnityEngine.UI;

public class MocksTest : MonoBehaviour
{
    [SerializeField] private Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DBServerMock.Init();
        var actionArea = DBServerMock.GetFirstActionArea();
        image.sprite = ImageServerMock.LoadImage(actionArea.PathToTexture);
        if (!image.sprite) throw new NullReferenceException();
    }

    void OnDestroy()
    {
        ImageServerMock.UnloadImage(image.sprite);
    }

    // Update is called once per frame
    void Update()
    {
    }
}