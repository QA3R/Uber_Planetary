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
        private int _characterIndex;

        //exposed fields
        [SerializeField]private float autoPlayDialogueTime;
        [SerializeField] private CustomerSO customerSO;
        [SerializeField] private DialogueSO dialogueSO;

        // public fields
        public TextMeshProUGUI custName;
        public TextMeshProUGUI dialogueBox;
        public GameObject dialogueWindow;
        public Image custFace;
        public TextAnimator textAnimator;
        public DialogueTrigger dialogueTrigger;
        public bool isStarted;
        public string characterTalking;

        //public properties
        public bool IsShowing { get; set; }
        public bool HasDialogue => dialogueSO != null;



        private void Awake()
        {
            dialogueTrigger = GetComponentInParent<DialogueTrigger>();
            textAnimatorPlayer = textAnimator.GetComponent<TextAnimatorPlayer>();
            _rideManager = FindObjectOfType<RideManager>();
            ToggleDialogueBox(false);
        }

        private void Start()
        {
            _rideManager.onCustomerPickedUp.AddListener(StartDialogue);
            _rideManager.onCustomerDroppedOff.AddListener(EndDialogue);
        }
        //Turns the dialogue box on and off
        public void ToggleDialogueBox(bool state)
        {
            dialogueWindow.SetActive(state);
            dialogueBox.gameObject.SetActive(state);
        }
        //pulls customer information from other scripts toggles dialogue window on
        public void StartDialogue(CustomerSO customerData)
        {
            customerSO = customerData;
            dialogueSO = customerSO.CustomerDialogue;
            ToggleDialogueBox(true);

            InitiateDialogue();
        }
        //populates the window with customer information and begins running dialogue lines
        public void InitiateDialogue()
        {
            isStarted = true;
            custName.text = customerSO.CustomerName;
            custFace.color = new Color(1, 1, 1, 1);
            custFace.sprite = customerSO.CustomerFace;
            textAnimatorPlayer.ShowText(dialogueSO.dialogueLines[_lineIndex++].line);

        }

        private void Update()
        {
            if (IsShowing || !isStarted || !HasDialogue) return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_lineIndex >= dialogueSO.dialogueLines.Length)
                {
                    FinishDialogue();
                    return;
                }
                custName.text = dialogueSO.dialogueLines[_lineIndex].characterName;
                custFace.sprite = dialogueSO.dialogueLines[_lineIndex].characterSpeaking;
                DisplayText(dialogueSO.dialogueLines[_lineIndex++].line);

            }
            _timeBetweenDialogue = Mathf.Clamp(_timeBetweenDialogue, 0,autoPlayDialogueTime);
            if (_timeBetweenDialogue >= autoPlayDialogueTime)
            {
                if (_lineIndex >= dialogueSO.dialogueLines.Length)
                {
                    FinishDialogue();
                    return;
                }
                DisplayText(dialogueSO.dialogueLines[_lineIndex++].line);
            }
            else
            {
                _timeBetweenDialogue += Time.deltaTime;
            }
        }
        //plays text with typewriter effect
        public void DisplayText(string textToDisplay)
        {
            textAnimatorPlayer.ShowText(textToDisplay);
            _timeBetweenDialogue = 0;
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
            isStarted = false;
            ToggleDialogueBox(false);
        }
    }
}