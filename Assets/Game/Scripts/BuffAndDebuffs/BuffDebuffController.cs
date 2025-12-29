using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffController : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;

    public Characteristics Characteristics => _characteristics;
    public Dictionary<string, Buff> ActiveBuffList = new();
    
    public IEnumerator Timer(Buff buff)
    {
        buff.buffdebuffController = this;
        buff.Add();
        _characteristics.UpdateCharacteristicsList();
        
       /* if (!ActiveBuffList.ContainsKey(buff.name))
        {
        }*/

        if (buff.infinity) {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(buff.seconds);
            buff.Sub();
            ActiveBuffList.Remove(buff.name);
        }
        _characteristics.UpdateCharacteristicsList();
    }

    public void ReloadTimer()
    {
        
    }

    public bool BuffAlreadyContains(Buff _buff)
    {
        
        return true;
    }
}
