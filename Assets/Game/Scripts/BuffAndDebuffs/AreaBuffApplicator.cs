using System;
using UnityEngine;

public class AreaBuffApplicator : MonoBehaviour
{
    [SerializeField] private BuffDebuffController _buffDebuffController;
    [SerializeField] private Buff _buff;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerSystems.instance.BuffDebuffController.ApplyBuff(_buff);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {

        }
    }
}
