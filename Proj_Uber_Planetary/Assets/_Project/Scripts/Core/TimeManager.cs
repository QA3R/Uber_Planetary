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
        private GameObject _dialogueUICanvas;
        
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

            _textAnimatorPlayer = GameObject.FindObjectOfType<TextAnimatorPlayer>();
            _textAnimator = GameObject.FindObjectOfType<TextAnimator>();
            _dialogueHistory = GameObject.FindObjectOfType<DialogueHistory>();

            if (_textAnimatorPlayer != null)
            {
                _textAnimator.timeScale = TextAnimator.TimeScale.Scaled;
            }
        }

        private void OnEnable()
        {
            if (_dialogueHistory != null)
            {
                _dialogueHistory.TimePaused += PauseTime;
                _dialogueHistory.TimeUnpaused += UnpauseTime;
            }
        }

        private void OnDisable()
        {
            if (_dialogueHistory != null)
            {
                _dialogueHistory.TimePaused -= PauseTime;
                _dialogueHistory.TimeUnpaused -= UnpauseTime;
            }
        }
        #endregion

        #region Methods
        [ContextMenu("Pause Time")]
        void PauseTime()
        {
            Time.timeScale = 0;
            
            if (_audioSource != null)
            {
                _audioSource.Pause();
            }
        }

        [ContextMenu("Unpause Time")]
        void UnpauseTime()
        {
            Time.timeScale = 1;

            if (_audioSource != null)
            { 
                _audioSource.UnPause();
            }
        }
        #endregion
    }
}


