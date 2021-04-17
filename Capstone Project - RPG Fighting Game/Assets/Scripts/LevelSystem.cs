using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Oliver Luo
public class LevelSystem : MonoBehaviour
{
    public PlayerGUI playerGUI;

    public static float maxHealthCoefficient = 1.05f;
    public static float maxManaCoefficient = 1.02f;
    public static float speedCoefficient = 1.03f;
    public static float damageCoefficient = 1.08f;

    public static float[] originalStats = new float[5];
    public static int timesLeveledUp = 0;

    public static void AddExp(int amt)
    {
        Character.exp += amt;
        originalStats = GetOriginalStats();

        while (Character.exp >= Character.expToNextLevel)
        {
            // if the player leveled up      
            // to take care of exp overflow into next level
            Character.exp -= Character.expToNextLevel;
            // enough exp to level up
            Character.level++;
            UpdatePlayerStats();
            SetExpToNextLevel(Character.level);
            timesLeveledUp++;
            Debug.Log("times leveled up: " + timesLeveledUp);
        }
    }

    public static float[] GetOriginalStats()
    {
        float[] playerStats = new float[5];

        playerStats[0] = Character.maxHealth;
        playerStats[1] = Character.maxMana;
        playerStats[2] = Character.damage;
        playerStats[3] = Character.speed;
        playerStats[4] = Character.level;

        return playerStats;
    }

    public static bool LeveledUp(int amt)
    {
        if (Character.exp + amt >= Character.expToNextLevel)
        {
            return true;
        }
        return false;
    }

    public static float levelUpCalculation(float stat, float coefficient, int digits)
    {
        
        for (int i = 0; i < timesLeveledUp; i++)
        {
            Debug.Log("current stat: " + stat);
            stat *= coefficient;
        }
        return (float) System.Math.Round(stat, digits);
    }

    public static int newLevelCalculation(int originalLevel)
    {
        return timesLeveledUp + originalLevel;
    }

    public static void UpdateLevelUpText(Text[] levelUpTexts)
    {
        Debug.Log("updateLevelUpText called again");
        levelUpTexts[0].text += " " + originalStats[0].ToString() + " --> " + levelUpCalculation(originalStats[0], maxHealthCoefficient, 0);
        levelUpTexts[1].text += " " + originalStats[1].ToString() + " --> " + levelUpCalculation(originalStats[1], maxManaCoefficient, 0);
        levelUpTexts[2].text += " " + originalStats[2].ToString() + " --> " + levelUpCalculation(originalStats[2], damageCoefficient, 0);
        levelUpTexts[3].text += " " + originalStats[3].ToString() + " --> " + levelUpCalculation(originalStats[3], speedCoefficient, 2);
        levelUpTexts[4].text += " " + originalStats[4].ToString() + " --> " + newLevelCalculation((int)originalStats[4]);
        timesLeveledUp = 0; // reset counter
    }

    public static void UpdatePlayerStats()
    {
        Character.maxHealth = (int) System.Math.Round(Character.maxHealth * maxHealthCoefficient, 0) ;
        Character.maxMana = (int) System.Math.Round(Character.maxMana * maxManaCoefficient, 0);
        Character.speed = (float) System.Math.Round(Character.speed * speedCoefficient, 2);
        Character.damage = (int)System.Math.Round(Character.damage * damageCoefficient, 0);

        RestoreHPAndMana();
    }

    public static void RestoreHPAndMana()
    {
        Character.currentHealth = Character.maxHealth;
        Character.currentMana = Character.maxMana;
    }

    public static int GetExpToNextLevel(int level)
    {
        return (int)Mathf.Floor(100 * level * Mathf.Pow(level, 0.5f)); // level up formula
    }

    public static void SetExpToNextLevel(int level)
    {
        Character.expToNextLevel = GetExpToNextLevel(level);
    }

    public static int GetLevelNumber()
    {
        return Character.level;
    }

}
