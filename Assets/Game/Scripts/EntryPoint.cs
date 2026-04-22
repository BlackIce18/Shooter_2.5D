using System;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public GameSettings gameSettings;
    public CameraService cameraService;
    private void Awake()
    {
        gameSettings.Initialize();
    }

    private void Start()
    {
        cameraService.Initialize();
    }
    
    private void Update()
    {
        
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
