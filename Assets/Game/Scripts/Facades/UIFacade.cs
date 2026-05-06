using System;
using TMPro;
using UnityEngine;

public class UIFacade : MonoBehaviour, IFacadeService
{
    public static UIFacade Instance;

    [SerializeField] private ItemDropFloatingWindow _dropFloatingWindow;
    public ItemDropFloatingWindow DropFloatingWindow
    {
        get => _dropFloatingWindow;
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
