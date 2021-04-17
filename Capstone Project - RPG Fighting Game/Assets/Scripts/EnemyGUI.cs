using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Oliver Luo
public class EnemyGUI : MonoBehaviour
{

    public Text nameText;
    public Text levelText;
    public Text healthText;
    public Text manaText;

    public Image healthBar;
    public Image manaBar;

    public BattleSystem battleSystem;

    public Image speedBar;
    private float targetProgress;
    private float randomStartPos;
    public Color32 defaultBarColor;
    public Color32 filledBarColor;

    public void Start()
    {
        defaultBarColor = new Color32(255, 255, 0, 255); // yellow
        filledBarColor = new Color32(0, 63, 255, 255); // cerulean blue
        speedBar.color = defaultBarColor;

        targetProgress = 0;
        randomStartPos = Random.Range(0, 0.40f);
        speedBar.fillAmount = randomStartPos;
        IncrementProgress(1f - randomStartPos);
    }

    public void Update()
    {
        if (battleSystem.state == BattleState.WAITING)
        {
            if (speedBar.fillAmount < targetProgress)
            {
                speedBar.fillAmount += battleSystem.enemyCharacter.speed * Time.deltaTime;
            }
            else
            {
                battleSystem.state = BattleState.ENEMY;
                speedBar.color = filledBarColor;
                battleSystem.EnemyTurn();
            }
        }
    }
    public void IncrementProgress(float newProgress)
    {
        targetProgress = speedBar.fillAmount + newProgress;
    }

    // default values when starting scene
    // will need to change when combing with other group members
    public void SetHUD(Enemy enemy)
    {
        nameText.text = enemy.charName;
        levelText.text = "Level " + enemy.level;
        healthText.text = enemy.currentHealth + "/" + enemy.maxHealth;
        manaText.text = enemy.currentMana + "/" + enemy.maxMana;
        healthBar.fillAmount = calculateBarProgress(enemy, "health");
        manaBar.fillAmount = calculateBarProgress(enemy, "mana");
    }
    public float calculateBarProgress(Enemy enemy, string type)
    {
        if (type.Equals("health"))
            return (float) enemy.currentHealth / enemy.maxHealth;
        else if (type.Equals("mana"))
        {
            return (float) enemy.currentMana / enemy.maxMana;
        }
        return 0;
    }

    public void SetHealth(Enemy enemy, int health)
    {
        healthText.text = health + "/" + enemy.maxHealth;
        healthBar.fillAmount = calculateBarProgress(enemy, "health");
    }
    public void SetMana(Enemy enemy, int mana)
    {
        manaBar.fillAmount = calculateBarProgress(enemy, "mana");
    }
}
