using System.Collections;
using UberPlanetary.Core.Interfaces;
using UberPlanetary.Currency;
using UberPlanetary.Navigation;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Quests
{
    public class RideManager : MonoBehaviour
    {
        //private members
        private NavigationManager _navigationManager;
        private CurrencyManager _currencyManager;
        private GameObject _player;
        private CustomerSO _currentCustomer;
        private Ride _currentRide;
        private bool _isRunning;
        
        //public events
        public UnityEvent<CustomerSO> onRideAccepted;
        public UnityEvent<CustomerSO> onCustomerPickedUp;
        public UnityEvent<CustomerSO> onCustomerDroppedOff;
        
        //serialized fields
        [SerializeField] private float searchRadius;
        [SerializeField] private PickUpDropOffDelay pickUpDropOff;
        
        public bool IsRideActive => _currentRide != null;
        

        private void Start()
        {
            _navigationManager = FindObjectOfType<NavigationManager>();
            _currencyManager = FindObjectOfType<CurrencyManager>();
            _player = GameObject.Find("PlayerShip");
        }

        public void AcceptRide(CustomerSO customerSo)
        {
            if(IsRideActive) return;

            _currentRide = customerSo.CustomerRide;
            _currentCustomer = customerSo;
            RideAccepted();
        }

        private void RideAccepted()
        {
            if (_currentRide.RideStartLandmark == null)
            {
                _currentRide.RideStartLandmark = _navigationManager.GetRandomLandmarkWithinRadius<ILandmark>(_player.transform.position, searchRadius);
            }

            if (_currentRide.RideStartLandmark == null)
            {
                _currentRide.RideStartLandmark = _navigationManager.GetRandomLandmark<ILandmark>();
            }

            if (_currentRide.RideStartLandmark == null)
            {
                Debug.LogError("No quest landmark could be found.");
                return;
            }
            _currentRide.RideCurrentLandmark = _currentRide.RideStartLandmark;

            onRideAccepted?.Invoke(_currentCustomer);
            _currentRide.RideStartLandmark.OnReached += StartLocationReached;
        }

        private void StartLocationReached()
        {
            _currentRide.RideStartLandmark.OnReached -= StartLocationReached;
            
            if (_currentRide.RideEndLandmark == null)
            {
                _currentRide.RideEndLandmark = _navigationManager.GetFurthestLandmark<ILandmark>(_player.transform.position);
            }
            
            _currentRide.RideCurrentLandmark = _currentRide.RideEndLandmark;
            StartCoroutine(InvokeWithDelay(onCustomerPickedUp, 5f, _currentCustomer));
            _currentRide.RideEndLandmark.OnReached += EndLocationReached;
        }

        private void EndLocationReached()
        {
            _currentRide.RideEndLandmark.OnReached -= EndLocationReached;
            StartCoroutine(InvokeWithDelay(onCustomerDroppedOff, 5f, _currentCustomer));
            RideCompleted();
        }

        private IEnumerator InvokeWithDelay(UnityEvent<CustomerSO> enventToInvoke, float time, CustomerSO data)
        {
            if (_isRunning) yield break;
            _isRunning = true;
            pickUpDropOff.PlayCutscene();
            yield return new WaitForSeconds(time);
            enventToInvoke?.Invoke(data);
            _isRunning = false;
        }

        private void RideCompleted()
        {
            _currencyManager.Amount += _currentRide.RideReward;
            _currentCustomer = null;
            _currentRide = null;
        }

        private void RideFailed()
        {
            //if the time runs out
        }
    }
}
