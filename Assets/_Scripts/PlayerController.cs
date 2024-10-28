using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Vector2 position;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 mousePosition;

    [Header("Particles")]
    [SerializeField] private GameObject TrailEffect;
    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;
    private float counter;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        HandleParticles();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;

        position = rb.position;

    }

    private void HandleParticles()
    {
        counter += Time.deltaTime;

        if (Mathf.Abs(rb.velocity.magnitude) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                ObjectPoolManager.SpawnObject(TrailEffect, transform.position, transform.rotation, ObjectPoolManager.PoolType.ParticleSystem);
                counter = 0;
            }

        }
    }



}