using System;
using UnityEngine;

public class Global : MonoBehaviour, IFacadeService
{
    public static Global _instance;
    public static Global Instance
    {
        get { return _instance; }
    }

    [HideInInspector] public UIFacade UI;
    [HideInInspector] public GamePlayFacade GamePlay;
    [HideInInspector] public EnvironmentFacade Environment;

    public void Initialize()
    {
        _instance = this;
        DontDestroyOnLoad(this);
        EventBus.Publish(new GlobalInitEvent());
    }
    
    private void OnEnable()
    {
        EventBus.Subscribe<GameplayInitEvent>(InitializeGamePlay);
        EventBus.Subscribe<EnvironmentFacadeInitEvent>(InitializeEnvironment);
        EventBus.Subscribe<UIFacadeInitEvent>(InitializeUI);
    }
    private void OnDisable()
    {
        EventBus.Subscribe<GameplayInitEvent>(InitializeGamePlay);
        EventBus.Subscribe<EnvironmentFacadeInitEvent>(InitializeEnvironment);
        EventBus.Subscribe<UIFacadeInitEvent>(InitializeUI);
    }

    private void InitializeUI(UIFacadeInitEvent e)
    {
        UI = e.facade;
    }

    private void InitializeEnvironment(EnvironmentFacadeInitEvent e)
    {
        Environment = e.facade;
    }

    private void InitializeGamePlay(GameplayInitEvent e)
    {
        GamePlay = e.facade;
    }
}
