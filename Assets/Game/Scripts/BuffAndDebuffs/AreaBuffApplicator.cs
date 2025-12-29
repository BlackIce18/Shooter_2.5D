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
            if (!PlayerSystems.instance.BuffDebuffController.ActiveBuffList.ContainsKey(_buff.name))
            {
                PlayerSystems.instance.BuffDebuffController.ActiveBuffList.Add(_buff.name, _buff);
            }
            StartCoroutine(PlayerSystems.instance.BuffDebuffController.Timer(_buff));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //StopCoroutine(_buffDebuffController.Timer());
    }
}
