using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject bulletPrefab;
    public float fireForce;


    public float currentCoolDown; //
    public float fireCoolDown;

    public bool isAutomatic;

    public float currentAmmo; //
    public int maxAmmo;

    public bool isReloading; //
    public float reloadTime;

    public int damage;

    public float spread;
    public float numberOfPellets;


    [Header("Upgrades")]
    public static float damageUpgrade;
    public static float fireCoolDownUpgrade;
    public static float pelletUpgrade;
    public static float spreadUpgrade;





    public void Fire(Transform firePoint)
    {
        float angleStep = 0;
        if (numberOfPellets + pelletUpgrade > 1)
        {
            angleStep = (spread + spreadUpgrade) / (numberOfPellets + pelletUpgrade - 1);
        }

        for (int i = 0; i < numberOfPellets + pelletUpgrade; i++)
        {
            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, firePoint.position, firePoint.rotation, ObjectPoolManager.PoolType.GameObject);
            bullet.GetComponent<Bullet>().SetDamage((int)(damage * damageUpgrade));

            // Calculate the rotation for each pellet
            Quaternion rotation = Quaternion.Euler(0f, 0f, firePoint.eulerAngles.z - ((spread + spreadUpgrade) / 2f) + i * angleStep);

            bullet.GetComponent<Rigidbody2D>().AddForce(rotation * Vector2.up * fireForce, ForceMode2D.Impulse);
        }
    }

}
