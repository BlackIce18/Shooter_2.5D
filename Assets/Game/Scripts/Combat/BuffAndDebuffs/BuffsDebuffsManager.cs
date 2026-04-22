using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffsDebuffsManager : MonoBehaviour
{
    public static BuffsDebuffsManager instance;
    public List<Buff> _buffs;
    
    private void Awake()
    {
        instance = this;
    }
}
