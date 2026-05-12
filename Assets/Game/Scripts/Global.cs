using System;
using UnityEngine;

public class Global : MonoBehaviour, IFacadeService
{
    public static Global _instance;
    public static Global Instance
    {
        get { return _instance; }
    }

    public UIFacade UI;
    public GamePlayFacade GamePlay;
    public EnvironmentFacade Environment;
    
    public void Initialize()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        //EventBus.Publish(new GlobalInitEvent(this));
    }
    
    private void OnEnable()
    {
        EventBus.Subscribe<GameplayInitEvent>(InitializeGamePlay);
        EventBus.Subscribe<EnvironmentFacadeInitEvent>(InitializeEnvironment);
        EventBus.Subscribe<UIFacadeInitEvent>(InitializeUI);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<GameplayInitEvent>(InitializeGamePlay);
        EventBus.Unsubscribe<EnvironmentFacadeInitEvent>(InitializeEnvironment);
        EventBus.Unsubscribe<UIFacadeInitEvent>(InitializeUI);
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
