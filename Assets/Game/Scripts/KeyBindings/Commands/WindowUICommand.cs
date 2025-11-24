using System;
using UnityEngine;

public class WindowUICommand : KeyCommand
{
    [SerializeField] private GameObject _window;
    [SerializeField] private bool _setFullPause = false;
    private float _originalTimeScale;

    private void Start()
    {
        _originalTimeScale = Time.timeScale;
    }

    public override void Execute()
    {
        
        if (_window.gameObject.activeSelf)
        {
            CloseWindow();
            if(_setFullPause)
                Time.timeScale = _originalTimeScale;
        }
        else
        {
            OpenWindow();
            if(_setFullPause)
                Time.timeScale = 0;
        }
    }

    public void CloseWindow()
    {
        _window.gameObject.SetActive(false);
    }

    public void OpenWindow()
    {
        _window.gameObject.SetActive(true);
    }
}
