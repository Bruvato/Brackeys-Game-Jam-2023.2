using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private void OnDisable()
    {
        if (Progression.instance != null)
            Progression.instance.AddScore(1, gameObject.transform);

    }
    private void OnDestroy()
    {
        Debug.DrawLine(PlayerController.position, gameObject.transform.position, Color.red, 3);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Die();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            other.gameObject.GetComponent<Player>().TakeDamage();
        }
    }

}
