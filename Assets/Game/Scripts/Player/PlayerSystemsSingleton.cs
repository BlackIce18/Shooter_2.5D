using System;
using UnityEngine;

public class PlayerSystemsSingleton : MonoBehaviour
{
    public static PlayerSystemsSingleton instance;
    [SerializeField] private BuffDebuffController _buffDebuffController;
    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }
    public BuffDebuffController BuffDebuffController => _buffDebuffController;

    private void Awake()
    {
        instance = this;
    }
}
