using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UberPlanetary.ScriptableObjects;
using TMPro;


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

        [SerializeField] private GameObject screenBorder;
        [SerializeField] private RectTransform dialogueHistory;
        [SerializeField] private GameObject dialoguePrefab;
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private Transform dialogueContainer;


        #endregion

        #region OnEnable, and OnDisable Methods
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
            screenBorder.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        public void CloseDialogueHistory()
        {
            screenBorder.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        #endregion
    }
}
