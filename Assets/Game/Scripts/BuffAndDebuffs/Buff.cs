using System;
using UnityEngine;

public class Buff : ScriptableObject
{
    public BuffDebuffController buffdebuffController;
    public string name;
    public int seconds;
    public int minutes;
    public bool infinity = false;

    public virtual void Add() { }
    public virtual void Sub() { }
}
