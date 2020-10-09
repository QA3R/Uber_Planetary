using System;
using System.Collections.Generic;
using UberPlanetary.Quests;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UberPlanetary.Phone.Applications
{
    public class UberApplication : BaseApplication
    {
        private RideManager _rideManager;

        [SerializeField] private List<CustomerSO> customerSos; 

        private void Awake()
        {
            _rideManager = FindObjectOfType<RideManager>();
        }

        public void GenerateNewCustomer()
        {
            if(_rideManager.IsRideActive || customerSos.Count < 1) return;
            var rand = Random.Range(0, customerSos.Count - 1);
            
            _rideManager.AcceptRide(customerSos[rand]);

            customerSos.Remove(customerSos[rand]);
        }
        
    }
}
