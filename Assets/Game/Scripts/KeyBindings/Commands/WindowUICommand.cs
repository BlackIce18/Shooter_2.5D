using System;
using UnityEngine;

public class WindowUICommand : KeyCommand
{
    [SerializeField] private GameObject _window;
    [SerializeField] private bool _setFullPause = false;
    private float _originalTimeScale;
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _originalTimeScale = Time.timeScale;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void Execute()
    {
        if (_canvasGroup)
        {
            if (_canvasGroup.alpha == 1)
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.blocksRaycasts = false;
                if (_setFullPause)
                    Time.timeScale = _originalTimeScale;

                PlayerSystems.instance.CanAttack = true;
                PlayerSystems.instance.CanMove = true;
            }
            else
            {
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.alpha = 1;
                if (_setFullPause)
                    Time.timeScale = 0;
                
                PlayerSystems.instance.CanAttack = false;
                PlayerSystems.instance.CanMove = false;
            }
            return;
        }
        
        if (_window.gameObject.activeSelf)
        {
            CloseWindow();
            if (_setFullPause)
                Time.timeScale = _originalTimeScale;
        }
        else
        {
            OpenWindow();
            if (_setFullPause)
                Time.timeScale = 0;
        }
    }

    public void CloseWindow()
    {
        _window.gameObject.SetActive(false);
        PlayerSystems.instance.CanAttack = true;
        PlayerSystems.instance.CanMove = true;
    }

    public void OpenWindow()
    {
        _window.gameObject.SetActive(true);
        PlayerSystems.instance.CanAttack = false;
        PlayerSystems.instance.CanMove = false;
    }
}