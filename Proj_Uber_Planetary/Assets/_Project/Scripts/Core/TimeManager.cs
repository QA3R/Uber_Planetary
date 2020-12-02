using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using UberPlanetary.ScriptableObjects;
using UberPlanetary.Dialogue;

namespace UberPlanetary.Core
{
    public class TimeManager : MonoBehaviour
    {
        /// <summary>
        /// This script is responsible for pausing and unpausing the game
        /// </summary>

        #region Variables
        private static TimeManager _instance;
        public static TimeManager Instance => _instance;
        private TextAnimatorPlayer _textAnimatorPlayer;
        private GameObject _dialogueCanvas;
        private Transform targetTrans;


        private TextAnimator _textAnimator;
        private AudioSource _audioSource;
        private DialogueHistory _dialogueHistory;
        #endregion

        #region Awake, Start, OnEnable, and OnDisable
        void Awake()
        {
            // Checks if there is a pre-existing TimeManager
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                //Sets the instance of the TimeManager to this object and assigns this Object as DontDestroyOnLoad
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _dialogueCanvas = GameObject.FindGameObjectWithTag("Dialogue");
            _audioSource = _dialogueCanvas.GetComponentInChildren<AudioSource>();
            _dialogueHistory = _dialogueCanvas.GetComponentInChildren<DialogueHistory>();

            //Looks for the GameObject with the DialogueController.cs attached
            foreach (Transform child in _dialogueCanvas.transform)
            {
                if (child.GetComponent<DialogueController>())
                {
                    //Gets the child of the GameObject that has the DialogueController.cs attached to it
                    targetTrans = child.transform.GetChild(0);

                }
            }

            if (targetTrans != null) 
            { 
                //Looks for the GameObject with the TextAnimator and TextAnimatorPlayer attached
                foreach (Transform child2 in targetTrans)
                {
                    if (child2.GetComponent<TextAnimator>())
                        _textAnimator = child2.GetComponent<TextAnimator>();

                    if (child2.GetComponent<TextAnimatorPlayer>())
                        _textAnimatorPlayer = child2.GetComponent<TextAnimatorPlayer>();
                }

                if (_textAnimatorPlayer != null)
                {
                    _textAnimator.timeScale = TextAnimator.TimeScale.Scaled;
                }
            }
        }

        #endregion

        #region Methods
        [ContextMenu("Pause Time")]
        public void PauseTime()
        {
            Debug.Log("paused");
            Time.timeScale = 0;
            
            if (_audioSource)
            {
                _audioSource.Pause();
            }
        }

        [ContextMenu("Unpause Time")]
        public void UnpauseTime()
        {
            Time.timeScale = 1;

            if (_audioSource)
            { 
                _audioSource.UnPause();
            }
        }
        #endregion
    }
}


