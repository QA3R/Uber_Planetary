using System.Collections;
using UberPlanetary.Phone;
using UberPlanetary.Player.Movement;
using UberPlanetary.Rides;
using UberPlanetary.ScriptableObjects;
using UnityEngine;

namespace UberPlanetary.OnBoarding
{
    [RequireComponent(typeof(AudioSource))]
    public class OnBoardingManager : MonoBehaviour
    {
        private PlayerController _playerController;
        private PhoneController _phoneController;
        private RideManager _rideManager;
        private int _index = 0;
        private AudioSource _audioSource;
        
        [SerializeField] private int tutorialCanvasTime;
        [SerializeField] private GameObject tutorialCanvas;
        [SerializeField] private GameObject controlTutorialCanvas;
        [SerializeField] private AudioClip[] tutorialSequenceAudio;
        [SerializeField] private int indexToFocusPhone;
        [SerializeField] private float bufferBetweenAudio;
        
        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _phoneController = FindObjectOfType<PhoneController>();
            _rideManager = FindObjectOfType<RideManager>();
            _audioSource = GetComponent<AudioSource>();
            TakeAwayControls();
            _rideManager.onCustomerDroppedOff.AddListener(FinishOnBoarding);
            _rideManager.onRideAccepted.AddListener(RideAccepted);
            //Invoke(nameof(GivePhoneControl), 3f);
            PlayAudioClip();
        }

        //Removes player movement capabilites
        private void TakeAwayControls()
        {
            _playerController.MovementAxisModifier = 0;
            _playerController.RotationAxisModifier = 0;
            _phoneController.CanToggle = false;
        }

        //Allows player phone control
        private void GivePhoneControl()
        {
            tutorialCanvas.SetActive(true);
            _phoneController.CanToggle = true;

        }

        // Gives player Movement control
        private void GiveMovementControl()
        {
            _playerController.MovementAxisModifier = 1;
        }

        //Gives player rotation Control
        private void GiveRotationControl()
        {
            _playerController.RotationAxisModifier = 1;
        }

        //Will Give player full controls on accepting client
        private void RideAccepted(CustomerSO customerSo)
        {
            GiveMovementControl();
            Invoke(nameof(GiveRotationControl), 2f);
            _rideManager.onRideAccepted.RemoveListener(RideAccepted);
            tutorialCanvas.SetActive(false);
            StartCoroutine(OnboardingControlsPanel());
        }
        
        //Destroys itself when the onboarding process is completed
        private void FinishOnBoarding(CustomerSO customerSo)
        {
            //Do what we need to do
            _rideManager.onCustomerDroppedOff.RemoveListener(FinishOnBoarding);
            Destroy(gameObject);
        }

        private void PlayAudioClip()
        {
            StartCoroutine(OnBoardingAudio());
            // _index++;
        }

        private IEnumerator OnBoardingAudio()
        {
            while (_index < tutorialSequenceAudio.Length)
            {
                _audioSource.clip = tutorialSequenceAudio[_index];
                _audioSource.Play();
                // _audioSource.te
                yield return new WaitForSeconds(_audioSource.clip.length + bufferBetweenAudio);
                _index++;
                if(_index ==1)
                {
                    GivePhoneControl();
                }
            }
        }

        private IEnumerator OnboardingControlsPanel ()
        {
            controlTutorialCanvas.SetActive(true);
            yield return new WaitForSeconds(tutorialCanvasTime);
            controlTutorialCanvas.SetActive(false);
        }
    }
}