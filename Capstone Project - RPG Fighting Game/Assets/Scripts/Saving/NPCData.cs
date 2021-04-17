/// <summary>
/// Kenneth Shortrede
/// Saving System --- Container for NPC data
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to keep a list of all the NPCs with the necessary data to load them back to their needed state
[System.Serializable]
public class NPCData
{
    public List<NPC> NPCLists = new List<NPC>();

    public NPCData()
    {
        GameObject[] allNPCs = GameObject.FindGameObjectsWithTag("NPC");

        for(int i = 0; i < allNPCs.Length; i++)
        {
            NPCLists.Add(new NPC(allNPCs[i]));
        }
    }
}

//Class to keep the information of an NPC to be able to restore it back to its previous state on load 
[System.Serializable]
public class NPC
{
    public string name;
    float[] _position;
    //Persistent Locations
    public float[] Position
    {
        set { _position = value; }
        get { return _position; }
    }

    public int MissionIndex;
    public bool IsMissionAvailable;
    public int[] MissionStates;

    public NPC(GameObject NPC)
    {
        name = NPC.name;
        Position = new float[3];
        Position[0] = NPC.transform.position.x;
        Position[1] = NPC.transform.position.y;
        Position[2] = NPC.transform.position.z;

        NPCInteractions tmp = NPC.GetComponent<NPCInteractions>();
        IsMissionAvailable = tmp.IsMissionAvailable;
        if (IsMissionAvailable)
        {
            MissionIndex = tmp.MissionIndex;
            MissionStates = new int[tmp.MissionsAvailable.Length];
            for (int i = 0; i < tmp.MissionsAvailable.Length; i++)
            {
                MissionStates[i] = tmp.MissionsAvailable[i].MissionState;
            }
        }

    }
}
