using System;
using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UberPlanetary.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Phone.Applications
{
    /// Just a test class for dumy functions
    public class BaseApplication : MonoBehaviour , IPhoneApplication
    {
        //Events exposed to Unity so we can drag and drop things, for eg, on select we might drag in a audio source and play a selection sound etc.
        [SerializeField] private UnityEvent OnEnter, OnExit, OnSelect, OnDeselect;
        
        public void Enter()
        {
            //Debug.Log("Entering :" + gameObject.name);
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            //Debug.Log("Exiting :" + gameObject.name);
            OnExit?.Invoke();
        }

        public void Select()
        {
            //Debug.Log("<color=blue>HIGHLIGHTED </color>" + gameObject.name);
            OnSelect?.Invoke();
        }

        public void Deselect()
        {
            //Debug.Log("<color=red>UNHIGHLIGHTED </color>" + gameObject.name);
            OnDeselect?.Invoke();
        }
        
        public void DisplayNotification()
        {
            Debug.Log("Displaying Notification for :" + gameObject.name);
        }
        
        public void GoToMainMenu(int index)
        {
            SceneLoader.instance.LoadSceneSingle(index);
        }

        public void ExitGame()
        {
            SceneLoader.instance.QuitGame();
        }
    }
}
