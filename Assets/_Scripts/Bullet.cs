using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private int damage = 50;

    private void OnEnable()
    {
        Invoke("Disable", 2f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                Impact();
                break;

            case "Enemy":
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                Impact();
                break;
        }
    }

    private void Impact()
    {
        // Instantiate(impactEffect, transform.position, Quaternion.identity);
        ObjectPoolManager.SpawnObject(impactEffect, transform.position, quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);

        // Destroy(gameObject);

        // gameObject.SetActive(false);

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    private void Disable()
    {
        // gameObject.SetActive(false);

        ObjectPoolManager.ReturnObjectToPool(gameObject);

    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    public void SetDamage(int amount)
    {
        damage = amount;
    }
}
