using UnityEngine;

[CreateAssetMenu(menuName = "Data/Bullet")]
public class BulletData : ScriptableObject
{
    public float speed;
    public float lifeTime;
    public float damage;
    public float acceleration = 10f; // Как быстро набирается скорость
    public float deceleration = 12f; // Как быстро останавливается
    public bool useAcceleration = true;
}
