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
        private int _characterIndex; //<- Remove Unused stuff like this. Find others and remove them too NOTE.

        //exposed fields
        [SerializeField]private float autoPlayDialogueTime;
        [SerializeField] private CustomerSO customerSO;
        [SerializeField] private DialogueSO dialogueSO;

        //See which ones are not referenced in other scripts NOTE
        //Fields that are used just in this class and are assigned though the inspector or not at all should be private and serialized NOTE
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
        public bool IsShowing { get; set; } // Why is this a property and not a regular bool? NOTE
                                            // // If its a property that is being used by scripts externally make a backing private bool that is used for this script. NOTE
        public bool HasDialogue => dialogueSO != null;



        private void Awake()
        {
            //Extract assignment into its own function NOTE
            dialogueTrigger = GetComponentInParent<DialogueTrigger>();
            textAnimatorPlayer = textAnimator.GetComponent<TextAnimatorPlayer>();
            _rideManager = FindObjectOfType<RideManager>();
            ToggleDialogueBox(false);
        }

        private void Start()
        {
            //Extract subscriptions into its own function NOTE
            _rideManager.onCustomerPickedUp.AddListener(StartDialogue);
            _rideManager.onCustomerDroppedOff.AddListener(EndDialogue);
        }
        
        //Turns the dialogue box on and off
        public void ToggleDialogueBox(bool state) //See if this can be made private? NOTE
        {
            dialogueWindow.SetActive(state);
            dialogueBox.gameObject.SetActive(state);
        }
        //pulls customer information from other scripts toggles dialogue window on
        public void StartDialogue(CustomerSO customerData)  //See if this can be made private? NOTE
        {
            customerSO = customerData;
            dialogueSO = customerSO.CustomerDialogue;
            ToggleDialogueBox(true);

            InitiateDialogue();
        }
        //populates the window with customer information and begins running dialogue lines
        public void InitiateDialogue()  //See if this can be made private? NOTE
        {
            isStarted = true;
            custName.text = customerSO.CustomerName;
            custFace.color = new Color(1, 1, 1, 1);
            custFace.sprite = customerSO.CustomerFace;
            textAnimatorPlayer.ShowText(dialogueSO.dialogueLines[_lineIndex++].line);
        }

        //Extract update logic to its own function. See what is common among that function and extract that into its own place NOTE
        private void Update()
        {
            if (IsShowing || !isStarted || !HasDialogue) return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_lineIndex >= dialogueSO.dialogueLines.Length) // eg for the comment above, here and on line 107 its the same code written twice. NOTE
                {
                    FinishDialogue();
                    return;
                }
                custName.text = dialogueSO.dialogueLines[_lineIndex].characterName; // Why is this done here? // Looks like this should be done in the display text function NOTE
                custFace.sprite = dialogueSO.dialogueLines[_lineIndex].characterSpeaking; // Why is this done here? NOTE
                DisplayText(dialogueSO.dialogueLines[_lineIndex++].line); // Upon further investigation I believe that displayText should be changed, check comments there. NOTE

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
        public void DisplayText(string textToDisplay) // This should perhaps take a Dialogue instead. (Its the non mono class we made to expand the SO) NOTE
        {
            //Here the function should assign the appropriate things based on the Dialogue's fields NOTE
            textAnimatorPlayer.ShowText(textToDisplay);
            _timeBetweenDialogue = 0;
            //At the end here it should increase the _lineIndex++; NOTE
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