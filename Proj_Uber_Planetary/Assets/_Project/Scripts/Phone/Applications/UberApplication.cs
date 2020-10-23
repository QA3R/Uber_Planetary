using System.Collections.Generic;
using UberPlanetary.Rides;
using UberPlanetary.ScriptableObjects;
using UberPlanetary.Phone.ApplicationFeature;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UberPlanetary.Phone.Applications
{
    public class UberApplication : BaseApplication
    {
        private RideManager _rideManager;
        private RideLoader _rideLoader;
        private List<CustomerSO> _customerSos = new List<CustomerSO>();

        [SerializeField] GameObject customerListItemPrefab; 
        [SerializeField] NavigableListProvider listProvider;
        [SerializeField] Transform listHolder;


        public NavigableListProvider ListProvider => listProvider;

        private void Awake()
        {
            _rideManager = FindObjectOfType<RideManager>();
            _rideLoader = _rideManager.GetComponent<RideLoader>();
            _customerSos.Clear(); //test
        }

        private void Start()
        {
            PopulateList();
            //_rideManager.onCustomerDroppedOff.AddListener(DoSomething);
            RideLoader.onHashSetUpdated += PopulateList;
            //CheckAvailablelist();
        }

        public void GenerateNewCustomer()
        {
            if(_rideManager.IsRideActive || _customerSos.Count < 1) return;
            var rand = Random.Range(0, _customerSos.Count - 1);
            
            _rideManager.AcceptRide(_customerSos[rand]);

            _customerSos.Remove(_customerSos[rand]);
        }

        public bool TryAcceptNewCustomer(CustomerSO so)
        {
            if(_rideManager.IsRideActive || !_customerSos.Contains(so)) return false;
            _rideManager.AcceptRide(so);
            _customerSos.Remove(so);
            return true;
        }
        
        private void PopulateList()
        {
            foreach (var customerSo in RideLoader.CurrentCustomerList)
            {
                if (!_customerSos.Contains(customerSo))
                {
                    _customerSos.Add(customerSo);
                    GameObject tempListItem = Instantiate(customerListItemPrefab, listHolder);
                    tempListItem.GetComponent<CustomerListItem>().Init(customerSo, this);
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
