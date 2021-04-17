/// <summary>
/// Kenneth Shortrede
/// Mission System --- Mission Parent
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public enum MissionType { EXECUTE, DELIVERY, ESCORT, GATHER}

/// <summary>
/// For future iterations, Mission could be a ScriptableObject for a much cleaner Hierarchy
/// Then we would create all missions on the Assets, and non-programmers could have an easy time adding new missions
/// </summary>

//Contains the necessary information for all the different children classes (MissionExecute, MissionDelivery)
//Can't be instantiated
[System.Serializable]
public abstract class Mission : MonoBehaviour//ScriptableObject
{
    public string Title;
    public string Description;
    public GameObject NPCOwner;

    protected PlayerGUINonFighting playerGUI;

    protected GameObject QuestFinishedPanel;
    protected Text QuestFinished;
    
    public int GoldReward;
    public int ExpReward;

    //Dialogues for Mission Start, During Mission, Mission Finished
    public NPCConversation[] Dialogues = new NPCConversation[3];

    [SerializeField]
    protected int _activeDialogue;
    public int ActiveDialogue
    {
        get { return _activeDialogue; }
        set { _activeDialogue = value; }
    }
    //Who owns the mission
    /*[SerializeField]
    protected GameObject NPCGiver;
    public GameObject GetNPCGiver()
    {
        return NPCGiver;
    }*/

    //Used to update the Player's active missions
    protected GameObject Player;

    [SerializeField]
    protected int _missionState; //0 = not started, 1 = started, 2 = Completed, 3 = Finished (reward received)
    

    private static void NewMissionOption()
    {

    }

    public int MissionState
    {
        get { return _missionState; }
        set { _missionState = value; }
    }

    private void Start()
    {
        QuestFinishedPanel.SetActive(false);
    }
    private void Awake()
    {       
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player == null) { print("error0"); }
        QuestFinishedPanel = GameObject.Find("QuestFinishedPanel");
        QuestFinished = GameObject.Find("QuestFinishedTextDisplay").GetComponent<Text>();
        NPCOwner = this.gameObject;
        playerGUI = GameObject.Find("PlayerGUI").GetComponent<PlayerGUINonFighting>();
    }
}

/// <summary>
/// Probably no use for this class unless I change the implementation of the NPCs as well
/// </summary>
[CreateAssetMenu(fileName = "New Mission List", menuName = "Mission System/MissionsList")]
public class MissionList : ScriptableObject
{
    public List<Mission> CompleteMissionList = new List<Mission>();
}
