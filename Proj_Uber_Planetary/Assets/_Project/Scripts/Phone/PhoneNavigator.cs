using System;
using System.Collections.Generic;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Phone
{
    public class PhoneNavigator : MonoBehaviour, IPhoneNavigator
    {
        private float _scrollTimer;
        private List<IPhoneNavigable> _currentList = new List<IPhoneNavigable>();
        private int _navigableIndex;
        private int _tempIncrement;
        private IPhoneNavigable _currentNavigable;
        
        [SerializeField] private float timeBetweenScrolls;
        [SerializeField] private List<GameObject> navigableObjects;
        
        public IPhoneNavigable CurrentNavigable
        {
            get => _currentNavigable;
            private set
            {
                _currentNavigable?.OnDeselect();
                _currentNavigable = value;
                _currentNavigable?.OnSelect();
            }
        }
        
        public List<IPhoneNavigable> NavigableList
        {
            get => _currentList;
            set
            {
                _currentList = value;
                CurrentNavigable = _currentList[0];
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
        }

        private void Start()
        {
            //Note: Assigning Navigable List to update current navigable etc, but _currentList is populated internally 
            NavigableList = _currentList;
        }

        private void Update()
        {
            if (_scrollTimer > timeBetweenScrolls) return;
            _scrollTimer += Time.deltaTime;
        }
        
        public void Scroll(float val)
        {
            if (_scrollTimer < timeBetweenScrolls || Mathf.Abs(val) < 0.1f) return;
            _tempIncrement = (int)Mathf.Sign(val);
            SetCurrentNavigable(_tempIncrement);
            _scrollTimer = 0;
        }

        private void SetCurrentNavigable(int val)
        {
            NavigableIndex += val;
            CurrentNavigable = NavigableList[NavigableIndex];
        }
    }
}
