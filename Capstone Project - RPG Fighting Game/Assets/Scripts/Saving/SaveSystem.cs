/// <summary>
/// Kenneth Shortrede
/// Saving System --- Main
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

/// <summary>
/// SaveSystem takes charge of the following:
/// * Save NPC Information
/// * Save Player Information
/// * Load NPC Information
/// * Load Player Information
/// * Load/Update status of missions when coming out of maze 
/// </summary>
public static class SaveSystem
{
    public static readonly string PathEndPlayer = "/player.capstone";
    public static readonly string PathEndNPC = "/NPC.capstone";

    public static readonly string MazePersistenceInfo = "/NPCInformation.capstone";
    public static readonly string MazePersistenceInfoPlayer = "/PlayerInformation.capstone";

    public static string[] MissionNames = new string[Player.ActiveMissions.Length];
    public static string[] MissionDescriptions = new string[Player.ActiveMissions.Length];

    /*public static void LoadGameFromMainMenu()
    {
        SceneManager.LoadScene("MissionScene");
        PauseMenuController.PauseMenuGlobal.Load();
    }*/

    //Save Player data on a binary formatted file
    public static void SavePlayer(GameObject player, string endPath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + endPath;
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }
    //Save NPC data on a binary formatted file
    public static void SaveNPC(string endPath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + endPath;

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            NPCData data = new NPCData();
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    //Load Player data from a binary formatted file into PlayerData object
    public static PlayerData LoadGame(string endPath)
    {
        string path = Application.persistentDataPath + endPath;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData game = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return game;
        }
        else
        {
            Debug.LogError("Save file not found" + path);
            return null;
        }
    }

    //Load NPC data from a binary formatted file into NPCData object
    public static NPCData LoadGameNPC(string endPath)
    {
        string path = Application.persistentDataPath + endPath;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            NPCData game = formatter.Deserialize(stream) as NPCData;
            stream.Close();

            return game;
        }
        else
        {
            Debug.LogError("Save file not found" + path);
            return null;
        }
    }

    /// <summary>
    /// Updates all the missions based on the killed obtained in the Maze
    /// </summary>
    /// <param name="player"></param>
    /*public static void UpdateMissionKills(GameObject player)
    {
        foreach(KeyValuePair<EnemyType, int> pair in EnemiesKilled.enemiesKilled)
        {
            int tmp = pair.Value;
            while(tmp > 0)
            {
                player.GetComponent<Player>().AddKill(pair.Key, pair.Value);
            }
        }
    }*/
}
