using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Общие характеристики для существ/предметов
[Serializable]
public class CharacteristicsData
{
    public float health;
    public float attackMin;
    public float attackMax;
    public float attackRate;
    public float defence;
    public float speed;
    public float attackDistance;
    public float attackDelay;
    /*public float critChance;
    public float critPercentage;
    public float defenceResistance;
    public float fireResistance;
    public float critResistance;*/
}

public class Characteristics : MonoBehaviour
{
    [SerializeField] private BaseCharacteristicsData _base;
    private CharacteristicsData _current = new();
    public CharacteristicsData Base => _base.CharacteristicsData;
    public CharacteristicsData Current => _current;
    public Dictionary<string, float> CharacteristicsList = new();
    
    private readonly List<CharacteristicsData> _flatAdditions = new();
    private readonly List<CharacteristicsData> _percentAdditions = new();
    private readonly List<CharacteristicsData> _flatSubtraction = new();
    private readonly List<CharacteristicsData> _percentSubtraction = new();
    private void Awake()
    {
        ResetToBase();
        CharacteristicsList.Add("health", _current.health);
        CharacteristicsList.Add("attackMin", _current.attackMin);
        CharacteristicsList.Add("attackMax", _current.attackMax);
        CharacteristicsList.Add("attackRate", _current.attackRate);
        CharacteristicsList.Add("defence", _current.defence);
        CharacteristicsList.Add("speed", _current.speed);
        CharacteristicsList.Add("attackDistance", _current.attackDistance);
        CharacteristicsList.Add("attackDelay", _current.attackDelay);
        UpdateCharacteristicsList();
    }
    public void ClearModifiers()
    {
        _flatAdditions.Clear();
        _percentAdditions.Clear();
        _flatSubtraction.Clear();
        _percentSubtraction.Clear();
    }
    public void RegisterFlat(CharacteristicsData data)
    {
        _flatAdditions.Add(data);
    }
    public void RegisterPercent(CharacteristicsData data)
    {
        _percentAdditions.Add(data);
    }

    public void RegisterSubFlat(CharacteristicsData data)
    {
        _flatSubtraction.Add(data);
    }

    public void RegisterSubPrecrent(CharacteristicsData data)
    {
        _percentSubtraction.Add(data);
    }
    public void Recalculate()
    {
        ResetToBase();

        foreach (var flat in _flatAdditions)
        {
            Debug.Log("F:"+flat.health);
            AddFlat(flat);
        }
        foreach (var flat in _flatSubtraction)
        {
            Debug.Log("F:"+flat.health);
            Negate(flat);
        }
        foreach (var percent in _percentAdditions)
        {
            Debug.Log("P:"+percent.health);
            AddPercent(percent);
        }
        
        foreach (var percent in _percentSubtraction)
        {
            Debug.Log("P:"+percent.health);
            NegatePercent(percent);
        }

        UpdateCharacteristicsList();
    }

    public void UpdateCharacteristicsList()
    {
        CharacteristicsList["health"] = _current.health;
        CharacteristicsList["attackMin"] = _current.attackMin;
        CharacteristicsList["attackMax"] = _current.attackMax;
        CharacteristicsList["attackRate"] = _current.attackRate;
        CharacteristicsList["defence"] = _current.defence;
        CharacteristicsList["speed"] = _current.speed;
        CharacteristicsList["attackDistance"] = _current.attackDistance;
        CharacteristicsList["attackDelay"] = _current.attackDelay;
    }

    public void ResetToBase()
    {
        _current.health = _base.CharacteristicsData.health;
        _current.attackMin = _base.CharacteristicsData.attackMin;
        _current.attackMax = _base.CharacteristicsData.attackMax;
        _current.attackRate = _base.CharacteristicsData.attackRate;
        _current.defence = _base.CharacteristicsData.defence;
        _current.speed = _base.CharacteristicsData.speed;
        _current.attackDelay = _base.CharacteristicsData.attackDelay;
        /*_current.critChance = _base.characteristics.critChance;
        _current.critPercentage = _base.characteristics.critPercentage;
        _current.defenceResistance = _base.characteristics.defenceResistance;
        _current.fireResistance = _base.characteristics.fireResistance;
        _current.critResistance = _base.characteristics.critResistance;*/
    }
    
    public void AddFlat(CharacteristicsData data)
    {
        Current.health += data.health;
        Current.attackMin += data.attackMin;
        Current.attackMax += data.attackMax;
        Current.attackRate += data.attackRate;
        Current.defence += data.defence;
        Current.speed += data.speed;
        Current.attackDelay += data.attackDelay;

        UpdateCharacteristicsList();
    }
    public CharacteristicsData Multiply(CharacteristicsData data, int stacks)
    {
        return new CharacteristicsData
        {
            health = data.health * stacks,
            attackMin = data.attackMin * stacks,
            attackMax = data.attackMax * stacks,
            attackRate = data.attackRate * stacks,
            defence = data.defence * stacks,
            speed = data.speed * stacks,
            attackDistance = data.attackDistance * stacks,
            attackDelay = data.attackDelay * stacks
        };
    }
    public void Negate(CharacteristicsData data)
    {
        Current.health -= data.health;
        Current.attackMin -= data.attackMin;
        Current.attackMax -= data.attackMax;
        Current.attackRate -= data.attackRate;
        Current.defence -= data.defence;
        Current.speed -= data.speed;
        Current.attackDelay -= data.attackDelay;

        UpdateCharacteristicsList();
    }
    
        public void AddFlat(ActiveBuff activeBuff)
    {
        var data = activeBuff.buff.flatPerStack;
        int stacks = activeBuff.stacks;
    
        Current.health += data.health * stacks;
        Current.attackMin += data.attackMin * stacks;
        Current.attackMax += data.attackMax * stacks;
        Current.attackRate += data.attackRate * stacks;
        Current.defence += data.defence * stacks;
        Current.speed += data.speed * stacks;
        Current.attackDelay += data.attackDelay * stacks;
    }

    // public void NegateFlate(ActiveBuff activeBuff)
    // {
    //     var data = activeBuff.buff.flatPerStack;
    //     int stacks = activeBuff.stacks;
    //     
    //     Current.health -= data.health * stacks;
    //     Current.attackMin -= data.attackMin * stacks;
    //     Current.attackMax -= data.attackMax * stacks;
    //     Current.attackRate -= data.attackRate * stacks;
    //     Current.defence -= data.defence * stacks;
    //     Current.speed -= data.speed * stacks;
    //     Current.attackDelay -= data.attackDelay * stacks;
    // }
    // public void AddPercent(ActiveBuff activeBuff)
    // { 
    //     var data = activeBuff.buff.percentValueModifier;
    //     int stacks = activeBuff.stacks;
    //
    //     Current.health += Current.health * data.health * stacks / 100;
    //     Current.attackMin += Current.attackMin * data.attackMin * stacks / 100;
    //     Current.attackMax += Current.attackMax * data.attackMax * stacks / 100;
    //     Current.attackRate += Current.attackRate * data.attackRate * stacks / 100;
    //     Current.defence += Current.defence * data.defence * stacks / 100;
    //     Current.speed += Current.speed * data.speed * stacks / 100;
    //     Current.attackDelay += Current.attackDelay * data.attackDelay * stacks / 100;
    // }
    // public void NegatePercent(ActiveBuff activeBuff)
    // {
    //     var data = activeBuff.buff.percentValueModifier;
    //     int stacks = activeBuff.stacks;
    //     
    //     Current.health -= Current.health * data.health * stacks / 100;
    //     Current.attackMin -= Current.attackMin * data.attackMin * stacks / 100;
    //     Current.attackMax -= Current.attackMax * data.attackMax * stacks / 100;
    //     Current.attackRate -= Current.attackRate * data.attackRate * stacks / 100;
    //     Current.defence -= Current.defence * data.defence * stacks / 100;
    //     Current.speed -= Current.speed * data.speed * stacks / 100;
    //     Current.attackDelay -= Current.attackDelay * data.attackDelay * stacks / 100;
    // }
    public void AddPercent(CharacteristicsData data)
    { 
        int stacks = 1;

        Current.health += Current.health * data.health * stacks / 100;
        Current.attackMin += Current.attackMin * data.attackMin * stacks / 100;
        Current.attackMax += Current.attackMax * data.attackMax * stacks / 100;
        Current.attackRate += Current.attackRate * data.attackRate * stacks / 100;
        Current.defence += Current.defence * data.defence * stacks / 100;
        Current.speed += Current.speed * data.speed * stacks / 100;
        Current.attackDelay += Current.attackDelay * data.attackDelay * stacks / 100;
    }
    public void NegatePercent(CharacteristicsData data)
    {
        int stacks = 1;
        
        Current.health -= Current.health * data.health * stacks / 100;
        Current.attackMin -= Current.attackMin * data.attackMin * stacks / 100;
        Current.attackMax -= Current.attackMax * data.attackMax * stacks / 100;
        Current.attackRate -= Current.attackRate * data.attackRate * stacks / 100;
        Current.defence -= Current.defence * data.defence * stacks / 100;
        Current.speed -= Current.speed * data.speed * stacks / 100;
        Current.attackDelay -= Current.attackDelay * data.attackDelay * stacks / 100;
    }
}