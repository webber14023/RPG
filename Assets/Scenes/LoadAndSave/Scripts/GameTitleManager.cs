using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class GameTitleManager : MonoBehaviour
{
    public SaveLoadManager saveLoad;
    public GameObject confirmScreen;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void NewGameButton() {
        if(File.Exists(Application.dataPath + "/JsonData.save")) {
            confirmScreen.SetActive(true);
        }
        else {
            StartCoroutine(LoadingNewGame());
        }
    }

    public void LoadButton() {
        if(File.Exists(Application.dataPath + "/JsonData.save")) {
            StartCoroutine(LoadingGame());
        }
    }

    public void ExitButton() {
        Application.Quit();
    }

    public void ConfirmButton() {
        File.Delete (Application.dataPath + "/JsonData.save");
        StartCoroutine(LoadingNewGame());
    }

    public void CancelButton() {
        confirmScreen.SetActive(false);
    }

    private IEnumerator LoadingNewGame() {
        var asyncLoadLevel = SceneManager.LoadSceneAsync("Vallage", LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone){
            Debug.Log("Loading the Scene"); 
            yield return null;
        }
        saveLoad.NewSaveByJson();
        saveLoad.LoadByJson();
        
    }
    
    private IEnumerator LoadingGame() {
        var asyncLoadLevel = SceneManager.LoadSceneAsync("Vallage", LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone){
            Debug.Log("Loading the Scene"); 
            yield return null;
        }
        saveLoad.LoadByJson();
    }
}
