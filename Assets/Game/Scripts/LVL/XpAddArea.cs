using UnityEngine;

public class XpAddArea : MonoBehaviour
{
    [SerializeField] private bool _add = true;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_add)
                EventBus.Publish(new AddXpEvent(5000));
            else 
                EventBus.Publish(new SubXpEvent(7000));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {

        }
    }
}
