using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    public float maxHealth;

    private float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
    public void Revive(float amount)
    {
        health = Mathf.Clamp(health + amount, 0f, maxHealth);
    }
}
