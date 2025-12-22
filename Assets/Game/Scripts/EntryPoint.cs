using System;
using System.Security.Cryptography;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public static EntryPoint Instance { get; private set; }
    public static event System.Action onAwake; 
    public static event System.Action onStart;
    public static event System.Action onUpdate; 
    public static event System.Action onFixedUpdate; 
    public static event System.Action onLateUpdate; 
    public static event System.Action onEnable; 
    public static event System.Action onDisable; 
    public static event System.Action onDestroy; 
    
    private void Awake()
    {
        Time.timeScale = 1.25f;
        
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
