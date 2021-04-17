/// <summary>
/// Kenneth Shortrede
/// Saving System --- Container for Player Data
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps all the information connected to the Player needed to restore a previous state on load
[System.Serializable]
public class PlayerData
{
    float[] _position;
    //Persistent Locations
    public float[] Position
    {
        set { _position = value; }
        get { return _position; }
    }

    //Persistent Stats
    public int MaxHealth, CurrentHealth, Experience, Gold, Level, ExperienceToNextLevel, Damage, MaxMana, CurrentMana;
	public float Speed;
    //public int EnemiesKilled;

    //Persistent Missions
    /// <summary>
    /// Find the missions by looking up the NPCs and using the MissionName
    /// Then add the mission to the player, and update its state
    /// </summary>
    public MissionData[] ActiveMissions = new MissionData[4];

    public PlayerData(GameObject player)
    {
		MaxHealth = Character.maxHealth;
        CurrentHealth = Character.currentHealth;
        Experience = Character.exp;
		ExperienceToNextLevel = Character.expToNextLevel;
        Gold = Character.gold;
		Level = Character.level;
		Damage = Character.damage;
		MaxMana = Character.maxMana;
		CurrentMana = Character.currentMana;
		Speed = Character.speed;

        if(player != null)
        {
            Position = new float[3];
            Position[0] = player.transform.position.x;
            Position[1] = player.transform.position.y;
            Position[2] = player.transform.position.z;
        }

        generateMissionData();
    }

    void generateMissionData()
    {
        for(int i = 0; i < 4; i++)
        {
            if(Player.ActiveMissions[i] != null)
            {
                Debug.Log("Generating..." + Player.ActiveMissions[i].Title);
                ActiveMissions[i] = new MissionData();
                ActiveMissions[i].NPCName = Player.ActiveMissions[i].NPCOwner.name;
                ActiveMissions[i].MissionName = Player.ActiveMissions[i].Title;
                if(Player.ActiveMissions[i] is MissionExecute)
                {
                    ActiveMissions[i].enemiesKilled = (Player.ActiveMissions[i] as MissionExecute).AmountKilled;
                } else
                {
                    ActiveMissions[i].enemiesKilled = 0;
                }
            } else
            {
                ActiveMissions[i] = new MissionData();
                ActiveMissions[i].NPCName = "null";
                ActiveMissions[i].MissionName = "null";
            }
        }
    }

}

//Keep the necessary data to reload and find the correct mission on load
[System.Serializable]
public class MissionData
{
    public string NPCName = "";
    public string MissionName = "";
    public int enemiesKilled;
}
