using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Oliver Luo
public class PlayerGUI : MonoBehaviour
{

    public Text nameText;
    public Text levelText;
    public Text healthText;
    public Text manaText;
    public Text expText;

    public Image healthBar;
    public Image manaBar;
    public Image expBar;

    public BattleSystem battleSystem;

    public Image speedBar;
    private float targetProgress;
    private float randomStartPos;
    public Color32 defaultBarColor;
    public Color32 filledBarColor;

    public void Start()
    {
        SetHUD();
        defaultBarColor = new Color32(255, 255, 0, 255); // yellow
        filledBarColor = new Color32(0, 63, 255, 255); // cerulean blue
        speedBar.color = defaultBarColor;

        targetProgress = 0;
        randomStartPos = Random.Range(0, 0.40f); // bar can start at a random position
        speedBar.fillAmount = randomStartPos;
        IncrementProgress(1f - randomStartPos);
    }

    public void Update()
    {
        if (battleSystem.state == BattleState.WAITING)
        {
            if (speedBar.fillAmount < targetProgress)
            {
                speedBar.fillAmount += Character.speed * Time.deltaTime;
            }
            else
            {
                battleSystem.state = BattleState.PLAYER;
                battleSystem.PlayerTurnNotification();
                speedBar.color = filledBarColor;
            }
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = speedBar.fillAmount + newProgress;
    }

    // updates the HUD
    public void SetHUD()
    {
        nameText.text = Character.charName;
        levelText.text = "Level " + Character.level;
        healthText.text = Character.currentHealth + "/" + Character.maxHealth;
        manaText.text = Character.currentMana + "/" + Character.maxMana;
        expText.text = Character.exp + "/" + Character.expToNextLevel;
        expBar.fillAmount = calculateBarProgress("exp");
        healthBar.fillAmount = calculateBarProgress("health");
        manaBar.fillAmount = calculateBarProgress("mana");
    }

    public float calculateBarProgress(string type)
    {
        if (type.Equals("exp"))
            return (float)Character.exp / Character.expToNextLevel;
        else if (type.Equals("health"))
            return (float)Character.currentHealth / Character.maxHealth;
        else if (type.Equals("mana"))
        {
            return (float)Character.currentMana / Character.maxMana;
        }
        return 0;
    }

    public void SetHealth(int health)
    {
        healthText.text = health + "/" + Character.maxHealth;
        healthBar.fillAmount = calculateBarProgress("health");
    }
    public void SetMana(int mana)
    {
        manaBar.fillAmount = calculateBarProgress("mana");
    }
    public void SetExp(int exp)
    {
        expBar.fillAmount = calculateBarProgress("exp");
    }
}