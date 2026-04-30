using System;
using UnityEngine;

public class UIFacade : MonoBehaviour
{
    public static UIFacade Instance;
    
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

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
