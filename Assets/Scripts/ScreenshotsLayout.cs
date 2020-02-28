using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotsLayout : MonoBehaviour
{
    [SerializeField] private ScreenshotView _template;
    [SerializeField] private Transform _container;

    [SerializeField] private Sprite _ImageOne;
    [SerializeField] private Sprite _ImageTwo;
    [SerializeField] private Sprite _ImageThree;
    [SerializeField] private Sprite _ImageFour;

    [SerializeField] private Transform _dragingParent;

    private void Awake()
    {
        Render(new List<Screenshot>() {
            new Screenshot(_ImageOne, DateTime.Now),
            new Screenshot(_ImageTwo, DateTime.Now),
            new Screenshot(_ImageThree, DateTime.Now),
            new Screenshot(_ImageFour, DateTime.Now),
            new Screenshot(_ImageFour, DateTime.Now)
            });
    }

    public void Render(IEnumerable<Screenshot> screenshots)
    {
        foreach(Screenshot screenshot in screenshots)
        {
            var view = Instantiate(_template, _container) as ScreenshotView;
            view.Init(_dragingParent);
            view.Render(screenshot);
        }
    }
}
