using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuffsDebuffs/Buffs")]
public class Buff : ScriptableObject
{
    public string name;
    public int secondsDuration;
    public bool infinity = false;
    public Sprite icon;
    public int maxStacks;
    
    public CharacteristicsData flatPerStack;
    public CharacteristicsData percentValueModifier;
    
    public virtual void Add(BuffDebuffController controller)
    {
        controller.Characteristics.AddFlat(flatPerStack);
    }

    public virtual void Remove(BuffDebuffController controller)
    {
        controller.Characteristics.Negate(flatPerStack);
    }
}

