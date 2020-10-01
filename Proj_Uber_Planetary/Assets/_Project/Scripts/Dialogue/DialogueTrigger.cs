using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UberPlanetary.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        private DialogueController dController;
        public CustomerInfoHandler custInfo;

        private void Awake()
        {
            dController = GetComponent<DialogueController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dController.InitiateDialogue();
            }
        }


    }
}

