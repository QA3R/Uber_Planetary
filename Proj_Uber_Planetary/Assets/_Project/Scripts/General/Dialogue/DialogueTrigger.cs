using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UberPlanetary.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public GameObject dialogueWindow;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogueWindow.SetActive(true);
            } 
        }
        public void TurnOff()
        {
            dialogueWindow.SetActive(false);
        }

    }
}

