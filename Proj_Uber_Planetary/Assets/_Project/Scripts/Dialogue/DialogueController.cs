using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using UberPlanetary.ScriptableObjects;
using UberPlanetary.Dialogue;

namespace UberPlanetary.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        // Dialogue Box objects
        public TextMeshProUGUI custName;
        public TextMeshProUGUI dialogueBox;
        public Image custFace;

        public TextAnimator textAnimator;
        private TextAnimatorPlayer textAnimatorPlayer;

        [SerializeField]
        private CustomerSO customerSO;
        [SerializeField]
        private DialogueSO dialogueSO;
        public DialogueTrigger dialogueTrigger;
    
        public bool isStarted;
        public bool IsShowing { get; set; }

        private int _lineIndex;

        private void OnEnable()
        {
            InitiateDialogue();
        }
        private void Awake()
        {
            dialogueTrigger = GetComponentInParent<DialogueTrigger>();
            textAnimatorPlayer = GetComponent<TextAnimatorPlayer>();
        }
        private void Update()
        {

            if (IsShowing || !isStarted) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_lineIndex >= dialogueSO.lines.Length - 1)
                {
                    FinishDialogue();
                    return;
                }
                DisplayText(dialogueSO.lines[++_lineIndex]);
            }
        }
        public void InitiateDialogue()
        {
            isStarted = true;
            custName.text = customerSO.CustomerName;
            custFace.color = new Color(1, 1, 1, 1);
            custFace.sprite = customerSO.CustomerFace;
            textAnimatorPlayer.ShowText(dialogueSO.lines[0]);
        }
        public void DisplayText(string textToDisplay)
        {
            textAnimatorPlayer.ShowText(textToDisplay);
            Debug.Log(_lineIndex + " comparing to: " + dialogueSO.lines.Length);
        }
        public void FinishDialogue()
        {
            dialogueTrigger.TurnOff();
        }

    }
}

