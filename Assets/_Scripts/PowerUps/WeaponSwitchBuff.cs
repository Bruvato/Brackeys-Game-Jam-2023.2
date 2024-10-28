using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/WeaponSwitchBuff")]
public class WeaponSwitchBuff : PowerUpEffect
{
    [SerializeField] private Weapon[] weapons;
    public override void Apply(GameObject target)
    {
        target.transform.GetChild(0).GetComponent<PlayerShoot>().ChangeWeapon(weapons[Random.Range(0, weapons.Length)]);
    }
}
