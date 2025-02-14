﻿using System;
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

            UpdateList();
        }

        public void UpdateList()
        {
            _currentList.Clear();
            //populating list
            //for (int i = 0; i < navigableObjects.Count; i++)
            //{
            //    _currentList.Add(navigableObjects[i].GetComponent<IPhoneNavigable>());
            //}
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<IPhoneNavigable>() != null)
                {
                    _currentList.Add(transform.GetChild(i).GetComponent<IPhoneNavigable>());

                }
            }
        }

        //Sets the value of the navigator's list to be this class's list, called through unity.
        public void SetNavigatorList()
        {
            _phoneNavigator.NavigableList = _currentList;
        }

        public void AddToList(IPhoneNavigable navigable)
        {
            _currentList.Add(navigable);
        }
        public void RemoveFromList(IPhoneNavigable navigable)
        {
            _currentList.Remove(navigable);
        }
    }
}