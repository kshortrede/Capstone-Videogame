/// <summary>
/// Kenneth Shortrede
/// Mission System --- Mission Execute
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
//Class for missions that require to execute X amount of enemies of type Y, before returning to the NPC who had the mission in the first place
//

//[CreateAssetMenu(fileName = "New Execute Mission Object", menuName = "Mission System/Missions/Execute")]
public class MissionExecute : Mission
{
    /// <summary>
    /// Need to set the values for Rewards and Dialogue on Inspector
    /// Need to set valus for enemy type and amount on Inspector
    /// </summary>

    [SerializeField]
    private MissionType MissionType;

    public EnemyType MonsterType; 
    public int Amount;
    [SerializeField]
    public int AmountKilled;

 

    bool tmp;

    // Start is called before the first frame update
    void Start()
    {
        MissionType = MissionType.EXECUTE;
        MissionState = 0;
        AmountKilled = 0;
        ActiveDialogue = 0;
        //CompleteMission();
    }



    //Received a MonsterType kill from the Player, and checks if it is helpful for the mission
    public void AddKill(EnemyType type)
    {
        if(type.Equals(MonsterType))
        {
            AmountKilled += 1;
            CheckMissionFinished();
        }
    }

    //0 = not started, 1 = started, 2 = Completed, 3 = Finished (reward received)
    public void ChangeMissionState(int state)
    {
        switch (state)
        {
            case 1:
                MissionState = 1;
                StartMission();
                print("Mission Started");
                break;
            case 2:
                MissionState = 2;
                CompleteMission();
                print("Mission Completed");
                break;
            case 3:
                MissionState = 3;
                FinishMission();
                print("Mission Ended");
                break;
        }
    }
    public void CheckMissionFinished()
    {
        if(AmountKilled >= Amount)
        {
            ChangeMissionState(2);
        }
    }
    public void StartMission()
    {
        ActiveDialogue = 1;
        if (!MainMenuController.loadingState.Equals(SceneLoaderState.OPENMAINFROMMAZE))
        {
            if(Player == null) { Player = GameObject.FindGameObjectWithTag("Player"); }
            Player.GetComponent<Player>().AddMission(this);
        }
    }
    
    public void CompleteMission()
    {
        ActiveDialogue = 2;
        //Activate Message GUI
        QuestFinished.text = "Congratulations! You just finish the Quest '" + Title + ".'\nGo back to " + this.name + " to receive your reward.";
        //ShowMessage --> Co-Routine to remove message after a couple of seconds
        QuestFinishedPanel.SetActive(true);
        QuestFinishedPanel.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(ShowMessage());
    }
    public void FinishMission()
    {
        //Give rewards to the player
        //Remove Mission from Player's list
		Character.gold += this.GoldReward;
		LevelSystem.AddExp(this.ExpReward);
        Player.GetComponent<Player>().RemoveMission(this);
        playerGUI.SetHUD();
    }

    //Show message for a couple of seconds
    public IEnumerator ShowMessage()
    {
        print("Printing...");
        
        yield return new WaitForSeconds(3);
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            QuestFinishedPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;
        }
        QuestFinishedPanel.SetActive(false);
    }
    
}
