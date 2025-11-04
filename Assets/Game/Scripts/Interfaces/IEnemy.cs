using UnityEngine;

public interface IEnemy
{
    Transform Transform { get; }
    Collider Collider { get; }
    bool CanSeePlayer { get; }
    bool InAttackRange { get; }
    Vector3 PlayerPosition { get; }
    void MoveTo(Vector3 position);
    void Attack();
    void LookAt(Vector3 position);
}