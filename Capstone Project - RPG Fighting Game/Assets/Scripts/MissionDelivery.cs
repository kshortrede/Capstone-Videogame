/// <summary>
/// Kenneth Shortrede
/// Mission System --- Mission Delivery
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//Class for missions that require to deliery something from one NPC to another
//

//[CreateAssetMenu(fileName ="New Delivery Mission Object", menuName = "Mission System/Missions/Delivery")]
public class MissionDelivery : Mission
{
    /// <summary>
    /// Need to set the values for Rewards and Dialogue on Inspector
    /// Need to set valus for enemy type and amount on Inspector
    /// </summary>

    [SerializeField]
    private MissionType MissionType;

    public string ItenName;

    public GameObject NPCReceiver;
    public bool FinishOnDeliver; //If true

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
    public void StartMission()
    {
        ActiveDialogue = 1;
        NPCReceiver.GetComponent<NPCInteractions>().ExpectingDelivery = true;
        //if(!MainMenuController.loadingState.Equals(SceneLoaderState.OPENMAINFROMMAZE)){
            Player.GetComponent<Player>().AddMission(this);
        //}
    }

    public void CompleteMission()
    {
        ActiveDialogue = 2;

        //Change Message based on whether mission ends on the delivery or you have to go back to get payment
        if (FinishOnDeliver)
        {
            QuestFinished.text = "Congratulations!\nPackage Delivered.";
        } else
        {
            QuestFinished.text = "Congratulations! Package Delivered\nGo back to " + this.name + " to receive your reward.";
        }
        
        QuestFinishedPanel.SetActive(true);
        QuestFinishedPanel.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(ShowMessage());
        if (FinishOnDeliver)
        {
            ChangeMissionState(3);
        }
    }
    public void FinishMission()
    {
        //Give rewards to the player
        //Remove Mission from Player's list
		Character.gold += this.GoldReward;
		LevelSystem.AddExp(this.ExpReward);
        Player.GetComponent<Player>().RemoveMission(this);
        playerGUI.SetHUD();
        //this.GetComponent<NPCInteractions>().IsMissionAvailable = false;
    }

    //Show message for a couple of seconds
    public IEnumerator ShowMessage()
    {

        yield return new WaitForSeconds(3);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            QuestFinishedPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;
        }
        QuestFinishedPanel.SetActive(false);
    }
}
