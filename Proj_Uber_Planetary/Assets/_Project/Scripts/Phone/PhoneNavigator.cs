using System;
using System.Collections.Generic;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Phone
{
    public class PhoneNavigator : MonoBehaviour, IPhoneNavigator
    {
        [SerializeField] private float timeBetweenScrolls;
        [SerializeField] private List<GameObject> navigableObjects;
        private float scrollTimer;
        
        private List<IPhoneNavigable> _currentList = new List<IPhoneNavigable>();
        private int _navigableIndex;

        public IPhoneNavigable GetCurrentNavigable { get; private set; }
        

        public List<IPhoneNavigable> NavigableList
        {
            get => _currentList;
            set
            {
                _currentList = value;
                GetCurrentNavigable = _currentList[0];
                NavigableIndex = 0;
            } 
        }
        
        private int NavigableIndex
        {
            get => _navigableIndex;
            set => _navigableIndex = value.Mod(NavigableList.Count);
        }

        private void Awake()
        {
            for (int i = 0; i < navigableObjects.Count; i++)
            {
                _currentList.Add(navigableObjects[i].GetComponent<IPhoneNavigable>());
            }

            //Note: Assigning Navigable List to update current navigable etc, but _currentList is populated internally 
            NavigableList = _currentList;
        }

        private void Update()
        {
            if (scrollTimer > timeBetweenScrolls) return;
            scrollTimer += Time.deltaTime;
        }
        
        public void Scroll(float val)
        {
            if (scrollTimer < timeBetweenScrolls || val == 0) return;
            val = (int)Mathf.Sign(val);
            Debug.Log("VAL "+ val);
            SetCurrentNavigable((int)val);
            scrollTimer = 0;
        }

        private void SetCurrentNavigable(int val)
        {
            NavigableIndex += val;
            Debug.Log("Index "+ NavigableIndex);
            GetCurrentNavigable.OnDeselect();
            GetCurrentNavigable = NavigableList[NavigableIndex];
            GetCurrentNavigable.OnSelect();
        }
    }
}
