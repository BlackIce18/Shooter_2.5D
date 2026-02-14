using System;
using UnityEngine;

[Serializable]
public struct SkillContext
{
    public GameObject caster;
    public GameObject target;
    public Vector3 castPoint;
    public Vector3 targetPoint;
}
