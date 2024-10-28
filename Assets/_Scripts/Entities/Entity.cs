using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathEffect;
    public GameObject spawnEffect;


    private void OnEnable()
    {
        ObjectPoolManager.SpawnObject(spawnEffect, transform.position, transform.rotation, ObjectPoolManager.PoolType.ParticleSystem);

        maxHealth += (int)(maxHealth * (Progression.difficulty - 1f) * 0.5f + 1f);
        currentHealth = maxHealth;
    }
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        ObjectPoolManager.SpawnObject(deathEffect, transform.position, transform.rotation, ObjectPoolManager.PoolType.ParticleSystem);

        if (gameObject.CompareTag("Player")) { Destroy(gameObject); return; }
        ObjectPoolManager.ReturnObjectToPool(gameObject);

    }
}
