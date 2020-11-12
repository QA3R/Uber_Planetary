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

        private void TakeAwayControls()
        {
            _playerController.MovementAxisModifier = 0;
            _playerController.RotationAxisModifier = 0;
            _phoneController.CanToggle = false;
        }

        private void GivePhoneControl()
        {
            _phoneController.CanToggle = true;
        }

        private void GiveMovementControl()
        {
            _playerController.MovementAxisModifier = 1;
        }

        private void GiveRotationControl()
        {
            _playerController.RotationAxisModifier = 1;
        }

        private void RideAccepted(CustomerSO customerSo)
        {
            GiveMovementControl();
            Invoke(nameof(GiveRotationControl), 2f);
            _rideManager.onRideAccepted.RemoveListener(RideAccepted);
        }
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
                if(_index ==1)
                {
                    GivePhoneControl();
                }
                yield return new WaitForSeconds(_audioSource.clip.length + bufferBetweenAudio);
                _index++;
            }
        }
    }
}