using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [Range(0f, 1f)]
    [SerializeField] private float turnSpeed = .1f;
    [SerializeField] private bool isShooter = false;
    [SerializeField] private float shootDistance = 5f;
    [SerializeField] private float strafeSpeed = 1f;
    [SerializeField] private float nextTimeToFire = .5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireForce = 10f;









    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }
    private void OnEnable()
    {
        moveSpeed *= (Progression.difficulty - 1f) * 0.5f + 1f;

    }
    private void FixedUpdate()
    {
        Vector2 direction = (PlayerController.position - rb.position).normalized;

        Vector2 newPos;

        float distance = Vector2.Distance(rb.position, PlayerController.position);



        if (isShooter)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;

            if (distance > shootDistance)
            {
                newPos = MoveRegular(direction);
            }
            else
            {
                newPos = MoveStrafing(direction);
            }

            Shoot();

            //newPos -= rb.position;

            //rb.AddForce(newPos, ForceMode2D.Force);
            rb.MovePosition(newPos);


        }
        else
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = Mathf.LerpAngle(rb.rotation, angle, turnSpeed);

            newPos = MoveRegular(direction);

            rb.MovePosition(newPos);
        }
    }

    private Vector2 MoveRegular(Vector2 direction)
    {

        Vector2 newPos = transform.position + transform.up * Time.fixedDeltaTime * moveSpeed;

        return newPos;
    }

    private Vector2 MoveStrafing(Vector2 direction)
    {
        Vector2 newPos = transform.position + transform.right * Time.fixedDeltaTime * strafeSpeed;
        return newPos;
    }

    private void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, firePoint.position, firePoint.rotation, ObjectPoolManager.PoolType.GameObject);

            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }




}
