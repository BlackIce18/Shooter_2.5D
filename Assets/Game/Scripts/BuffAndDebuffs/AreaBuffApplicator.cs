using UnityEngine;

public class AreaBuffApplicator : MonoBehaviour
{
    [SerializeField] private BuffDebuffController _buffDebuffController;
    [SerializeField] private Buff _buff;
    [SerializeField] private Buff _deBuff;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var buffController = PlayerSystems.instance.BuffDebuffController;
            if(_buff) buffController.Apply(_buff, buffController.ActiveBuffs);
            if(_deBuff) buffController.Apply(_deBuff, buffController.ActiveDebuffs);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {

        }
    }
}
