using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    public Weapon currentWeapon;

    [SerializeField] Image circle;



    // public void Shoot()
    // {
    //     currentWeapon.Fire(firePoint);

    //     //AUDIO

    // }

    private void Start()
    {
        currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        currentWeapon.currentCoolDown = currentWeapon.fireCoolDown;
        currentWeapon.isReloading = false;

        circle.fillAmount = 0;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            StartCoroutine(Reload());
            return;
        }
        if (currentWeapon.isReloading)
        {
            return;
        }
        if (currentWeapon.currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if (currentWeapon.isAutomatic)
        {
            if (Input.GetMouseButton(0) && currentWeapon.currentCoolDown <= 0f)
            {
                currentWeapon.Fire(firePoint);
                currentWeapon.currentAmmo--;
                currentWeapon.currentCoolDown = currentWeapon.fireCoolDown - Weapon.fireCoolDownUpgrade;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && currentWeapon.currentCoolDown <= 0f)
            {
                currentWeapon.Fire(firePoint);
                currentWeapon.currentAmmo--;
                currentWeapon.currentCoolDown = currentWeapon.fireCoolDown - Weapon.fireCoolDownUpgrade;
            }
        }

        currentWeapon.currentCoolDown -= Time.deltaTime;
    }

    private IEnumerator Reload()
    {
        currentWeapon.isReloading = true;

        float reloadStartTime = Time.time;
        float reloadEndTime = reloadStartTime + currentWeapon.reloadTime;

        while (Time.time < reloadEndTime)
        {
            float progress = 1 - ((reloadEndTime - Time.time) / currentWeapon.reloadTime);
            circle.fillAmount = progress;

            yield return null;
        }
        circle.fillAmount = 0;


        // yield return new WaitForSeconds(currentWeapon.reloadTime);
        currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        currentWeapon.isReloading = false;
    }

    public void ChangeWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        currentWeapon.isReloading = false;
    }

}
