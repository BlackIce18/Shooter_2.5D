using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActiveBuff
{
    public Buff buff;
    public int stacks;
    public float timeLeft;
}
public class BuffDebuffController : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;
    private Dictionary<string, ActiveBuff> ActiveBuffList = new();
    public Characteristics Characteristics => _characteristics;

    public void ApplyBuff(Buff buff)
    {
        if (ActiveBuffList.TryGetValue(buff.name, out ActiveBuff active))
        {
            active.timeLeft = buff.secondsDuration;
            return;
        }

        ActiveBuff newBuff = new() { timeLeft = buff.secondsDuration, buff = buff };
        buff.Add(this);
        _characteristics.UpdateCharacteristicsList();
        ActiveBuffList.Add(buff.name, newBuff);
        Debug.Log(ActiveBuffList.Count);
        
        if(!buff.infinity)
            StartCoroutine(BuffTimer(buff));
    }

    private IEnumerator BuffTimer(Buff buff)
    {
        while (ActiveBuffList[buff.name].timeLeft > 0)
        {
            ActiveBuffList[buff.name].timeLeft -= Time.deltaTime;
            yield return null;
        }

        RemoveBuff(buff);
    }

    private void RemoveBuff(Buff buff)
    {
        ActiveBuffList.Remove(buff.name);
        buff.Remove(this);
        _characteristics.UpdateCharacteristicsList();
        StopCoroutine(BuffTimer(buff));
    }
}
