using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Oliver Luo
public enum EnemyType { ZOMBIE, DRAGON_INFANT, VAMPIRE_EYE, GREED_EATER };

public class Enemy : MonoBehaviour
{
    public BattleSystem battleSystem;

    public EnemyType enemyType;

    public string charName;
    public int level;
    public int exp;

    public GameObject enemyPointer;
    public string enemyTag;

    public int damage;
    public float critChance;
    public float critMultiplier;
    public float[] damageRangeMultipliers;

    public int maxHealth;
    public int currentHealth;

    public int maxMana;
    public int currentMana;

    public int healAmount;
    public int healManaCost;

    public float speed;

    public bool canHeal;

    public EnemyAudioManager enemyAM;
    public void Start()
    {
		if (SceneManager.GetActiveScene().name == "FightScene")
		{
			battleSystem = GameObject.Find("Battle System").GetComponent<BattleSystem>();
			GameObject enemyParent = GameObject.Find("EnemySpawn");
			enemyAM = enemyParent.transform.GetChild(0).GetComponent<EnemyAudioManager>();
		}
    }

    // Method to set stats of enemy based on type from WORLD collision with enemy (Elliot's maze)
    // transfer that statically over to fight scene and put it into enemyTag string
    public void initializeStats ()
    {
        enemyTag = enemyPointer.transform.GetChild(1).gameObject.tag;
    }

    public void StartNormalAttack()
    {
        StopCoroutine(NormalAttack());
        StartCoroutine(NormalAttack());
    }
    public bool isOpponentDead()
    {
        if (Character.currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    public IEnumerator NormalAttack()
    {
        int rolledDamage = battleSystem.RollDamage("enemy");

        battleSystem.actionText.text = battleSystem.enemyCharacter.charName + " attacks!";
        // store enemy original position to return after moving to player
        Vector3 originalPosition = battleSystem.enemySpawn.transform.position;

        // store player position to move to
        Vector3 playerPosition = battleSystem.playerSpawn.transform.position;
        playerPosition.x -= 1; // offset by 1 so that player isn't clipping into enemy

        // enemy moves towards player
        StartCoroutine(battleSystem.UnitMoves(battleSystem.enemyCharacter.gameObject, originalPosition, playerPosition));

        yield return new WaitForSeconds(1f);

        battleSystem.enemyCharacter.GetComponent<Animator>().SetTrigger("Attack");
        battleSystem.actionText.text = battleSystem.enemyCharacter.charName + " deals " + highlightText(rolledDamage.ToString(), "red") + " damage to " + Character.charName + ".";

        Character.TakeDamage(rolledDamage);

        yield return new WaitForSeconds(1.5f);

        // enemy returns to original position
        StartCoroutine(battleSystem.UnitMoves(battleSystem.enemyCharacter.gameObject, playerPosition, originalPosition));
        yield return new WaitForSeconds(1f);

        if (isOpponentDead())
        {
            battleSystem.state = BattleState.LOSE;
            battleSystem.playerCharacter.GetComponent<Animator>().SetTrigger("Die");
            battleSystem.StartEndBattle();
        }
        else
        {
            battleSystem.ResetATBBar("enemy"); // reset ATB barback to 0
            battleSystem.state = BattleState.WAITING;
        }
    }

    public void StartHeal()
    {
        StopCoroutine(Heal());
        StartCoroutine(Heal());
    }

    public IEnumerator Heal()
    {
        if (isHPFull() || ! enoughMana(healManaCost))
        {
            StartNormalAttack();
        }
        else
        {
            int netHealAmount = healAmount;
            if (currentHealth + healAmount > maxHealth)
            {
                netHealAmount = maxHealth - currentHealth;
                currentHealth = maxHealth;
            } else
            {
                currentHealth += healAmount;
            }
            currentMana -= healManaCost;
            battleSystem.actionText.text = battleSystem.enemyCharacter.charName + " uses " + highlightText(healManaCost.ToString(), "blue") +  
                " mana and recovers " + highlightText(netHealAmount.ToString(), "green") + " HP.";
            battleSystem.enemyGUI.SetHUD(this);

            battleSystem.enemyCharacter.GetComponent<Animator>().SetTrigger("Heal");
            enemyAM.PlayHealSound();

            yield return new WaitForSeconds(1.5f);

            battleSystem.ResetATBBar("enemy"); // reset ATB barback to 0
            battleSystem.state = BattleState.WAITING;
        }
    }

    // when player loses
    public void Cheer()
    {
        battleSystem.enemyCharacter.GetComponent<Animator>().SetTrigger("Cheer");
    }

    public bool isHPFull()
    {
        if (currentHealth == maxHealth)
            return true;
        return false;
    }

    public bool enoughMana(int manaCost)
    {
        // if mana is insufficient for skill
        if (currentMana - manaCost < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public string highlightText(string text, string color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }
}
