using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Oliver Luo
public class EnemyAnimationHandler : MonoBehaviour
{

    public BattleSystem battleSystem;
    public EnemyAudioManager enemyAM;
    public Enemy enemy;
	public ParticleSystem BloodPrefab;
	
    public void Awake()
    {
		if (SceneManager.GetActiveScene().name == "FightScene")
		{
			battleSystem = GameObject.Find("Battle System").GetComponent<BattleSystem>();
			GameObject enemyParent = GameObject.Find("EnemySpawn");
			enemy = enemyParent.transform.GetChild(0).GetComponent<Enemy>();
			enemyAM = enemyParent.transform.GetChild(0).GetComponent<EnemyAudioManager>();
		}
    }
    public void HitPlayer()
    {
        enemyAM.PlayAttackSound();
        SpawnBlood();

        battleSystem.playerCharacter.GetComponent<Animator>().SetTrigger("TakeDamage");
        battleSystem.playerGUI.SetHealth(Character.currentHealth);

        Debug.Log("Player was hit, and flinched.");
    }

    public void SpawnBlood()
    {
        Vector3 playerSpawnOffset = new Vector3(battleSystem.playerSpawn.transform.position.x, battleSystem.playerSpawn.transform.position.y + 1, battleSystem.playerSpawn.transform.position.z);
        GameObject bloodInstance = Instantiate(BloodPrefab, playerSpawnOffset, Quaternion.identity).gameObject;
        float bloodLifetime = BloodPrefab.main.duration + BloodPrefab.main.startLifetimeMultiplier;
        Destroy(bloodInstance, bloodLifetime);
    }
}
