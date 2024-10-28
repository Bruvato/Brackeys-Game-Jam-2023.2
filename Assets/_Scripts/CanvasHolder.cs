using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CanvasHolder : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        // Follow the player's position, ignoring the player's rotation
        transform.rotation = quaternion.identity;
    }
}
