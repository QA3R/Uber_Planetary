using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UberPlanetary
{
    public class LoadMainMenu : MonoBehaviour
    {
        public float timeToLoad;
        
        private void Start()
        {
            StartTransition(timeToLoad);
        }

        public void StartTransition(float time)
        {
            Invoke(nameof(GoToMenu), time);
        }
        
        private void GoToMenu()
        {
            SceneManager.LoadScene("MainMenu_Scene");
        }
    }
}
