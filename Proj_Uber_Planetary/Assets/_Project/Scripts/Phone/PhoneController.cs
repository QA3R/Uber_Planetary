using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Phone
{
    /// Delegates tasks to other scripts based on provided input
    public class PhoneController : MonoBehaviour
    {
        //the input interface, gives us access to all the button presses and scroll etc
        private IInputProvider _inputProvider;
        
        private bool isActive;
        private AudioSource _audioSource;
        
        [SerializeField] private Vector3 offset;
        [SerializeField] private GameObject phone;
        [SerializeField] private Canvas canvas;
        [SerializeField] private AudioClip pullUp, pullDown;
        [SerializeField] private UnityEvent onStart;
        [SerializeField] private UnityEvent onPhonePulledUp;

        //The Phone Navigator interface, lets us access the currently selected item and scroll through the rest.
        private IPhoneNavigator _phoneNavigator;

        private void Awake()
        {
            AssignComponents();
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void Start()
        {
            _inputProvider = GameObject.Find("PlayerShip").GetComponent<IInputProvider>();
            AssignDelegates();
            onStart?.Invoke();
        }

        public void TempPlayAudio()
        {
            if (isActive)
            {
                _audioSource.clip = pullUp;
            }
            else
            {
                _audioSource.clip = pullDown;
            }
            _audioSource.Play();
        }


        /// When the middle mouse button is pressed, do this
        private void TogglePhone()
        {
            //Manage internal state of the phone, is it currently usable or not?
            isActive = !isActive;
            onPhonePulledUp?.Invoke();
            //TODO: Animate phone based on state
            //When its active move the phone up
            if (isActive)
            {
                phone.transform.position -= offset * canvas.scaleFactor;
                return;
            }
            //When its inactive move the phone down
            phone.transform.position += offset * canvas.scaleFactor;
        }

        /// When the left mouse button is pressed, do this
        private void OnLeftClick()
        {
            //when the phone is not active do nothing
            if(!isActive) return;
            
            //when the phone is active call the Enter function of the currently selected icon/feature.
            _phoneNavigator.CurrentNavigable.Enter();
        }

        /// When the right mouse button is pressed, do this
        private void OnRightClick()
        {
            //when the phone is not active do nothing
            if(!isActive) return;
            
            //when the phone is active call the Exit function of the currently selected icon/feature.
            _phoneNavigator.CurrentNavigable.Exit();
        }

        private void Scroll(float val)
        {
            //when the phone is not active do nothing
            if(!isActive) return;
            
            //Pass in the value of the scroll input to the navigator
            _phoneNavigator.Scroll(val);
        }

        /// Asks inputProvider to let it know when the events are called (Gets added to the list of things to notify on event being invoked)
        private void AssignDelegates()
        {
            _inputProvider.ClickInfo[KeyCode.Mouse2].OnDown += TogglePhone;
            _inputProvider.ClickInfo[KeyCode.Mouse1].OnDown += OnRightClick;
            _inputProvider.ClickInfo[KeyCode.Mouse0].OnDown += OnLeftClick;
            _inputProvider.OnScroll += Scroll;
        }
        
        private void AssignComponents()
        {
            _phoneNavigator = GetComponent<IPhoneNavigator>();
        }
    }
}
