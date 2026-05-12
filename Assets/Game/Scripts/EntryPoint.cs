using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    public Global global;
    public GameSettings gameSettings;
    public CameraService cameraService;

    [SerializeField] private Scene _UI;
    [SerializeField] private Scene _Enveronment;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private DropsController _dropsController;

    private void Awake()
    {
        /*
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        SceneManager.LoadScene("TestEnveronment", LoadSceneMode.Additive);*/

    }

    private IEnumerator Start()
    {
        global.Initialize();
        
        yield return WaitForFacades();
        Debug.Log(Global.Instance);
        GamePlayFacade.Instance.Initialize();
        UIFacade.Instance.Initialize();
        EnvironmentFacade.Instance.Initialize();
        gameSettings.Initialize();
        cameraService.Initialize();
        _playerController.Initialize();
        _dropsController.Initialize();
    }
    
    private IEnumerator WaitForFacades()
    {
        while (GamePlayFacade.Instance == null ||
               UIFacade.Instance == null ||
               EnvironmentFacade.Instance == null)
        {
            yield return null;
        }
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