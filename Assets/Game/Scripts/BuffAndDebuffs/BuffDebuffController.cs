using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBuff
{
    public Buff buff;
    public int stacks;
    public float timeLeft;
}
public class BuffDebuffController : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;
    private Dictionary<string, ActiveBuff> ActiveBuffList = new();
    private Dictionary<string, ActiveBuff> ActiveDeBuffList = new();
    public Characteristics Characteristics => _characteristics;

    public void ApplyBuff(Buff buff)
    {
        if (ActiveBuffList.TryGetValue(buff.name, out ActiveBuff active))
        {
            active.timeLeft = buff.secondsDuration;
            return;
        }

        ActiveBuff newBuff = new()
        {
            timeLeft = buff.secondsDuration, 
            buff = buff,
            stacks = 1,
        };
        
        ActiveBuffList.Add(buff.name, newBuff);
        RecalculateStats();
        
        if(!buff.infinity)
            StartCoroutine(BuffTimer(buff, ActiveBuffList));
    }

    public void ApplyDebuff(Buff buff)
    {
        if (ActiveDeBuffList.TryGetValue(buff.name, out ActiveBuff active))
        {
            active.timeLeft = buff.secondsDuration;
            return;
        }
        ActiveBuff newBuff = new()
        {
            timeLeft = buff.secondsDuration, 
            buff = buff,
            stacks = 1,
        };
        
        ActiveDeBuffList.Add(buff.name, newBuff);
        RecalculateStats();
        
        if(!buff.infinity)
            StartCoroutine(BuffTimer(buff, ActiveDeBuffList));
    }

    private IEnumerator BuffTimer(Buff buff, Dictionary<string, ActiveBuff> dictionary)
    {
        while (dictionary[buff.name].timeLeft > 0)
        {
            dictionary[buff.name].timeLeft -= Time.deltaTime;
            yield return null;
        }

        RemoveBuff(buff, dictionary);
    }

    private void RemoveBuff(Buff buff, Dictionary<string, ActiveBuff> dictionary)
    {
        dictionary.Remove(buff.name);
        RecalculateStats();
        StopCoroutine(BuffTimer(buff, dictionary));
    }
    public void AddFlat(ActiveBuff activeBuff)
    {
        var data = activeBuff.buff.flatPerStack;
        int stacks = activeBuff.stacks;

        _characteristics.Current.health += data.health * stacks;
        _characteristics.Current.attackMin += data.attackMin * stacks;
        _characteristics.Current.attackMax += data.attackMax * stacks;
        _characteristics.Current.attackRate += data.attackRate * stacks;
        _characteristics.Current.defence += data.defence * stacks;
        _characteristics.Current.speed += data.speed * stacks;
        _characteristics.Current.attackDelay += data.attackDelay * stacks;
    }
    public void NegateFlate(ActiveBuff activeBuff)
    {
        var data = activeBuff.buff.percentValueModifier;
        int stacks = activeBuff.stacks;
        
        _characteristics.Current.health -= data.health * stacks;
        _characteristics.Current.attackMin -= data.attackMin * stacks;
        _characteristics.Current.attackMax -= data.attackMax * stacks;
        _characteristics.Current.attackRate -= data.attackRate * stacks;
        _characteristics.Current.defence -= data.defence * stacks;
        _characteristics.Current.speed -= data.speed * stacks;
        _characteristics.Current.attackDelay -= data.attackDelay * stacks;
    }
    public void AddPercent(ActiveBuff activeBuff)
    { 
        var data = activeBuff.buff.percentValueModifier;
        int stacks = activeBuff.stacks;

        _characteristics.Current.health += _characteristics.Current.health * data.health * stacks / 100;
        _characteristics.Current.attackMin += _characteristics.Current.attackMin * data.attackMin * stacks / 100;
        _characteristics.Current.attackMax += _characteristics.Current.attackMax * data.attackMax * stacks / 100;
        _characteristics.Current.attackRate += _characteristics.Current.attackRate * data.attackRate * stacks / 100;
        _characteristics.Current.defence += _characteristics.Current.defence * data.defence * stacks / 100;
        _characteristics.Current.speed += _characteristics.Current.speed * data.speed * stacks / 100;
        _characteristics.Current.attackDelay += _characteristics.Current.attackDelay * data.attackDelay * stacks / 100;
    }
    public void NegatePercent(ActiveBuff activeBuff)
    {
        var data = activeBuff.buff.percentValueModifier;
        int stacks = activeBuff.stacks;
        
        _characteristics.Current.health -= _characteristics.Current.health * data.health * stacks / 100;
        _characteristics.Current.attackMin -= _characteristics.Current.attackMin * data.attackMin * stacks / 100;
        _characteristics.Current.attackMax -= _characteristics.Current.attackMax * data.attackMax * stacks / 100;
        _characteristics.Current.attackRate -= _characteristics.Current.attackRate * data.attackRate * stacks / 100;
        _characteristics.Current.defence -= _characteristics.Current.defence * data.defence * stacks / 100;
        _characteristics.Current.speed -= _characteristics.Current.speed * data.speed * stacks / 100;
        _characteristics.Current.attackDelay -= _characteristics.Current.attackDelay * data.attackDelay * stacks / 100;
    }
    private void RecalculateStats()
    {
        _characteristics.ResetToBase();
        foreach (var buffs in ActiveBuffList.Values)
        {
            AddFlat(buffs);
            AddPercent(buffs);
        }

        _characteristics.UpdateCharacteristicsList();
    }
}
