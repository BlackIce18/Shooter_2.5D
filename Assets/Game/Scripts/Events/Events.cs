using UnityEngine;

public struct DamageEvent
{
    public GameObject target;
    public float damage;
    public Vector3 hitPoint;

    public DamageEvent(GameObject target, float damage, Vector3 hitPoint)
    {
        this.target = target;
        this.damage = damage;
        this.hitPoint = hitPoint;
    }
}

public struct DeathEvent
{
    public GameObject target;

    public DeathEvent(GameObject target)
    {
        this.target = target;
    }
}

public struct SoundEvent
{
    public GameObject target;
    public AudioClip audioClip;

    public SoundEvent(GameObject target, AudioClip audioClip)
    {
        this.target = target;
        this.audioClip = audioClip;
    }
}