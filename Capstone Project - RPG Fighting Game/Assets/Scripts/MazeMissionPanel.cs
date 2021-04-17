/// <summary>
/// Kenneth Shortrede
/// Menu --- Mission Panel for Maze
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//A simpler controller for the Mission panel inside the maze. It manages less information than the one in the Mission Scene
public class MazeMissionPanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Text[] titles;
    public Text[] descriptions;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SaveSystem.MissionNames.Length; i++)
        {
            if (SaveSystem.MissionNames[i] != null && !SaveSystem.MissionNames[i].Equals(""))
            {
                titles[i].text = SaveSystem.MissionNames[i];
                descriptions[i].text = SaveSystem.MissionDescriptions[i];
            } else
            {
                titles[i].text = "No Mission Started";
                descriptions[i].text = "";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(canvasGroup.alpha == 1)
            {
                canvasGroup.alpha = 0;
            } else
            {
                canvasGroup.alpha = 1;
            }
        }
    }
}
