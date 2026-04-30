using System;
using UnityEngine;

public class DashCommand : KeyCommand
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Dash _dash;
    public override void Execute()
    {
        _dash.StartDash(_playerController.DashDirection);
    }
}