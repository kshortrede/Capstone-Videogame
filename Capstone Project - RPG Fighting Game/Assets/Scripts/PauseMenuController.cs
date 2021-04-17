/// <summary>
/// Kenneth Shortrede
/// GUI and Save Systems --- Controls the Pause Menu and provides a connection with the Save System
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    //public static PauseMenuController PauseMenuGlobal = new PauseMenuController();

    public static bool IsMenuOpen = false;
    GameObject MenuBackGround = null;
    public PlayerGUINonFighting playerGUI;

    public GameObject player;

    private void Awake()
    {
        MenuBackGround = GameObject.Find("MenuBackground Prefab");
    }
    
    //Makes sure to load the correct aspects of the scene on Start if needed
    private void Start()
    {
        
        if (MainMenuController.loadingState.Equals(SceneLoaderState.LOADMAIN))
        {
            print("Run Start - Load Main - Load Everything");
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            Load(false);
            playerGUI.SetHUD();
        } else if (MainMenuController.loadingState.Equals(SceneLoaderState.OPENMAINFROMMAZE))
        {
            print("Active Missions: "+Player.ActiveMissions.Length);
            print("Run Start - Load Main from Maze - Load NPCs");
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            
            //Load the NPC and the Missions for the player
            Load(true);
           
            for(int i = 0; i < Player.ActiveMissions.Length; i++)
            {
                if(Player.ActiveMissions[i] != null)
                {
                    print("Mission Name:" + Player.ActiveMissions[i].Title);
                } else
                {
                    print("No mission Here");
                }
            }

            //Go through the dictionary and add all enemy kills to the missions
            if (EnemiesKilled.enemiesKilled.ContainsKey(EnemyType.ZOMBIE))
            {
                player.GetComponent<Player>().AddKill(EnemyType.ZOMBIE, EnemiesKilled.enemiesKilled[EnemyType.ZOMBIE]);
            }
            if (EnemiesKilled.enemiesKilled.ContainsKey(EnemyType.DRAGON_INFANT))
            {
                player.GetComponent<Player>().AddKill(EnemyType.DRAGON_INFANT, EnemiesKilled.enemiesKilled[EnemyType.DRAGON_INFANT]);
            }
            if (EnemiesKilled.enemiesKilled.ContainsKey(EnemyType.GREED_EATER))
            {
                player.GetComponent<Player>().AddKill(EnemyType.GREED_EATER, EnemiesKilled.enemiesKilled[EnemyType.GREED_EATER]);
            }
            if (EnemiesKilled.enemiesKilled.ContainsKey(EnemyType.VAMPIRE_EYE))
            {
                player.GetComponent<Player>().AddKill(EnemyType.VAMPIRE_EYE, EnemiesKilled.enemiesKilled[EnemyType.VAMPIRE_EYE]);
            }
            //Clear the dictionary
            EnemiesKilled.enemiesKilled.Clear();
            playerGUI.SetHUD();
        }
        MainMenuController.loadingState = SceneLoaderState.NONE;
    }
    //Open Menu
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.GetComponent<CanvasGroup>().alpha = 1;
        IsMenuOpen = true;
        Time.timeScale = 0;
    }

    //Close Menu and go back to game
    public void Resume()
    {
        print("Resume");
        IsMenuOpen = false;
        Time.timeScale = 1; //This will probably stop Unity UI Animations, so exclude them
        this.GetComponent<CanvasGroup>().alpha = 0;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Close Menu
    }
    //Call the Save System on button pressed
    public void Save()
    {
        SaveSystem.SavePlayer(player, SaveSystem.PathEndPlayer);
        SaveSystem.SaveNPC(SaveSystem.PathEndNPC);
        print("Save");
    }
    //Call teh Save System (load) on button pressed
    public void Load(bool loadingFromMaze)
    {
        LoadNPCs(loadingFromMaze);

        LoadPlayer(loadingFromMaze);

        print("Loaded");
    }
    
    //Receive and assign the necessary information to update the NPCs to a previously saved state
    private void LoadNPCs(bool loadingFromMaze)
    {
        NPCData data;
        if (loadingFromMaze)
        {
            data = SaveSystem.LoadGameNPC(SaveSystem.MazePersistenceInfo);
        } else
        {
            data = SaveSystem.LoadGameNPC(SaveSystem.PathEndNPC);
        }
        

        for(int i = 0; i < data.NPCLists.Count; i++)
        {
            NPC tmpNPC = data.NPCLists[i];
            GameObject NPCTemporary = GameObject.Find(tmpNPC.name);
            NPCInteractions tmp = NPCTemporary.GetComponent<NPCInteractions>();

            if (tmpNPC.IsMissionAvailable)
            {
                tmp.IsMissionAvailable = tmpNPC.IsMissionAvailable;
                tmp.MissionIndex = tmpNPC.MissionIndex;

                for(int j = 0; j < tmp.MissionsAvailable.Length; j++)
                {
                    if(tmp.MissionsAvailable[j] is MissionExecute)
                    {
                        
                        (tmp.MissionsAvailable[j] as MissionExecute).ChangeMissionState(tmpNPC.MissionStates[j]);
                    } else if(tmp.MissionsAvailable[j] is MissionDelivery)
                    {
                       
                        (tmp.MissionsAvailable[j] as MissionDelivery).ChangeMissionState(tmpNPC.MissionStates[j]);
                    }
                }
            }

            Vector3 position;
            position.x = tmpNPC.Position[0];
            position.y = tmpNPC.Position[1];
            position.z = tmpNPC.Position[2];
            NPCTemporary.transform.position = position;
        }
    }


    /// <summary>
    /// Receive and assign the necessary information to update the Player to a previously saved state
    /// UPDATE --- IT IS A PROBLEM AT THE MOMENT IF WE WANT TO CHANGE THE AMOUNT OF MISSIONS ALLOWED FOR THE USER
    /// </summary>
    private void LoadPlayer(bool loadingFromMaze)
    {
        PlayerData data;
        if (loadingFromMaze)
        {
            data = SaveSystem.LoadGame(SaveSystem.MazePersistenceInfoPlayer);
        } else
        {
            data = SaveSystem.LoadGame(SaveSystem.PathEndPlayer);
			
            
			Character.maxHealth = data.MaxHealth;
			Character.currentHealth = data.CurrentHealth;
			Character.exp = data.Experience;
			Character.expToNextLevel = data.ExperienceToNextLevel;
			Character.gold = data.Gold;
			Character.level = data.Level;
			Character.damage = data.Damage;
			Character.maxMana = data.MaxMana;
			Character.currentMana = data.CurrentMana;
			Character.speed = data.Speed;
        }

        if (player != null)
        {
            Vector3 PlayerPosition;
            if(data.Position != null)
            {
                PlayerPosition.x = data.Position[0];
                PlayerPosition.y = data.Position[1];
                PlayerPosition.z = data.Position[2];
            } else
            {
                PlayerPosition.x = 0;
                PlayerPosition.y = 0;
                PlayerPosition.z = 0;
            }
            

            player.transform.position = PlayerPosition;
        }
        else
        {
            print("Failed");
        }

        Player playerScript = player.GetComponent<Player>();
        if(Player.ActiveMissions == null )//|| Player.ActiveMissions.Length == 0) 
        {
            Player.ActiveMissions = new Mission[playerScript.MaxMissionsAllowed]; 
        }
        for (int i = 0; i < Player.ActiveMissions.Length; i++)
        {
            
            if (!data.ActiveMissions[i].NPCName.Equals("null"))
            {
                NPCInteractions tmpNPC = GameObject.Find(data.ActiveMissions[i].NPCName).GetComponent<NPCInteractions>();
                
                for (int j = 0; j < tmpNPC.MissionsAvailable.Length; j++)
                {
                    Debug.Log(tmpNPC.MissionsAvailable[j].Title + "---" + data.ActiveMissions[i].MissionName);
                    if (tmpNPC.MissionsAvailable[j].Title.Equals(data.ActiveMissions[i].MissionName))
                    {
                        //Debug.Log("Success in finding the saved mission!");

                        Player.ActiveMissions[i] = tmpNPC.MissionsAvailable[j];
                        if(Player.ActiveMissions[i] is MissionExecute)
                        {
                            (Player.ActiveMissions[i] as MissionExecute).AmountKilled = data.ActiveMissions[i].enemiesKilled;
                        }
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("We didnt find any NPC");
                Player.ActiveMissions[i] = null;
            }
        }
        playerScript.UpdateMissionPanel();
    }
    
    public void Exit()
    {
        print("Get out!");
        Resume();
        SceneManager.LoadScene("MainMenu");
        //Application.Quit(); //Only works in .exe not in Unity Editor
    }

}
