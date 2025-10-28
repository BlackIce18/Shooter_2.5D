using System;
using UnityEngine;

public class DashCommand : MonoBehaviour, IKeyCommand
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Dash _dash;
    public void Execute()
    {
        Debug.Log(_playerController.DashDirection);
        _dash.StartDash(_playerController.DashDirection);
    }
}