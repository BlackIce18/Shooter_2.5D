using System;
using UnityEngine;

public class EnvironmentFacade : MonoBehaviour, IFacadeService
{
    public static EnvironmentFacade Instance;

    [SerializeField] private Transform _dropsParent;

    public Transform DropsParent
    {
        get => _dropsParent;
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        Global.Instance.Environment = this;
    }
}
