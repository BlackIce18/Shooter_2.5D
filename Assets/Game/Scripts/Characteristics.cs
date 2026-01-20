using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Общие характеристики для существ/предметов
[Serializable]
public class CharacteristicsData
{
    public float health;
    public float healthMax;
    public float mana;
    public float manaMax;
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
        CharacteristicsList.Add("healthMax", _current.healthMax);
        CharacteristicsList.Add("mana", _current.mana);
        CharacteristicsList.Add("manaMax", _current.manaMax);
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
            AddFlat(flat);
        }
        foreach (var percent in _percentAdditions)
        {
            AddPercent(percent);
        }

        UpdateCharacteristicsList();
    }

    public void UpdateCharacteristicsList()
    {
        CharacteristicsList["health"] = _current.health;
        CharacteristicsList["healthMax"] = _current.healthMax;
        CharacteristicsList["mana"] = _current.mana;
        CharacteristicsList["manaMax"] = _current.manaMax;
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
        _current.healthMax = _base.CharacteristicsData.healthMax;
        _current.mana = _base.CharacteristicsData.mana;
        _current.manaMax = _base.CharacteristicsData.manaMax;
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
        Current.healthMax += data.healthMax;
        Current.mana += data.mana;
        Current.manaMax += data.manaMax;
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
            healthMax = data.healthMax * stacks,
            mana = data.mana * stacks,
            manaMax = data.manaMax * stacks,
            attackMin = data.attackMin * stacks,
            attackMax = data.attackMax * stacks,
            attackRate = data.attackRate * stacks,
            defence = data.defence * stacks,
            speed = data.speed * stacks,
            attackDistance = data.attackDistance * stacks,
            attackDelay = data.attackDelay * stacks
        };
    }
    public void AddPercent(CharacteristicsData data)
    { 
        Current.health += Current.health * data.health / 100;
        Current.healthMax += Current.healthMax * data.healthMax / 100;
        Current.mana += Current.mana * data.mana / 100;
        Current.manaMax += Current.manaMax * data.manaMax / 100;
        Current.attackMin += Current.attackMin * data.attackMin / 100;
        Current.attackMax += Current.attackMax * data.attackMax / 100;
        Current.attackRate += Current.attackRate * data.attackRate / 100;
        Current.defence += Current.defence * data.defence / 100;
        Current.speed += Current.speed * data.speed / 100;
        Current.attackDelay += Current.attackDelay * data.attackDelay / 100;
    }
}