using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public Collider head;
    public Collider body;
    public float health;
    
    public void applyDamage(float amount)
    {
        health -= amount;
        if (health < 0f)
        {
            Destroy(gameObject);
        }
    }
}
