using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class SpeedBuff : PowerUpEffect
{
    [SerializeField] private float amount = 5;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().moveSpeed += amount;
    }
}
