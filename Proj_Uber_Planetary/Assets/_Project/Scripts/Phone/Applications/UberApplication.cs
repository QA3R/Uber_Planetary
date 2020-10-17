using System.Collections.Generic;
using UberPlanetary.Rides;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UberPlanetary.Phone.Applications
{
    public class UberApplication : BaseApplication
    {
        private RideManager _rideManager;
        private RideLoader _rideLoader;

        [SerializeField] private List<CustomerSO> customerSos; 
        [SerializeField] GameObject _customerListItemPrefab; 
        [SerializeField] NavigableListProvider _listProvider; 

        private void Awake()
        {
            _rideManager = FindObjectOfType<RideManager>();
            _rideLoader = _rideManager.GetComponent<RideLoader>();
            customerSos.Clear(); //test
            PopulateList();
        }

        private void Start()
        {
            //_rideManager.onCustomerDroppedOff.AddListener(DoSomething);
            RideLoader.onHashSetUpdated += PopulateList;
            //CheckAvailablelist();
        }

        public void GenerateNewCustomer()
        {
            if(_rideManager.IsRideActive || customerSos.Count < 1) return;
            var rand = Random.Range(0, customerSos.Count - 1);
            
            _rideManager.AcceptRide(customerSos[rand]);

            customerSos.Remove(customerSos[rand]);
        }

        public bool TryAcceptNewCustomer(CustomerSO so)
        {
            if(_rideManager.IsRideActive || !customerSos.Contains(so)) return false;
            _rideManager.AcceptRide(so);
            customerSos.Remove(so);
            return true;
        }
        
        private void PopulateList()
        {
            foreach (var customerSo in RideLoader.CurrentCustomerList)
            {
                if (!customerSos.Contains(customerSo))
                {
                    customerSos.Add(customerSo);
                    //Instantiate(_customerListItemPrefab, transform);
                    //_customerListItemPrefab.GetComponent<CustomerListItem>().Init(customerSo);
                }
            }
            //_listProvider.UpdateList();
        }

        private void OnDestroy()
        {
            //_rideManager.onCustomerDroppedOff.RemoveListener(DoSomething);
            RideLoader.onHashSetUpdated -= PopulateList;
        }
    }
}
