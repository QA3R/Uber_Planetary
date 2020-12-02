using Febucci.UI;
using TMPro;
using UberPlanetary.Rides;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace UberPlanetary.Dialogue
{
    /// <summary>
    /// Controls dialogue flow and translates customer data into UI
    /// </summary>
    
    public class DialogueController : MonoBehaviour
    {
        #region Variables
        //private members
        private RideManager _rideManager;
        private TextAnimatorPlayer _textAnimatorPlayer;
        private int _lineIndex;
        private bool _isStarted;
        private bool _isShowing;
        private Dialogue[] _dialogueArray;
        private bool _hasDelivered;
        private CustomerSO _customerSO;
        private DialogueSO _dialogueSO;
        private AudioSource _audioSource;
        private Action <Dialogue> onDialoguePlayed; 

        //[SerializeField] private float autoPlayDialogueTime;
        [SerializeField] private float timeBetweenDialogue;
        [SerializeField] private TextMeshProUGUI custName;
        [SerializeField] private TextMeshProUGUI dialogueBox;
        [SerializeField] private GameObject dialogueWindow;
        [SerializeField] private Image custFace;
        #endregion

        #region Properites
        //private properties
        private bool HasDialogue => _dialogueSO != null;
        
        public Action <Dialogue> OnDialoguePlayed
        {
            get => onDialoguePlayed;
            set => onDialoguePlayed = value;
        }
        #endregion

        #region OnEnable, OnDisable, Awake and Update Methods
        private void Awake()
        {
            _textAnimatorPlayer = FindObjectOfType<TextAnimatorPlayer>();
            _rideManager = FindObjectOfType<RideManager>();
            _audioSource = GetComponent<AudioSource>();
            ToggleDialogueBox(false);
            _lineIndex = 0;
        }

        private void OnEnable()
        {
            _rideManager.onCustomerPickedUp.AddListener(StartDialogue);
            _rideManager.onCustomerDroppedOff.AddListener(EndDialogue);
        }

        private void OnDisable()
        {
            _rideManager.onCustomerPickedUp.RemoveListener(StartDialogue);
            _rideManager.onCustomerDroppedOff.RemoveListener(EndDialogue);
        }

        private void Update()
        {
            if (_isShowing || !_isStarted || !HasDialogue) return;

            //Checks if there the Audio Source is playing and the game isn't paused before calling the EndCheck method
            if (!_audioSource.isPlaying && Time.timeScale == 1)
            {
                //Checks if the EndCheck method returns true before calling hte DisplayText method
                if (!EndCheck())
                {
                    DisplayText(_dialogueArray[_lineIndex]);
                }
            }
        }
        #endregion

        #region Methods
        //Turns the dialogue box on and off
        private void ToggleDialogueBox(bool state)
        {
            dialogueWindow.SetActive(state);
            dialogueBox.gameObject.SetActive(state);
        }

        // Takes in the CustomerSO and assigns the information to the DialogueBox while toggling it on
        private void StartDialogue(CustomerSO customerData)
        {
            _customerSO = customerData;
            _dialogueSO = _customerSO.CustomerDialogue;
            ToggleDialogueBox(true);
            InitiateDialogue(customerData.CustomerDialogue.dialogueLines);
        }

        // Takes in and passes the DialgoueArray to the InitiateDialogue method
        private void StartDialogue(Dialogue[] dialogues)
        {

            InitiateDialogue(dialogues);

        }

        //populates the window with customer information and begins running dialogue lines
        private void InitiateDialogue(Dialogue[] dialogues)
        {
            _dialogueArray = dialogues;
            _isStarted = true;
            custFace.color = new Color(1, 1, 1, 1);
            DisplayText(dialogues[_lineIndex]);
        }

        //plays text with typewriter effect
        public void DisplayText(Dialogue dialogue)
        {
            //Assigns the text in the dialogue box to the correct line index of the CustomerSO's dialgoue passed in
            custName.text = _dialogueArray[_lineIndex].characterName;
            custFace.sprite = _dialogueArray[_lineIndex].characterSprite;
            _textAnimatorPlayer.ShowText(_dialogueArray[_lineIndex].line);

            //Checks if there is a voiceOver clip within the dialogue before assigning the text and playing the audio clip
            if (dialogue.voiceOver != null)
            {
                _audioSource.clip = dialogue.voiceOver;
                _audioSource.Play();
                _textAnimatorPlayer.ShowText(dialogue.line);
                timeBetweenDialogue = 0;

                //Checks if the Action is null before invoking and passing the dialogue to the DialogueHistory.cs
                if (onDialoguePlayed != null)
                {
                    onDialoguePlayed(dialogue);
                }

                _lineIndex++;
            }
        }

        //checks to see if dialogue is over and if not, calls the FinishDialogue method
        private bool EndCheck() 
        {
            if( _lineIndex >= _dialogueArray.Length)
            {
                FinishDialogue();
                return true;
            }
            return false;
        }

        //called at the end of the ride to run dialogue end methods
        // Needs to start the dropOffLines
        public void EndDialogue(CustomerSO customerData)
        {
            _customerSO = customerData;
            _dialogueSO = _customerSO.CustomerDialogue;
            _audioSource.Stop();

            if (_customerSO.CustomerDialogue.dropOffLines == null  || _customerSO.CustomerDialogue.dropOffLines?.Length < 1)
            {
                FinishDialogue();
                return;
            }
            
            ToggleDialogueBox(true);
            _lineIndex = 0;
            StartDialogue(_customerSO.CustomerDialogue.dropOffLines);
        }


        //Clears the dialogueBox text
        public void ClearDialogueBox()
        {
            dialogueBox.text = "";
        }
        
        //Clears the temp variables used in script
        public void ClearCustomerData()
        {
            _customerSO = null;
            _dialogueSO = null;
            custName.text = null;
            custFace.sprite = null;
        }

        //toggles dialogue box off and sets dialogue condition back to false
        public void FinishDialogue()
        {
            _isStarted = false;
            ToggleDialogueBox(false);
            _lineIndex = 0;
            ClearCustomerData();
            ClearDialogueBox();
        }
        #endregion
    }
}