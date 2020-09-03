using System.Collections.Generic;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Phone
{
    public class PhoneNavigator : MonoBehaviour, IScrollHandler, IPhoneNavigator
    {
        [SerializeField] private float timeBetweenScrolls;
        private float scrollTimer;
        
        private List<IPhoneNavigable> _currentList;
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
            set => _navigableIndex = value.Mod(5);
        }

        private void Update()
        {
            if (scrollTimer > timeBetweenScrolls) return;
            scrollTimer += Time.deltaTime;
        }
        
        public void Scroll(float val)
        {
            val = (int)Mathf.Sign(val);
            if (scrollTimer < timeBetweenScrolls || val == 0) return;
            SetCurrentNavigable((int)val);
            scrollTimer = 0;
        }

        private void SetCurrentNavigable(int val)
        {
            NavigableIndex += val;
            Debug.Log("Index "+ NavigableIndex);
            //GetCurrentNavigable = NavigableList[NavigableIndex];
        }
    }
}
