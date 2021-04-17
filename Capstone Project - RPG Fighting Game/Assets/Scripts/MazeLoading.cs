/// <summary>
/// Kenneth Shortrede
/// Loading --- A middle place between scenes 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Maze Loading will show a loading scene while asynchronously loading the required new scene
public class MazeLoading : MonoBehaviour
{
    Image progressBar;

    private void Awake()
    {
        progressBar = GameObject.Find("LoadingBar").GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        yield return null;
        //Load scene in the background - Async Operation
        //Fill the bar = Async Op. progress

        AsyncOperation asyncLoad;
        if (MainMenuController.loadingState.Equals(SceneLoaderState.STARTMAZE) 
            || MainMenuController.loadingState.Equals(SceneLoaderState.MAZEFROMFIGHT)){
            asyncLoad = SceneManager.LoadSceneAsync("MazeScene");
        } else if(MainMenuController.loadingState.Equals(SceneLoaderState.FIGHTFROMMAZE))
        {
            asyncLoad = SceneManager.LoadSceneAsync("FightScene");
        }
        else
        {
            asyncLoad = SceneManager.LoadSceneAsync("MissionScene");
        }
        
        //Check if we are ready to load
        while(asyncLoad.progress < 1)
        {
            progressBar.fillAmount = asyncLoad.progress;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForEndOfFrame();


    }
}
