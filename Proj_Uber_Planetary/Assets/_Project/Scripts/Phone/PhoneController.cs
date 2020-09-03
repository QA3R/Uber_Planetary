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

        private IScrollHandler _scrollHandler;

        private void Awake()
        {
            AssignComponents();
        }

        private void AssignComponents()
        {
            _inputProvider = GameObject.Find("PlayerShip").GetComponent<IInputProvider>();
            _scrollHandler = GetComponent<IScrollHandler>();
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
                phone.transform.position -= offset;
                return;
            }
            phone.transform.position += offset;
        }

        private void OnClick()
        {
            if(!isActive) return;
            // GetCurrentNavigable . Enter()
        }

        private void Scroll(float val)
        {
            if(!isActive) return;
            _scrollHandler.Scroll(val);
        }

        private void AssignDelegates()
        {
            _inputProvider.ClickInfo[KeyCode.Mouse2].OnDown += TogglePhone;
            _inputProvider.ClickInfo[KeyCode.Mouse1].OnDown += OnClick;
            _inputProvider.OnScroll += Scroll;
        }
    }
}
