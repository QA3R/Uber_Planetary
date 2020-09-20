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
        private bool _canScroll => _scrollTimer > timeBetweenScrolls;
        
        [SerializeField] private float timeBetweenScrolls;
        
        public IPhoneNavigable CurrentNavigable
        {
            get => _currentNavigable;
            
            //when the navigable is changed
            private set
            {
                //Call Deselect on previous navigable
                _currentNavigable?.Deselect();
                //change the value to new navigable
                _currentNavigable = value;
                //call select on new navigable
                _currentNavigable?.Select();
            }
        }

        public List<IPhoneNavigable> NavigableList
        {
            get => _currentList;
            
            //When the value of the list is changed
            set
            {
                //set list to the provided list
                _currentList = value;
                //Set the navigable to the 1st element of the new list
                CurrentNavigable = _currentList[0];
                //reset the index
                NavigableIndex = 0;
            } 
        }
        
        private int NavigableIndex
        {
            get => _navigableIndex;
            set => _navigableIndex = value.Mod(NavigableList.Count); // makes sure the value passed into the index is always within the range and loops
        }

        private void Update()
        {
            //if the player can scroll do nothing
            if (_canScroll) return;
            
            //if the player can not scroll increase the time till they can scroll
            _scrollTimer += Time.deltaTime;
        }
        
        // This is the function called when phone controller scrolls up or down
        public void Scroll(float val)
        {
            //if player cannot scroll and the scroll input is too small, less than 0.1f then do nothing.
            if (!_canScroll || Mathf.Abs(val) < 0.1f) return;
            
            //Checks the sign of the provided input value, so converting the input from whatever it is to -1 or 1 as an int
            _tempIncrement = (int)Mathf.Sign(val);
            SetCurrentNavigable(_tempIncrement);
            //reset timer on successful scroll
            _scrollTimer = 0;
        }

        private void SetCurrentNavigable(int val)
        {
            //move the index up or down based on provided value
            // Now go up and read the properties.
            NavigableIndex += val;
            CurrentNavigable = NavigableList[NavigableIndex];
        }
    }
}
