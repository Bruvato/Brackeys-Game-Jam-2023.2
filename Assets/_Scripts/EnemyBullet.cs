using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;

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
                if (other.gameObject != gameObject)
                {
                    Impact();
                }

                break;
            case "Player":
                other.gameObject.GetComponent<Player>().TakeDamage();
                Impact();
                break;
        }
    }

    public void Impact()
    {
        ObjectPoolManager.SpawnObject(impactEffect, transform.position, quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
    private void Disable()
    {

        ObjectPoolManager.ReturnObjectToPool(gameObject);

    }
    private void OnDisable()
    {
        CancelInvoke();
    }




}
