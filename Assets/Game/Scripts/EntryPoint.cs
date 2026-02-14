using System;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public static EntryPoint Instance { get; private set; }
    public static event Action onAwake; 
    public static event Action onStart;
    public static event Action onUpdate; 
    public static event Action onFixedUpdate; 
    public static event Action onLateUpdate; 
    public static event Action onEnable; 
    public static event Action onDisable; 
    public static event Action onDestroy; 
    
    private void Awake()
    {
        Time.timeScale = 1.5f;
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        onAwake?.Invoke();
    }

    private void Start()
    {
        onStart?.Invoke();
    }
    
    private void Update()
    {
        //onUpdate?.Invoke();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
