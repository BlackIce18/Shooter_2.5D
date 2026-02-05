using UnityEngine;

public struct SkillContext
{
    public GameObject caster;
    public Transform castPoint;
    public Vector3 targetPoint;

    public BuffDebuffController buffController;
    public Characteristics characteristics;
}
