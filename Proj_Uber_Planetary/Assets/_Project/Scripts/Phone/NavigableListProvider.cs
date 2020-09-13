using System;
using System.Collections.Generic;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Phone
{
    public class NavigableListProvider : MonoBehaviour
    {
        [SerializeField] private List<GameObject> navigableObjects;
        [SerializeField] private String phoneObjectName;
        private readonly List<IPhoneNavigable> _currentList = new List<IPhoneNavigable>();
        private IPhoneNavigator _phoneNavigator;

        private void Awake()
        {
            _phoneNavigator = GameObject.Find(phoneObjectName).GetComponent<IPhoneNavigator>();
            
            for (int i = 0; i < navigableObjects.Count; i++)
            {
                _currentList.Add(navigableObjects[i].GetComponent<IPhoneNavigable>());
            }
        }

        public void SetNavigatorList()
        {
            _phoneNavigator.NavigableList = _currentList;
        }
    }
}