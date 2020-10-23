using Febucci.UI;
using TMPro;
using UberPlanetary.Rides;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Dialogue
{
    /// <summary>
    /// Controls dialogue flow and translates customer data into UI
    /// </summary>
    public class DialogueController : MonoBehaviour
    {
        //private members
        private RideManager _rideManager;
        private float _timeBetweenDialogue;
        private TextAnimatorPlayer textAnimatorPlayer; //NOTE: Naming convention
        private int _lineIndex;
        private bool _isStarted;
        private bool _isShowing;

        private Dialogue[] _dialogueArray;
        private bool _hasDelivered;

        //exposed fields
        [SerializeField] private float autoPlayDialogueTime;
        [SerializeField] private CustomerSO customerSO; //NOTE: This and the one below are private fields I think?
        [SerializeField] private DialogueSO dialogueSO;

        //NOTE: Looks like these should be serialized private fields since we only change them though inspector(Drag and drop)
        // public fields
        public TextMeshProUGUI custName; 
        public TextMeshProUGUI dialogueBox;
        public GameObject dialogueWindow;
        public Image custFace;


        //public properties
        public bool HasDialogue => dialogueSO != null; // NOTE: this can be made private too...
        
        
        private void Awake()
        {
            AssignReferences();
            ToggleDialogueBox(false);
        }
        private void Start()
        {
            EventSubscriber();
        }

        private void AssignReferences()
        {
            textAnimatorPlayer = FindObjectOfType<TextAnimatorPlayer>();
            _rideManager = FindObjectOfType<RideManager>();
        }
        //NOTE: This is good practice, add another function at the bottom, OnDisable or destroy? RemoveListener
        //you should always do the same with events and delegates too!
        private void EventSubscriber()
        {
            _rideManager.onCustomerPickedUp.AddListener(StartDialogue);
            _rideManager.onCustomerDroppedOff.AddListener(EndDialogue);
        }

        //Turns the dialogue box on and off
        private void ToggleDialogueBox(bool state)
        {
            dialogueWindow.SetActive(state);
            dialogueBox.gameObject.SetActive(state);
        }
        //pulls customer information from other scripts toggles dialogue window on
        private void StartDialogue(CustomerSO customerData)
        {
            customerSO = customerData;
            dialogueSO = customerSO.CustomerDialogue;
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
            custName.text = customerSO.CustomerName;
            custFace.color = new Color(1, 1, 1, 1);
            custFace.sprite = customerSO.CustomerFace;
            textAnimatorPlayer.ShowText(_dialogueArray[_lineIndex++].line);
        }

        private void Update()
        {
            if (_isShowing || !_isStarted || !HasDialogue) return;

            if (Input.GetKeyDown(KeyCode.F)) //NOTE: In the next meeting I'll show you how to get input though the Input Handler class instead.
            {
                EndCheck();
            }
            _timeBetweenDialogue = Mathf.Clamp(_timeBetweenDialogue, 0,autoPlayDialogueTime);
            if (_timeBetweenDialogue >= autoPlayDialogueTime)
            {
                EndCheck();
            }
            else
            {
                _timeBetweenDialogue += Time.deltaTime;
            }
        }

        //plays text with typewriter effect
        public void DisplayText(Dialogue dialogue)
        {
            custName.text = dialogue.characterName;
            custFace.sprite = dialogue.characterSpeaking;
            textAnimatorPlayer.ShowText(dialogue.line);
            _timeBetweenDialogue = 0;
            _lineIndex++;
        }

        // NOTE: Check end implies it just checks the condition for ending. But the function is also  calling display text here.
        //I'll show you a case where I use exactly the same function but you'll see how I don't make it a void
        //checks to see if dialogue is over and if not, plays the next line
   
        private void EndCheck() 
        {
            //NOTE: Code formatting indentation issues
            if (_lineIndex >= _dialogueArray.Length)
            {
                FinishDialogue();
                return;
            }
            DisplayText(_dialogueArray[_lineIndex]);
        }

        //called at the end of the ride to run dialogue end methods
        // Needs to start the dropOffLines
        public void EndDialogue(CustomerSO customerData)
        {
            customerSO = customerData;
            dialogueSO = customerSO.CustomerDialogue;

            Debug.Log("The drop off dialogue length is " + customerSO.CustomerDialogue.dropOffLines.Length);
            if (customerSO.CustomerDialogue.dropOffLines.Length < 1)
            {
                FinishDialogue();
                return;
            }
            
            ToggleDialogueBox(true);
            _lineIndex = 0;
            StartDialogue(customerSO.CustomerDialogue.dropOffLines);
        }

        public void ClearDialogueBox()
        {
            dialogueBox.text = "";
        }
        
        public void ClearCustomerData()
        {
            customerSO = null;
            dialogueSO = null;
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
    }
}