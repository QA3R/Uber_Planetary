using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UberPlanetary.ScriptableObjects;
using TMPro;
using UberPlanetary.Core;


namespace UberPlanetary.Dialogue
{
    public class DialogueHistory : MonoBehaviour
    {
        /// <summary>
        /// This script is responsible for populating the dialogue history.
        /// </summary>

        #region Variables
        private TextMeshProUGUI _dialogueLine;
        private TextMeshProUGUI _characterDialogue;
        private List <string> _dialogueList;
        private Action <int> _timePaused;
        private Action <int> _timeUnpaused;
        private TimeManager _timeManager;

        [SerializeField] private GameObject canvas;
        [SerializeField] private RectTransform dialogueHistory;
        [SerializeField] private GameObject dialoguePrefab;
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private Transform dialogueContainer;
        #endregion

        #region Properties
        public Action <int> TimePaused 
        {
            get => _timePaused;
            set => _timePaused = value;
        }

        public Action <int> TimeUnpaused 
        {
            get => _timeUnpaused;
            set => _timeUnpaused = value;
        }
        #endregion


        #region Start, OnEnable, and OnDisable Methods
        private void Start()
        {
            _timeManager = GameObject.FindObjectOfType<TimeManager>();
        }

        private void OnEnable()
        {
            dialogueController.OnDialoguePlayed += PopulateDialogue;
        }

        private void OnDisable()
        {
            dialogueController.OnDialoguePlayed -= PopulateDialogue;
        }
        #endregion

        #region Methods
        [ContextMenu("Populate")]
        void PopulateDialogue(Dialogue dialogue)
        {
            Debug.Log("pp");
            GameObject _tempObj = Instantiate(dialoguePrefab, dialogueContainer);

            _tempObj.GetComponent<TextMeshProUGUI>().text = dialogue.characterName;
            _tempObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogue.line;
            dialogueHistory.offsetMin = new Vector2 (dialogueHistory.offsetMin.x, (dialogueHistory.offsetMin.y - 500));
        }

        public void OpenDialogueHistory()
        {
            canvas.SetActive(true);
            Cursor.visible = true;

            if (_timeManager != null)
                _timeManager.PauseTime();
        }

        public void CloseDialogueHistory()
        {
            canvas.SetActive(false);
            Cursor.visible = false;

            if (_timeManager != null)
                _timeManager.UnpauseTime();
        }
        #endregion
    }
}
