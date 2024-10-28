using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float multiplier;

    private void Update()
    {
        globalLight.intensity = 2 - Progression.difficulty * Progression.difficulty;
    }
}
