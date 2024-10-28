using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("References")]
    [SerializeField] private GameObject player;
    private PlayerController playerController;
    private PlayerShoot playerShoot;
    private Player playerScript;
    [SerializeField] private Weapon[] weapons;

    [Header("Upgrades")]
    [SerializeField] private List<string> upgrades = new List<string>();


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerShoot = player.transform.GetChild(0).GetComponent<PlayerShoot>();
        playerScript = player.GetComponent<Player>();

        ResetWeaponUpgrades();
    }

    public void ApplyUpgrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "smg":
                playerShoot.ChangeWeapon(weapons[0]);
                break;
            case "shotgun":
                playerShoot.ChangeWeapon(weapons[1]);
                break;
            case "Increased Damage":
                Weapon.damageUpgrade += 0.2f;
                break;
            case "Rapid Fire":
                Weapon.fireCoolDownUpgrade += 0.05f;
                break;
            case "Piercing Shots":

                break;
            case "Multi-Shot":
                Weapon.pelletUpgrade += 1;
                Weapon.spreadUpgrade += 20;
                break;
            case "Health":
                playerScript.Heal();
                break;
            case "Oxygen":
                playerScript.AddBubble(3);
                break;

        }
    }
    public string GetRandomUpgrade()
    {
        if (upgrades.Count > 0)
        {
            int randomIndex = Random.Range(0, upgrades.Count);
            string selectedUpgrade = upgrades[randomIndex];

            return selectedUpgrade;
        }
        return null;

    }

    private void ResetWeaponUpgrades()
    {
        foreach (Weapon w in weapons)
        {
            Weapon.damageUpgrade = 1;
            Weapon.fireCoolDownUpgrade = 0;
            Weapon.pelletUpgrade = 0;
            Weapon.spreadUpgrade = 0;

        }
    }
}
