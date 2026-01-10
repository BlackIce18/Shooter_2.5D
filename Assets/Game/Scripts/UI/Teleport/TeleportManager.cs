using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct TeleportStruct
{
    public TeleportsScriptableObject data;
    public TeleportPoint point;
    public Button button;
    public TextMeshProUGUI buttonText;
}
public class TeleportManager : MonoBehaviour
{
    [SerializeField] private List<TeleportStruct> _teleportList;
    [SerializeField] private List<TeleportStruct> _availableTeleportList;
    [SerializeField] private Transform _player;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private WindowUICommand _teleportMapUI;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _teleportList.Count; i++)
        {
            _teleportList[i].buttonText.text = _teleportList[i].data.title;
            ButtonBind(_teleportList[i]);
        }
    }

    private void ButtonBind(TeleportStruct teleportStruct)
    {
        teleportStruct.button.onClick.AddListener(() =>
        {
            _player.transform.position = teleportStruct.point.transform.position;
            _cameraMovement.MoveToPlayerImmediately();
            _teleportMapUI.Execute();
        });
    }
}
