using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float _health = 30;

    public float Health
    {
        get { return _health; }
        set
        {
            if (_health <= 0)
            {
                Die();
            }

            _health = value;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log($"{gameObject.name} получил {damage} урона. Осталось: {Health}");
    }

    public void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject);
    }
}
