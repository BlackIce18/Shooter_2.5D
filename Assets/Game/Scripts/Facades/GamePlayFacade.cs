using System;
using UnityEngine;

public class GamePlayFacade : MonoBehaviour, IFacadeService
{
    public static GamePlayFacade Instance;
    
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private LVLSystem _lvlSystem;
    
    public Characteristics Characteristics
    {
        get => _characteristics;
    }
    
    public LVLSystem LvlSystem
    {
        get => _lvlSystem;
    }
    private void OnEnable()
    {
        EventBus.Subscribe<GlobalInitEvent>(Initialize);
    }
    private void OnDisable()
    {
        EventBus.Subscribe<GlobalInitEvent>(Initialize);
    }
    public void Initialize(GlobalInitEvent g)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        throw new NotImplementedException();
    }
}
