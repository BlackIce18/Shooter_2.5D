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

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        Global.Instance.UI = this;
    }
}
