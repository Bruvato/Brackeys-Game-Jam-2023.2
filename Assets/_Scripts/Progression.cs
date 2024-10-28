using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progression : MonoBehaviour
{
    public static Progression instance;


    public static int score;
    public static float difficulty;
    [SerializeField] private float difficultyIncreaseRate = 0.1f; // Rate at which difficulty increases

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject upgrade;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        score = 0;
        difficulty = 1f;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void Update()
    {
        // Increase difficulty over time
        difficulty += Time.deltaTime * difficultyIncreaseRate;
        scoreText.text = score.ToString();

    }

    public void AddScore(int amount, Transform location)
    {
        score += amount;

        if (score % 10 == 0 && score != 0)
        {
            StartCoroutine(SpawnUpgrade(location));
        }
    }

    private IEnumerator SpawnUpgrade(Transform loc)
    {
        Time.timeScale = .3f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        ObjectPoolManager.SpawnObject(upgrade, loc.position, transform.rotation, ObjectPoolManager.PoolType.GameObject);

        yield return new WaitForSeconds(.5f);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;


    }

}
