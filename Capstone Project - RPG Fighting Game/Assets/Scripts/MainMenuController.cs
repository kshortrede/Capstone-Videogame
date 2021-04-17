/// <summary>
/// Kenneth Shortrede
/// Menu --- Main Menu controller with functions for each button
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//SceneLoaderState will let the Loading scene know where to go next
//This way, only 1 loading scene is required, instead of 1 for every possible transition
public enum SceneLoaderState { NONE, STARTMAIN, LOADMAIN, STARTMAZE, MAZEFROMFIGHT, OPENMAINFROMMAZE, FIGHTFROMMAZE}

public class MainMenuController : MonoBehaviour
{
    
    public static SceneLoaderState loadingState;


    private void Awake()
    {
        loadingState = SceneLoaderState.STARTMAIN;
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        //progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }

    public void StartGame(float waitTime)
    {
        StartCoroutine(StartGameAsync(waitTime));
    }
    private IEnumerator StartGameAsync(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Loading");
    }
    
    public void Load(float waitTime)
    {
        loadingState = SceneLoaderState.LOADMAIN;
        StartGame(waitTime);
    }
    public void Exit()
    {
        print("Get out!");
        Application.Quit(); //Only works in .exe not in Unity Editor
    }
    public void Highlight()
    {

    }
    
}
