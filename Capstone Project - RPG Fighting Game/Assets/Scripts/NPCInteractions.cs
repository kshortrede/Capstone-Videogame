/// <summary>
/// Kenneth Shortrede
/// NPC System --- "Brain" of the NPC
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;

//
// Allows the NPC to contain specific missions to give to the place
// Allows the NPC to have and select the correct dialogue when talking to the place
//

public class NPCInteractions : MonoBehaviour
{
    //Only set MissionAvailable to false, if the NPC won't ever give a mission to the player
    //Not if the mission is finished
    public bool IsMissionAvailable;
    public Mission MissionToGive;
    public Mission[] MissionsAvailable;
    public int MissionIndex = 0;
    
    public NPCConversation NormalDialogue;
    public NPCConversation MaxMissionsReachedDialogue;

    /// <summary>
    /// Variables for NPCs who are going to be receivers of packages
    /// Useful only for Delivery Missions
    /// </summary>
    public bool IsReceiver;
    public bool ExpectingDelivery;
    public NPCConversation ReceiveDelivery;


    public void Start()
    {
        if (IsMissionAvailable)
        {
            print("Found the mission");
            MissionToGive = MissionsAvailable[MissionIndex];
        }
    }

    /// <summary>
    /// Plays the correct dialogue based on the active parameters of the NPC 
    /// and the ability of the Player to obtain a new mission
    /// </summary>
    /// <param name="PlayerCanMission"></param>
    public void PlayDialogue(bool PlayerCanMission)
    {
        if (ExpectingDelivery)
        {
            ConversationManager.Instance.StartConversation(ReceiveDelivery);
            ExpectingDelivery = false;
        } else
        {
            //If player can't take any more missions, and we have a dialogue for that, play it
            if (!PlayerCanMission && MaxMissionsReachedDialogue != null)
            {
                ConversationManager.Instance.StartConversation(MaxMissionsReachedDialogue);
            }
            else
            {
                //If NPC has no mission to give, go into normal dialogue
                //Else, play one of the 3 dialogues from the Mission, based on the state of the mission
                if (!IsMissionAvailable)
                {
                    ConversationManager.Instance.StartConversation(NormalDialogue);
                }               
                else
                {
                    print(MissionToGive.Dialogues[MissionToGive.ActiveDialogue].transform.name);
                    ConversationManager.Instance.StartConversation(MissionToGive.Dialogues[MissionToGive.ActiveDialogue]);
                }
            }
        }
    }

    //**********************************************************************************************************************
    //The following section is only for givers of Mission

    /// <summary>
    /// Activates the Mission given to the player by changing the state
    /// </summary>
    public void BeginMission()
    {
        //Show Popup message here to let the user know a mission has been added
        //if(MissionToGive == null) { MissionToGive = MissionsAvailable[MissionIndex]; }
        if(MissionToGive is MissionExecute)
        {
            (MissionToGive as MissionExecute).ChangeMissionState(1);
            
        }
        else
        {
            (MissionToGive as MissionDelivery).ChangeMissionState(1);
        }
        GameObject.Find("SoundEffectsController").GetComponent<SoundEffectsController>().Accepted();
    }

    /// <summary>
    /// De-Activates Mission, gives rewards to player by changing the State
    /// De-Activates the availability of the Mission in the NPC
    /// </summary>
    public void FinishMission()
    {
        if(MissionToGive is MissionExecute)
        {
            (MissionToGive as MissionExecute).ChangeMissionState(3);
            //StartCoroutine((MissionToGive as MissionExecute).ShowMessage());
        } else
        {
            (MissionToGive as MissionDelivery).ChangeMissionState(3);
        }
        if(MissionIndex < MissionsAvailable.Length - 1)
        {
            MissionIndex += 1;
            MissionToGive = MissionsAvailable[MissionIndex];
            IsMissionAvailable = true;
        } else
        {
            IsMissionAvailable = false;
        }
        GameObject.Find("SoundEffectsController").GetComponent<SoundEffectsController>().Completed();
    }
}
