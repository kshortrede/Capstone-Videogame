using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Oliver Luo
public enum BattleState { SETUP, WAITING, ACTION, PLAYER, ENEMY, WIN, LOSE };

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerSpawn;
    public Transform enemySpawn;

    public Character playerCharacter;
    public Enemy enemyCharacter;

    public Text actionText;
    public Text[] playerSkillNames;

    public PlayerGUI playerGUI;
    public EnemyGUI enemyGUI;

    public AudioManager battleAM;
    public AudioClip[] songs;

    public Camera fightCamera;
    public string cameraPosition;

    public GameObject skillsMenuContainer;

    public GameObject LevelUpScreenContainer;
    public Text[] LevelUpTexts;

    public GameObject winContainer;
    public GameObject gameOverContainer;

    // total up from all monsters killed in fight
    public int gainedExp;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Character.speed);
        cameraPosition = "general";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SetContainersToInactive();

        battleAM = FindObjectOfType<AudioManager>();
        battleAM.ChangeBGM(songs[battleAM.ChooseRandomBattleTheme()]);

        state = BattleState.SETUP;
        StartCoroutine(SetupBattle());
    }

    public void SetContainersToInactive()
    {
        winContainer.SetActive(false);
        gameOverContainer.SetActive(false);
        skillsMenuContainer.SetActive(false);
        LevelUpScreenContainer.SetActive(false);
    }

    public int CheckEnemyType()
    {
        if (InstanceEnemyInfo.enemyTypeName.Equals("Zombie"))
            return 0;
        else if (InstanceEnemyInfo.enemyTypeName.Equals("Dragon Infant"))
            return 1;
        else if (InstanceEnemyInfo.enemyTypeName.Equals("Vampire Eye"))
            return 2;
        else if (InstanceEnemyInfo.enemyTypeName.Equals("Greed Eater"))
            return 3;
        return 0;
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
        playerGO.name = "PlayerInstance";
        playerCharacter = playerGO.GetComponent<Character>();

        // get the enemy instance info from collision in World (which enemy did the player touch)
        GameObject enemyGO = Instantiate(enemyPrefab.transform.GetChild(CheckEnemyType()).gameObject, enemySpawn);
        enemyGO.name = "EnemyInstance";
        enemyCharacter = enemyGO.GetComponent<Enemy>();
        //enemyCharacter.GetComponent<BoxCollider>().enabled = false; // to prevent scene restart
        
        gainedExp += enemyCharacter.exp;
        

        Debug.Log(enemyCharacter.charName);
        // initialize stats based on World enemy
        enemyCharacter.initializeStats();

        actionText.text = Character.charName + " has encountered a " + enemyCharacter.charName + "...";

        playerGUI.SetHUD();
        enemyGUI.SetHUD(enemyCharacter);

        playerCharacter.GetComponent<Animator>().SetTrigger("getReady");
        enemyCharacter.GetComponent<Animator>().SetTrigger("getReady");
        yield return new WaitForSeconds(2f);
        state = BattleState.WAITING;
    }

    // the damage dealt will be based on a range of the character's base damage value
    public int RollDamage(string unitType)
    {
        float newDamageFloat;
        if (unitType == "player")
        {
            newDamageFloat = Character.damage;
            newDamageFloat = (int) newDamageFloat * Random.Range(Character.damageRangeMultipliers[0], Character.damageRangeMultipliers[1]);
        } else
        {
            newDamageFloat = enemyCharacter.damage;
            newDamageFloat = (int) newDamageFloat * Random.Range(enemyCharacter.damageRangeMultipliers[0], enemyCharacter.damageRangeMultipliers[1]);
        }

        return (int) newDamageFloat;
    }


    // method for movement towards another unit
    public IEnumerator UnitMoves(GameObject unit, Vector3 startPos, Vector3 targetPos)
    {
        float counter = 0;
        float duration = 1.0f;

        unit.GetComponent<Animator>().SetBool("isRunning", true);
        while (counter < duration)
        {
            counter += Time.deltaTime;
            unit.transform.position = Vector3.Lerp(startPos, targetPos, counter / duration);
            yield return null;
        }
        unit.GetComponent<Animator>().SetBool("isRunning", false);
    }

    public void EnemyTurn()
    {
        int moveChoice = Random.Range(0, 2);

        if (moveChoice == 0 || enemyCharacter.canHeal == false)
            enemyCharacter.StartNormalAttack();
        else
            enemyCharacter.StartHeal();
    }

    public void StartEndBattle()
    {
        StartCoroutine(EndBattle());
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WIN)
        {
            PlayVictoryTheme();
            actionText.text = "Battle Won! Gained " + gainedExp + " exp.";

            yield return new WaitForSeconds(3.5f);

            // check to see if player leveled up
            if (LevelSystem.LeveledUp(gainedExp) == true)
            {
                LevelSystem.AddExp(gainedExp);
                if (SceneManager.GetActiveScene().name == "FightScene")
                {
                    LevelUpScreenContainer.SetActive(true);
                    LevelSystem.UpdateLevelUpText(LevelUpTexts);
                    actionText.text = "Leveled up! You are now level " + (Character.level + LevelSystem.timesLeveledUp) + ". Your health and mana have been fully restored.";
                }
            } else
            {
                LevelSystem.AddExp(gainedExp);
            }

            playerCharacter.doVictoryPose();
            playerGUI.SetHUD();

            Debug.Log("Enemy character type: " + enemyCharacter.enemyType);
			EnemiesKilled.AddEnemyKill(enemyCharacter.enemyType);
			Destroy(enemyCharacter);

            winContainer.SetActive(true);
        } 
        else if (state == BattleState.LOSE)
        {
            enemyCharacter.Cheer();
            gameOverContainer.SetActive(true);
            battleAM.ChangeBGM(songs[4]);
            battleAM.StopLoop();
            actionText.text = "Battle Lost.";
        }
    }

    // toggles the camera back and forth
    public void ChangeCamera()
    {
        if (cameraPosition == "general")
        {
            // switch to player position
            cameraPosition = "player";
            fightCamera.transform.position = new Vector3(-35, 3, -3);
            fightCamera.transform.eulerAngles = new Vector3(11, -64, 0);

        } else if (cameraPosition == "player")
        {
            // switch to general view position
            cameraPosition = "general";
            fightCamera.transform.position = new Vector3(-40, 5, -5);
            fightCamera.transform.eulerAngles = new Vector3(35, 0, 0);
        }
    }

    public void PlayVictoryTheme()
    {
        battleAM.ChangeBGM(songs[3]); // play victory theme
        battleAM.StopLoop(); // don't loop it
    }

    public void WinReturnToMaze()
    {
        MainMenuController.loadingState = SceneLoaderState.MAZEFROMFIGHT;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("MazeScene");
    }

    public void GameOverChoice(string choice)
    {
        playerCharacter.RestoreAllStats();
        if (choice == "town")
        {
            EnvironmentGenerator.resetMaze();
            MainMenuController.loadingState = SceneLoaderState.OPENMAINFROMMAZE;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("Loading");
        } else
        {
            EnvironmentGenerator.resetMaze();
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void PlayerTurnNotification()
    {
        actionText.text = Character.charName + "'s turn.";
    }
    public void PlayerHeal()
    {
        playerCharacter.StartHeal();
    }

    public void PlayerAttack(string type)
    {
        if (type == "normal")
        {
            playerCharacter.StartPlayerAttack("normal");
            
        } else if (type == "strong")
        {
            playerCharacter.StartPlayerAttack("strong");
        }
        CloseSkillsMenu();
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYER)
            return;

        CloseSkillsMenu(); // in case the skills menu is opened
        PlayerAttack("normal");
    }

    public void OnSkillsButton()
    {
        if (state != BattleState.PLAYER)
            return;

        if (skillsMenuContainer.activeInHierarchy)
            CloseSkillsMenu();
        else
            OpenSkillsMenu();
    }

    public void OpenSkillsMenu()
    {
        skillsMenuContainer.SetActive(true);
    }

    public void CloseSkillsMenu()
    {
        skillsMenuContainer.SetActive(false);
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYER)
            return;

        CloseSkillsMenu();
        PlayerHeal();
    }

    // resets Action Bar of a unit to 0 whenever that unit's turn ends
    public void ResetATBBar(string unitType)
    {
        if (unitType == "player")
        {
            playerGUI.speedBar.fillAmount = 0;
            playerGUI.speedBar.color = playerGUI.defaultBarColor;
        } 
        else
        {
            enemyGUI.speedBar.fillAmount = 0;
            enemyGUI.speedBar.color = enemyGUI.defaultBarColor;
        }
    }
}
