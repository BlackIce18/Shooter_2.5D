using System;
using UnityEngine;

// Общие характеристики для существ/предметов
[Serializable]
public class CharacteristicsData
{
    public float health;
    public float attack;
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

    private void Awake()
    {
        ResetToBase();
    }

    public void ResetToBase()
    {
        _current.health = _base.characteristicsData.health;
        _current.attack = _base.characteristicsData.attack;
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
        _current.attack += add.attack;
        _current.attackRate += add.attackRate;
        _current.defence += add.defence;
        _current.speed += add.speed;
        _current.attackDelay += add.attackDelay;
    }

    public void Remove(CharacteristicsData sub)
    {
        _current.health -= sub.health;
        _current.attack -= sub.attack;
        _current.attackRate -= sub.attackRate;
        _current.defence -= sub.defence;
        _current.speed -= sub.speed;
        _current.attackDelay -= sub.attackDelay;
    }
}