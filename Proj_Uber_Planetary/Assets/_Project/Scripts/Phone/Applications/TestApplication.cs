using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Phone.Applications
{
    public class TestApplication : MonoBehaviour , IPhoneApplication
    {
        
        public void Enter()
        {
            Debug.Log("Entering :" + gameObject.name);
        }

        public void Exit()
        {
            Debug.Log("Exiting :" + gameObject.name);
        }

        public void OnSelect()
        {
            //throw new System.NotImplementedException();
            Debug.Log("<color=blue>HIGHLIGHTED </color>" + gameObject.name);

        }

        public void OnDeselect()
        {
            //throw new System.NotImplementedException();
            Debug.Log("<color=red>UNHIGHLIGHTED </color>" + gameObject.name);

        }


        public void DisplayNotification()
        {
            Debug.Log("Displaying Notification for :" + gameObject.name);
        }
    }
}
