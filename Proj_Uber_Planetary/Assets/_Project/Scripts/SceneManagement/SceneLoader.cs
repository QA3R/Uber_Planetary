using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UberPlanetary.SceneManagement
{
    /// <summary>
    /// Manages the scene changes
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader _instance;
        public static SceneLoader instance { get { return _instance; } }

        public static event Action onSceneSwitched;
    
        void Awake ()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                //Debug.Log("Instance was found");
            }
            else
            {
                _instance = this;
                //Debug.Log("Instance was not found");
                DontDestroyOnLoad(gameObject);
            }
        }
        
        // Loads the scene using LoadSceneMode.Additive
        public void LoadSceneAdditive (int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
            //Debug.Log(sceneIndex + " was loaded");
            Time.timeScale = 1;
        }

        // Loads the scene using LoadSceneMode.Single
        public void LoadSceneSingle (int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
            //Debug.Log(sceneIndex + " was loaded");
            Time.timeScale = 1;
            onSceneSwitched?.Invoke();
        }
    
        public void QuitGame()
        {
            Application.Quit();
            //Debug.Log("Game Successfully Closed");
        }
    }
}
