using System;
using System.Collections.Generic;
using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UnityEngine;

namespace UberPlanetary.Phone
{
    /// Populates a list of navigable and sets the function
    public class NavigableListProvider : MonoBehaviour
    {
        [SerializeField] private List<GameObject> navigableObjects;
        [SerializeField] private String phoneObjectName;
        private readonly List<IPhoneNavigable> _currentList = new List<IPhoneNavigable>();
        private IPhoneNavigator _phoneNavigator;

        private void Awake()
        {
            //finding the refrence
            _phoneNavigator = GameObject.Find(phoneObjectName).GetComponent<IPhoneNavigator>();
            
            //populating list
            for (int i = 0; i < navigableObjects.Count; i++)
            {
                _currentList.Add(navigableObjects[i].GetComponent<IPhoneNavigable>());
            }
        }

        //Sets the value of the navigator's list to be this class's list, called through unity.
        public void SetNavigatorList()
        {
            _phoneNavigator.NavigableList = _currentList;
        }
    }
}