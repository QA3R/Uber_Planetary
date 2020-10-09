using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
    public static SceneLoader instance { get { return _instance; } }
    
    void Awake ()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("Instance was found");
        }
        else
        {
            _instance = this;
            Debug.Log("Instance was not found");
            DontDestroyOnLoad(gameObject);
        }
    }


   // Loads the scene using LoadSceneMode.Additive
   public void LoadSceneAdditive (int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
        Debug.Log(sceneIndex + " was loaded");
        Time.timeScale = 1;
    }

    // Loads the scene using LoadSceneMode.Single
    public void LoadSceneSingle (int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        Debug.Log(sceneIndex + " was loaded");
        Time.timeScale = 1;
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Successfully Closed");
    }
}
