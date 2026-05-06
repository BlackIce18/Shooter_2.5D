using UnityEngine;

public class EnvironmentFacade : MonoBehaviour, IFacadeService
{
    public static EnvironmentFacade Instance;

    [SerializeField] private Transform _dropsParent;

    public Transform DropsParent
    {
        get => _dropsParent;
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
        throw new System.NotImplementedException();
    }
}
