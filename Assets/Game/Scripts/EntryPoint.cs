using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    public Global global;
    public GameSettings gameSettings;
    public CameraService cameraService;

    private void Awake()
    {
        global.Initialize();
        gameSettings.Initialize();
    }

    private void Start()
    {
        /*SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);*/
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