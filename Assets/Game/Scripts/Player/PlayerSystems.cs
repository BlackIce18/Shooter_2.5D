using System;
using UnityEngine;

public class PlayerSystems : MonoBehaviour
{
    public static PlayerSystems instance;
    [SerializeField] private BuffDebuffController _buffDebuffController;

    public BuffDebuffController BuffDebuffController => _buffDebuffController;

    private void Awake()
    {
        instance = this;
    }
}
