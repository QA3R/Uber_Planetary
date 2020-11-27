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

        //exposed fields
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

            if (!_audioSource.isPlaying)
            {
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

        //pulls customer information from other scripts toggles dialogue window on
        private void StartDialogue(CustomerSO customerData)
        {
            _customerSO = customerData;
            _dialogueSO = _customerSO.CustomerDialogue;
            ToggleDialogueBox(true);
            InitiateDialogue(customerData.CustomerDialogue.dialogueLines);
        }

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
            /*custName.text = _dialogueArray[_lineIndex].characterName;
            custFace.color = new Color(1, 1, 1, 1);
            custFace.sprite = _dialogueArray[_lineIndex].characterSprite;
            _textAnimatorPlayer.ShowText(_dialogueArray[_lineIndex].line);*/
        }

        /*
        IEnumerator DisplayText(Dialogue dialogue)
        {
            custName.text = dialogue.characterName;
            custFace.sprite = dialogue.characterSprite;
            if (dialogue.voiceOver != null)
            {
                _audioSource.clip = dialogue.voiceOver;
                _audioSource.Play();
            }
            _textAnimatorPlayer.ShowText(dialogue.line);
            _timeBetweenDialogue = 0;
            _lineIndex++;
            yield return new WaitForSeconds(2);
        }
        */

        //plays text with typewriter effect
        public void DisplayText(Dialogue dialogue)
        {
            //custName.text = dialogue.characterName;
            //custFace.sprite = dialogue.characterSprite;

            custName.text = _dialogueArray[_lineIndex].characterName;
            custFace.sprite = _dialogueArray[_lineIndex].characterSprite;
            _textAnimatorPlayer.ShowText(_dialogueArray[_lineIndex].line);

            if (dialogue.voiceOver != null)
            {
                _audioSource.clip = dialogue.voiceOver;
                _audioSource.Play();
                _textAnimatorPlayer.ShowText(dialogue.line);
                timeBetweenDialogue = 0;

                if (onDialoguePlayed != null)
                {
                    onDialoguePlayed(dialogue);
                }

                _lineIndex++;
            }
        }

        //checks to see if dialogue is over and if not, plays the next line
   
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

        public void ClearDialogueBox()
        {
            dialogueBox.text = "";
        }
        
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