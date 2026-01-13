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
    private Dictionary<string, ActiveBuff> _activeBuffList = new();
    private Dictionary<string, ActiveBuff> _activeDeBuffList = new();

    public Dictionary<string, ActiveBuff> ActiveBuffs => _activeBuffList;
    public Dictionary<string, ActiveBuff> ActiveDebuffs => _activeDeBuffList;

    private void Update()
    {
        float dt = Time.deltaTime;
        BuffTimer(dt, _activeBuffList);
        BuffTimer(dt, _activeDeBuffList);
    }

    private void BuffTimer(float dt, Dictionary<string, ActiveBuff> activeBuffList)
    {
        List<string> toRemove = null;

        foreach (var pair in activeBuffList)
        {
            var buff = pair.Value;
            
            if(buff.buff.infinity) continue;

            buff.timeLeft -= dt;

            if (buff.timeLeft <= 0)
            {
                toRemove ??= new();
                toRemove.Add(pair.Key);
            }
        }

        if (toRemove != null)
        {
            foreach (var key in toRemove)
            {
                activeBuffList.Remove(key);
            }
            RecalculateStats();
            EventBus.Publish(new RecalculateCharacteristicsEvent());
        }
    }
    public void Apply(Buff buff, Dictionary<string, ActiveBuff> buffList)
    {
        if (buffList.TryGetValue(buff.name, out ActiveBuff active))
        {
            if (active.stacks < buff.maxStacks) active.stacks++;
            active.timeLeft = buff.secondsDuration;
        }
        else
        {
            ActiveBuff newBuff = new()
            {
                timeLeft = buff.secondsDuration, 
                buff = buff,
                stacks = 1,
            };
            
            buffList.Add(buff.name, newBuff);
        }
        RecalculateStats();
        EventBus.Publish(new RecalculateCharacteristicsEvent());
    }
    public void RecalculateStats()
    {
        _characteristics.ClearModifiers();

        foreach (var buffs in _activeBuffList.Values)
        {
            var flat = _characteristics.Multiply(buffs.buff.flatPerStack, buffs.stacks);
            var percent = _characteristics.Multiply(buffs.buff.percentValueModifier, buffs.stacks);
            
            _characteristics.RegisterFlat(flat);
            _characteristics.RegisterPercent(percent);
        }

        foreach (var debuffs in _activeDeBuffList.Values)
        {
            var flat = _characteristics.Multiply(debuffs.buff.flatPerStack, debuffs.stacks);
            var percent = _characteristics.Multiply(debuffs.buff.percentValueModifier, debuffs.stacks);
            
            _characteristics.RegisterSubFlat(flat);
            _characteristics.RegisterSubPrecrent(percent);
        }
        
        //_characteristics.UpdateCharacteristicsList();
    }
}
