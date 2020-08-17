using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public Collider head;
    public Collider body;
    public float health;
    public GameObject ragdollPrefab;

    public void applyDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Destroy(gameObject);
            Destroy(Instantiate(ragdollPrefab, transform.position, transform.rotation), 8f);
        }
    }
}
