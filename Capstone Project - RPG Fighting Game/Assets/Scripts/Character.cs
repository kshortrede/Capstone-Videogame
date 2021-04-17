using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Oliver Luo
public class Character : MonoBehaviour
{
    public static string charName = "The One";

    public static int level = 1;
    public static int exp = 0;
    public static int expToNextLevel = 100;
    public static int damage = 35;
    public static int rolledDamage = damage;
    public static int totalDamageInTurn = 0;
    public static float[] damageRangeMultipliers = { 0.8f, 1.2f };
    public static float strongAttackMultiplier = 0.40f;
    public static int strongAttackManaCost = 20;

    public static int maxHealth = 125;
    public static int currentHealth = 125;

    public static int maxMana = 100;
    public static int currentMana = 100;

    public static float speed = 1.50f;
    public static float ATBPosition = 0;
    public static float targetProgress = 0;

    public static int healAmount = 65;
    public static int healManaCost = 25;

	public static int gold = 0;

    public CharacterAudioManager characterAM;
    public PlayerAnimationHandler playerAnimationHandler;


    public static BattleSystem battleSystem;

    void Start()
    {
        battleSystem = GameObject.Find("Battle System").GetComponent<BattleSystem>();

        GameObject playerParent = GameObject.Find("PlayerSpawn");
        characterAM = playerParent.transform.GetChild(0).GetComponent<CharacterAudioManager>();

        playerAnimationHandler = playerParent.transform.GetChild(0).GetComponent<PlayerAnimationHandler>();
    }

    public void RestoreAllStats()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    public static bool TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            return true;
        } 
        else
        {
            return false;
        }
    }
    public void SpendMana(int amount)
    {
        currentMana -= amount;
        battleSystem.playerGUI.SetMana(currentMana);
        battleSystem.playerGUI.SetHUD();
    }

    public static bool EnoughMana(int manaCost)
    {
        // if mana is insufficient for skill
        // return false and tell player in dialogue text
        if (currentMana - manaCost < 0)
        {
            battleSystem.actionText.text = charName + " has insufficient mana to use this skill!";
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsOpponentDead()
    {
        if (battleSystem.enemyCharacter.currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    // the damage dealt will be based on a range of the character's base damage value
    public int RollDamage(string attackType)
    {
        float newDamageFloat;

        newDamageFloat = damage;

        if (attackType == "strong")
        {
            newDamageFloat *= strongAttackMultiplier;
        }

        newDamageFloat = (int)newDamageFloat * Random.Range(damageRangeMultipliers[0], damageRangeMultipliers[1]);

        return (int) newDamageFloat;
    }

    public void StartPlayerAttack(string attackType)
    {
        StopCoroutine(PlayerAttack(attackType));
        StartCoroutine(PlayerAttack(attackType));
    }
    public IEnumerator PlayerAttack(string attackType)
    {
        battleSystem.state = BattleState.ACTION;
        rolledDamage = RollDamage(attackType);

        if (attackType == "normal")
        {
            StartMoveToAttack(attackType);
            battleSystem.actionText.text = charName + " slashes the " + battleSystem.enemyCharacter.charName + "!";
        }
        else if (attackType == "strong" && EnoughMana(strongAttackManaCost))
        {
            StartMoveToAttack(attackType);
            battleSystem.actionText.text = charName + " uses " + battleSystem.playerSkillNames[1].text + "!";
            SpendMana(strongAttackManaCost);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            battleSystem.PlayerTurnNotification();
            battleSystem.state = BattleState.PLAYER;
        }

    }

    public void StartMoveToAttack(string attackType)
    {
        StopCoroutine(MoveToAttack(attackType));
        StartCoroutine(MoveToAttack(attackType));
    }

    public IEnumerator MoveToAttack(string attackType)
    {
        // store enemy original position to return after moving to player
        Vector3 originalPosition = battleSystem.playerSpawn.transform.position;

        Vector3 enemyPosition = battleSystem.enemySpawn.transform.position;
        enemyPosition.x += 1; // offset by 1 so that player isn't clipping into enemy

        StartCoroutine(battleSystem.UnitMoves(this.gameObject, originalPosition, enemyPosition));
        yield return new WaitForSeconds(1f);

        if (attackType == "normal")
        {
            battleSystem.playerCharacter.GetComponent<Animator>().SetTrigger("Attack");
            yield return new WaitForSeconds(1f);
        } 
        else
        {
            battleSystem.playerCharacter.GetComponent<Animator>().SetTrigger("StrongAttack");
            yield return new WaitForSeconds(3f);
        }

        battleSystem.actionText.text = charName + " deals " + highlightText(totalDamageInTurn.ToString(), "red") + " damage to " + battleSystem.enemyCharacter.charName + ".";

        // unit begins moving to the opponent
        StartCoroutine(battleSystem.UnitMoves(this.gameObject,enemyPosition, originalPosition));
        yield return new WaitForSeconds(1f);

        totalDamageInTurn = 0; // reset the total damage in turn back to 0 after turn ends

        if (IsOpponentDead())
        {
            battleSystem.enemyCharacter.GetComponent<Animator>().SetTrigger("Die");
            battleSystem.state = BattleState.WIN;
            battleSystem.StartEndBattle();
        }
        else
        {
            battleSystem.ResetATBBar("player");
            battleSystem.state = BattleState.WAITING;
        }
    }

    public string highlightText(string text, string color)
    {
        return "<color="+ color + ">" + text + "</color>";
    }

    public void doVictoryPose()
    {
        battleSystem.playerCharacter.GetComponent<Animator>().SetTrigger("Victory");
    }

    public void StartHeal()
    {
        StopCoroutine(Heal());
        StartCoroutine(Heal());
    }

    public IEnumerator Heal()
    {        
        // if player is already at full health
        // or if player has insufficient mana
        // then prevent the use of the healing skill
        if (EnoughMana(healManaCost) == false)
        {
            yield return new WaitForSeconds(1f);
            battleSystem.PlayerTurnNotification();
        }
        else
        {
            if (currentHealth != maxHealth)
            {
                int netHealAmount = healAmount;
                // in case the player's current health would go over the max HP after the heal
                // take the difference and heal it to just up to the max HP
                if (currentHealth + healAmount > maxHealth)
                {
                    netHealAmount = maxHealth - currentHealth;
                }

                currentHealth += netHealAmount;

                // check if player has enough mana
                EnoughMana(healManaCost);

                battleSystem.playerGUI.SetHealth(currentHealth);

                // player spends predetermined amount of mana
                SpendMana(healManaCost);

                battleSystem.playerCharacter.GetComponent<Animator>().SetTrigger("Heal");
                characterAM.PlayHealSound();

                battleSystem.actionText.text = charName + " uses " + highlightText(healManaCost.ToString(), "blue")  + " mana and restores " + highlightText(netHealAmount.ToString(), "green") + " health.";
                battleSystem.state = BattleState.ACTION;
                yield return new WaitForSeconds(1.5f);

                battleSystem.ResetATBBar("player");
                battleSystem.state = BattleState.WAITING;
            }
            else if (currentHealth == maxHealth)
            {
                battleSystem.actionText.text = charName + " is already at full health!";
                yield return new WaitForSeconds(1f);
                battleSystem.PlayerTurnNotification();
            }
        }
    }
}
