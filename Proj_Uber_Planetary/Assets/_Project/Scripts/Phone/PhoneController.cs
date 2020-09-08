using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Phone
{
    public class PhoneController : MonoBehaviour
    {
        private IInputProvider _inputProvider;
        private bool isActive;
        [SerializeField] private Vector3 offset;
        [SerializeField] private GameObject phone;
        [SerializeField] private Canvas canvas;

        private IPhoneNavigator _phoneNavigator;

        private void Awake()
        {
            AssignComponents();
        }

        private void AssignComponents()
        {
            _inputProvider = GameObject.Find("PlayerShip").GetComponent<IInputProvider>();
            _phoneNavigator = GetComponent<IPhoneNavigator>();
        }

        private void Start()
        {
            AssignDelegates();
        }

        private void TogglePhone()
        {
            isActive = !isActive;

            if (isActive)
            {
                phone.transform.position -= offset * canvas.scaleFactor;
                return;
            }
            phone.transform.position += offset * canvas.scaleFactor;
        }

        private void OnLeftClick()
        {
            if(!isActive) return;
            // GetCurrentNavigable . Enter()
            _phoneNavigator.GetCurrentNavigable.Enter();
        }

        private void Scroll(float val)
        {
            if(!isActive) return;
            _phoneNavigator.Scroll(val);
        }

        private void AssignDelegates()
        {
            _inputProvider.ClickInfo[KeyCode.Mouse2].OnDown += TogglePhone;
            _inputProvider.ClickInfo[KeyCode.Mouse0].OnDown += OnLeftClick;
            _inputProvider.OnScroll += Scroll;
        }
    }
}
