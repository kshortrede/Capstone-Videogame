/// <summary>
/// Kenneth Shortrede
/// Player --- Player data for the Mission Scene
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Contains all the necessary data to maintain a list of missions, ability to talk to NPCs, onGoingDialogue
//Contains methods to allow for interactions with GameObjects in the world (such as NPC)
public class Player : MonoBehaviour
{
    /// <summary>
    /// Change ActiveMissions to private
    /// </summary>
    //[SerializeField]
    public static Mission[] ActiveMissions;

    public int MaxMissionsAllowed = 4;
    private int MaxActiveMissions;
    private int CurrentActiveMissions;

    //Eliminate later
    bool CanTalkToNPC = false;

    [SerializeField]
    private GameObject MissionsActivePanel;
    [SerializeField]
    private Transform[] MissionsInPanel;

    private GameObject TalkToNPCMessagePanel;
    private GameObject CurrentNPCDetected;
    private bool OnGoingDialogue;

    private void Awake()
    {
        MissionsActivePanel = GameObject.Find("ActiveQuestsPanel");
        TalkToNPCMessagePanel = GameObject.Find("TalkToNPCPanel");
        TalkToNPCMessagePanel.SetActive(false);
        MissionsInPanel = new Transform[MaxMissionsAllowed];
        for (int i = 1; i <= MaxMissionsAllowed; i++)
        {
            MissionsInPanel[i - 1] = MissionsActivePanel.transform.GetChild(i);
        }
        MissionsActivePanel.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        MaxActiveMissions = MaxMissionsAllowed;
        
        if(Player.ActiveMissions == null)
        {
            print("Reiniciar Misiones");
            Player.ActiveMissions = new Mission[MaxActiveMissions];
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UpdateMissionPanel();
            MissionsActivePanel.SetActive(!MissionsActivePanel.activeSelf);
        }
        if (CanTalkToNPC == true && Input.GetKeyDown(KeyCode.E) && OnGoingDialogue == false)
        {
            TalkToNPCMessagePanel.SetActive(false);
            OnGoingDialogue = true;
            TalkToNPC(CurrentNPCDetected);
        }
    }
    public void EndOnGoingDialogueTrigger()
    {
        OnGoingDialogue = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    public bool GetOnGoingDialogue()
    {
        return OnGoingDialogue;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC" && CurrentNPCDetected == null && OnGoingDialogue == false)
        {
            StartCoroutine(ShowMessage());
            //TalkToNPCMessagePanel.SetActive(true);
            CanTalkToNPC = true;
            CurrentNPCDetected = other.gameObject;
            //TalkToNPC(other.gameObject);
        }

        //
        // For Testing Purposes only - Delete afterwards
        //
        if(other.tag == "ExecutionPlain")
        {
            AddKill(EnemyType.ZOMBIE, 1);
        }

        if(other.tag == "MazeEntrance")
        {
            SaveSystem.SaveNPC(SaveSystem.MazePersistenceInfo);
            
            //This will likely be removed
            SaveSystem.SavePlayer(null, SaveSystem.MazePersistenceInfoPlayer);

            
            for(int i = 0; i < Player.ActiveMissions.Length; i++)
            {
                if(Player.ActiveMissions[i] != null)
                {
                    SaveSystem.MissionNames[i] = Player.ActiveMissions[i].Title ?? "";
                    SaveSystem.MissionDescriptions[i] = Player.ActiveMissions[i].Description ?? "";
                } else
                {
                    SaveSystem.MissionNames[i] = "";
                    SaveSystem.MissionDescriptions[i] = "";
                }
                
            }
            MainMenuController.loadingState = SceneLoaderState.STARTMAZE;
            SceneManager.LoadScene("Loading");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (CurrentNPCDetected != null && other.name.Equals(CurrentNPCDetected.name))
        {
            print("Can't talk anymore");
            CurrentNPCDetected = null;
            CanTalkToNPC = false;
        }
    }
    
    public void TalkToNPC(GameObject NPC)
    {
        CanTalkToNPC = true;
        bool CanGetMission = false;
        if(MaxActiveMissions > CurrentActiveMissions) { CanGetMission = true; }

        NPCInteractions tmp = NPC.GetComponent<NPCInteractions>();
        tmp.PlayDialogue(CanGetMission);
    }
    //Update the missions in the Panel
    //This method could be moved to a different script more related to GUI, to have a bigger abstraction between the player and the 2D GUI
    public void UpdateMissionPanel()
    {
        print("Update Mission Panel: Missions in Panel" + MissionsInPanel.Length + "\n" + "Missions Available" + Player.ActiveMissions.Length);

        int tempPos = 0;
        for (int i = 0; i < MissionsInPanel.Length; i++)
        {
            if(tempPos > Player.ActiveMissions.Length) { break; }
            for (int j = tempPos; j < Player.ActiveMissions.Length; j++)
            {
                //print("-");
                if (Player.ActiveMissions[j] != null)
                {
                    Text titleText = MissionsInPanel[i].GetChild(0).GetComponent<Text>();
                    Text descriptionText = MissionsInPanel[i].GetChild(1).GetComponent<Text>();
                    titleText.text = Player.ActiveMissions[j].Title;
                    descriptionText.text = Player.ActiveMissions[j].Description;
                    tempPos = j + 1;
                    break;
                }
                else
                {
                    Text titleText = MissionsInPanel[i].GetChild(0).GetComponent<Text>();
                    Text descriptionText = MissionsInPanel[i].GetChild(1).GetComponent<Text>();
                    titleText.text = "No Mission Active";
                    descriptionText.text = "";
                }
            }
        }
    }
    public void AddMission(Mission mission)
    {
        for(int i = 0; i < Player.ActiveMissions.Length; i++)
        {
            if(Player.ActiveMissions[i] == null)
            {
                print("Add Mission");
                Player.ActiveMissions[i] = mission;
                CurrentActiveMissions += 1;
                UpdateMissionPanel();
                break;
            }
        }
    }
    public void RemoveMission(Mission mission)
    {
        for(int i = 0; i < Player.ActiveMissions.Length; i++)
        {
            if (Player.ActiveMissions[i] != null && Player.ActiveMissions[i].name.Equals(mission.name))
            {
                Player.ActiveMissions[i] = null;
                CurrentActiveMissions -= 1;
                UpdateMissionPanel();
                break;
            }
        }
    }

    public void AddKill(EnemyType enemyKilledType, int amount)
    {
        print("Killed new enemy");
        for(int i = 0; i < Player.ActiveMissions.Length; i++)
        {
            if(Player.ActiveMissions[i] != null)
            {
                (Player.ActiveMissions[i] as MissionExecute).AddKill(enemyKilledType);
                print("Enemy type killed was: " + enemyKilledType);
            }
        }
    }
    public void AddDelivery(GameObject receiverNPC)
    {
        for(int i = 0; i < Player.ActiveMissions.Length; i++)
        {
            if(Player.ActiveMissions[i] is MissionDelivery && (Player.ActiveMissions[i] as MissionDelivery).NPCReceiver.Equals(receiverNPC))
            {
                (Player.ActiveMissions[i] as MissionDelivery).ChangeMissionState(2);
            }
        }
    }
    //Show message for a couple of seconds
    public IEnumerator ShowMessage()
    {
        TalkToNPCMessagePanel.GetComponent<CanvasGroup>().alpha = 1;
        TalkToNPCMessagePanel.SetActive(true);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            TalkToNPCMessagePanel.GetComponent<CanvasGroup>().alpha -= 0.1f;
        }
        TalkToNPCMessagePanel.SetActive(false);
    }
}
