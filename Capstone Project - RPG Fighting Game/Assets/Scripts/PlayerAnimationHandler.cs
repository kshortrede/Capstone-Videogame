using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Oliver Luo
public class PlayerAnimationHandler : MonoBehaviour
{

    public BattleSystem battleSystem;
    public CharacterAudioManager CharacterAM;
	public ParticleSystem BloodPrefab;
    public void Awake()
    {
        battleSystem = GameObject.Find("Battle System").GetComponent<BattleSystem>();
        GameObject playerParent = GameObject.Find("PlayerSpawn");
        CharacterAM = playerParent.transform.GetChild(0).GetComponent<CharacterAudioManager>();
    }
    public void HitEnemy()
    {
        Debug.Log("Current Total Damage: " + Character.rolledDamage);
        battleSystem.enemyCharacter.TakeDamage(Character.rolledDamage);
        Character.totalDamageInTurn += Character.rolledDamage;

        CharacterAM.PlayAttackSound();
        SpawnBlood();

        battleSystem.enemyCharacter.GetComponent<Animator>().SetTrigger("TakeDamage");
        battleSystem.enemyGUI.SetHealth(battleSystem.enemyCharacter, battleSystem.enemyCharacter.currentHealth);
    }

    public void SpawnBlood()
    {
        Vector3 enemySpawnOffset = new Vector3(battleSystem.enemySpawn.transform.position.x, battleSystem.enemySpawn.transform.position.y + 1, battleSystem.enemySpawn.transform.position.z);
        GameObject bloodInstance = Instantiate(BloodPrefab, enemySpawnOffset, Quaternion.identity).gameObject;
        float bloodLifetime = BloodPrefab.main.duration + BloodPrefab.main.startLifetimeMultiplier;
        Destroy(bloodInstance, bloodLifetime);
    }
}
