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
        private TextAnimatorPlayer textAnimatorPlayer;
        private int _lineIndex;
        private bool _isStarted;
        private bool _isShowing;

        //exposed fields
        [SerializeField]private float autoPlayDialogueTime;
        [SerializeField] private CustomerSO customerSO;
        [SerializeField] private DialogueSO dialogueSO;

        // public fields
        public TextMeshProUGUI custName; 
        public TextMeshProUGUI dialogueBox;
        public GameObject dialogueWindow;
        public Image custFace;


        //public properties
        public bool HasDialogue => dialogueSO != null;



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
            textAnimatorPlayer = GetComponent<TextAnimatorPlayer>();
            _rideManager = FindObjectOfType<RideManager>();
        }
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

            InitiateDialogue();
        }
        //populates the window with customer information and begins running dialogue lines
        private void InitiateDialogue()
        {
            _isStarted = true;
            custName.text = customerSO.CustomerName;
            custFace.color = new Color(1, 1, 1, 1);
            custFace.sprite = customerSO.CustomerFace;
            textAnimatorPlayer.ShowText(dialogueSO.dialogueLines[_lineIndex++].line);
        }

        private void Update()
        {
            if (_isShowing || !_isStarted || !HasDialogue) return;

            if (Input.GetKeyDown(KeyCode.F))
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

        //checks to see if dialogue is over and if not, plays the next line
        private void EndCheck()
        {
            if (_lineIndex >= dialogueSO.dialogueLines.Length)
                {
                    FinishDialogue();
                    return;
                }
                DisplayText(dialogueSO.dialogueLines[_lineIndex]);
        }

        //called at the end of the ride to run dialogue end methods
        public void EndDialogue(CustomerSO customerData)
        {
            ClearCustomerData();
            ClearDialogueBox();
            FinishDialogue();
            _lineIndex = 0;
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
        }
    }
}