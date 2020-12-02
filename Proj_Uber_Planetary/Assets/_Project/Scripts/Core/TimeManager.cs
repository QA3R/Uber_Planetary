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

            _dialogueCanvas = GameObject.FindGameObjectWithTag("Dialogue");
            _audioSource = _dialogueCanvas.GetComponentInChildren<AudioSource>();
            _dialogueHistory = _dialogueCanvas.GetComponentInChildren<DialogueHistory>();

            foreach (Transform child in _dialogueCanvas.transform)
            {
                if (child.GetComponent<DialogueController>())
                {
                    //Gets the child of the GameObject that has the DialogueController.cs attached to it
                    Transform targetTrans = child.transform.GetChild(0);

                    //Checks all the children of the rargetTrans for the GameObject with the TextAnimator and TextAnimatorPlayer
                    foreach (Transform child2 in targetTrans.transform)
                    {
                        if (child2.GetComponent<TextAnimator>())
                            _textAnimator = child2.GetComponent<TextAnimator>();

                        if (child2.GetComponent<TextAnimatorPlayer>())
                            _textAnimatorPlayer = child2.GetComponent<TextAnimatorPlayer>();
                    }
                }
            }

            if (_textAnimatorPlayer != null)
            {
                _textAnimator.timeScale = TextAnimator.TimeScale.Scaled;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        {
            Debug.Log("subscribed to PauseTime and UnpauseTime");
            _dialogueHistory.TimePaused += PauseTime;
            _dialogueHistory.TimeUnpaused += UnpauseTime;
        }

        private void OnDisable()
        {

            Debug.Log("subscribed to PauseTime and UnpauseTime");
            _dialogueHistory.TimePaused -= PauseTime;
            _dialogueHistory.TimeUnpaused -= UnpauseTime;
        }
        #endregion

        #region Methods
        [ContextMenu("Pause Time")]
        void PauseTime(int timeScale)
        {
            Debug.Log("paused");
            Time.timeScale = timeScale;
            
            if (_audioSource)
            {
                _audioSource.Pause();
            }
        }

        [ContextMenu("Unpause Time")]
        void UnpauseTime(int timeScale)
        {
            Time.timeScale = timeScale;

            if (_audioSource)
            { 
                _audioSource.UnPause();
            }
        }
        #endregion
    }
}


