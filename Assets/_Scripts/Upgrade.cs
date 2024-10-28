using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject upgradeEffect;

    private string selectedUpgrade;
    [SerializeField] private TextMeshProUGUI upgradeText;

    private void OnEnable()
    {
        Invoke("Disable", 10f);
        selectedUpgrade = UpgradeManager.instance.GetRandomUpgrade();
        upgradeText.text = selectedUpgrade;

        Explode();

    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                UpgradeManager.instance.ApplyUpgrade(selectedUpgrade);
                Impact();
                break;

            case "Enemy":
                Impact();
                break;

            case "Wall":
                ObjectPoolManager.ReturnObjectToPool(gameObject);
                break;
        }

    }
    private void Impact()
    {
        ObjectPoolManager.SpawnObject(upgradeEffect, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Die();
                }
            }
            EnemyBullet eb = collider.GetComponent<EnemyBullet>();
            if (eb != null)
            {
                eb.Impact();
            }

        }
    }
    private void Disable()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }


}
