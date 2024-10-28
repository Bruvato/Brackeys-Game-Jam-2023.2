using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject popEffect;

    private void OnEnable()
    {
        Invoke("Disable", 5f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.GetComponent<Player>().AddBubble(1);
                break;

        }
        Pop();
    }

    private void Pop()
    {
        ObjectPoolManager.SpawnObject(popEffect, transform.position, popEffect.transform.rotation, ObjectPoolManager.PoolType.ParticleSystem);

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    private void Disable()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

}
