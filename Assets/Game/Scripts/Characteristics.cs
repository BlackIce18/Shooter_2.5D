using System;
using System.Collections.Generic;
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

    public CharacteristicsData Current => _current;
    public Dictionary<string, float> CharacteristicsList = new();

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
    }

    public void UpdateCharacteristicsList()
    {
        Debug.Log(_current.health);
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
        _current.health = _base.characteristicsData.health;
        _current.attackMin = _base.characteristicsData.attackMin;
        _current.attackMax = _base.characteristicsData.attackMax;
        _current.attackRate = _base.characteristicsData.attackRate;
        _current.defence = _base.characteristicsData.defence;
        _current.speed = _base.characteristicsData.speed;
        _current.attackDelay = _base.characteristicsData.attackDelay;
        /*_current.critChance = _base.characteristics.critChance;
        _current.critPercentage = _base.characteristics.critPercentage;
        _current.defenceResistance = _base.characteristics.defenceResistance;
        _current.fireResistance = _base.characteristics.fireResistance;
        _current.critResistance = _base.characteristics.critResistance;*/
    }
    public void Add(CharacteristicsData add)
    {
        _current.health += add.health;
        _current.attackMin += add.attackMin;
        _current.attackMax += add.attackMax;
        _current.attackRate += add.attackRate;
        _current.defence += add.defence;
        _current.speed += add.speed;
        _current.attackDelay += add.attackDelay;
    }

    public void Remove(CharacteristicsData sub)
    {
        _current.health -= sub.health;
        _current.attackMin -= sub.attackMin;
        _current.attackMax -= sub.attackMax;
        _current.attackRate -= sub.attackRate;
        _current.defence -= sub.defence;
        _current.speed -= sub.speed;
        _current.attackDelay -= sub.attackDelay;
    }
}