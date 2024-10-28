using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] private int lives = 3;
    [SerializeField] int maxBubbles = 10;
    private int currentBubbles;

    [SerializeField] private float maxOxygenLevel = 100f;
    [SerializeField] private float currentOxygenLevel;
    [SerializeField] private float oxygenDepletionRate = 10f;

    [Header("I-frames")]
    private bool isInvincible = false;
    [SerializeField] private float invincibilityDuration = 1.5f; // Duration of invincibility in seconds

    [Header("UI")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private Color black;
    [SerializeField] private Color red;
    [SerializeField] private Transform bubbleUICAnvas;
    [SerializeField] private Image bubbleUI;
    private Image[] bubUIList;



    private SpriteRenderer playerSprite;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        currentBubbles = maxBubbles;
        currentOxygenLevel = maxOxygenLevel;
        bubUIList = new Image[maxBubbles];

        UpdateHearts();

        SetUpBubbleUI();
    }
    private void Update()
    {
        DepleteOxygen();
        ChangeBubbleUIColor();

        if (currentOxygenLevel <= 0)
        {
            PopBubble();
        }
        HandleInvincibiltyEffect();



    }

    private void DepleteOxygen()
    {
        currentOxygenLevel -= oxygenDepletionRate * Progression.difficulty * Time.deltaTime;
    }

    private void PopBubble()
    {
        currentBubbles--;
        UpdateBubblesUI();

        if (currentBubbles <= 0) { Die(); return; }

        currentOxygenLevel = maxOxygenLevel;

    }
    public override void Die()
    {
        base.Die();
        GameManager.instance.PlayerDied();
    }

    public void AddBubble(int amount)
    {
        if (currentBubbles < maxBubbles) { currentBubbles += amount; };

        currentBubbles = Mathf.Clamp(currentBubbles, 0, maxBubbles);
        UpdateBubblesUI();
    }

    public override void TakeDamage(int amount = 0)
    {
        if (!isInvincible) // Check if the player is not currently invincible
        {
            lives--;
            UpdateHearts();
            if (lives <= 0) { Die(); }

            // Activate i-frames
            isInvincible = true;

            // Deactivate i-frames after the invincibility duration
            StartCoroutine(DisableInvincibility());
        }
    }
    public void Heal()
    {
        if (lives < 3)
        {
            lives++;
            UpdateHearts();
        }
    }
    private IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void HandleInvincibiltyEffect()
    {
        if (isInvincible)
        {
            // Calculate a flickering alpha value based on time
            float flickerAlpha = Mathf.PingPong(Time.time * 10f, 1.0f);

            // Update the sprite renderer's color with the flickering alpha
            Color newColor = playerSprite.color;
            newColor.a = flickerAlpha;
            playerSprite.color = newColor;
        }
        else
        {
            // Reset the sprite renderer's color to full opacity when not invincible
            Color newColor = playerSprite.color;
            newColor.a = 1.0f;
            playerSprite.color = newColor;
        }
    }

    private void UpdateHearts()
    {
        foreach (Image h in hearts)
        {
            h.color = black;
        }

        for (int j = 0; j < lives; j++)
        {
            hearts[j].color = red;
        }
    }

    private void SetUpBubbleUI()
    {
        for (int i = 0; i < maxBubbles; i++)
        {
            Image bub = Instantiate(bubbleUI);
            bub.transform.SetParent(bubbleUICAnvas, true);
            float yPos = -.25f - (i * .75f); // Calculate the Y position based on index and spacing
            bub.rectTransform.anchoredPosition = new Vector2(0, yPos);

            bubUIList[i] = bub;
        }
    }

    private void UpdateBubblesUI()
    {
        foreach (Image b in bubUIList)
        {
            b.color = black;
        }

        for (int i = 0; i < currentBubbles; i++)
        {
            bubUIList[i].color = Color.white;
        }
    }

    private void ChangeBubbleUIColor()
    {
        if (currentBubbles > 0)
        {
            Image lastBubbleImage = bubUIList[currentBubbles - 1];
            Color newColor = lastBubbleImage.color;

            // Normalize the alpha value between 0 and 1
            float alpha = currentOxygenLevel / maxOxygenLevel;

            newColor.a = alpha;
            lastBubbleImage.color = newColor;
        }

    }



}
