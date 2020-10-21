using System;
using System.Collections.Generic;
using UberPlanetary.Currency;
using UberPlanetary.ScriptableObjects;
using UnityEngine;

namespace UberPlanetary.Rides
{
    public class RideLoader : MonoBehaviour
    {
        private static List<CustomerSO> _currentCustomerSet = new List<CustomerSO>();
        public static List<CustomerSO> CurrentCustomerList
        {
            get => _currentCustomerSet;
            set => _currentCustomerSet = value;
        }
        
        public static event Action onHashSetUpdated;

        // private CustomerSO _customer;
        // public CustomerSO Customer
        // {
        //     get => _customer;
        //     set
        //     {
        //         _customer = value;
        //         OnRewarded(_customer);
        //     }
        // }

        private void Awake()
        {
            Initialize();
        }

        public void OnRewarded(CustomerSO customerSo)
        {
            CurrencyManager.Instance.Amount += customerSo.CustomerRide.rideRewards.cashReward;
            foreach (var customer in customerSo.CustomerRide.rideRewards.unlockedCustomers)
            {
                if (!_currentCustomerSet.Contains(customer))
                {
                    _currentCustomerSet.Add(customer);
                }
                Debug.Log(customer.CustomerName);
            }
            if (_currentCustomerSet.Contains(customerSo))
            {
                _currentCustomerSet.Remove(customerSo);
            }
            onHashSetUpdated?.Invoke();
        }
        public void Initialize()
        {
            var boop = Resources.LoadAll<CustomerSO>("StartingSO");
            foreach (var so in boop)
            {
                if (!_currentCustomerSet.Contains(so))
                {
                    _currentCustomerSet.Add(so);
                }
            }
        }
    }
}