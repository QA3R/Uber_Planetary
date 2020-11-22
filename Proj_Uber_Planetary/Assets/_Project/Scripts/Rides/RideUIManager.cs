using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UberPlanetary.ScriptableObjects;
using TMPro;

namespace UberPlanetary.Rides
{
    public class RideUIManager : MonoBehaviour
    {
        /// <summary>
        /// This script is responsible for updating the Objective text in-game. 
        /// There is probably another way to merge the DisplayPickUpObj and DisplayDropOffObj 
        /// but this will have to do for now.
        /// </summary>

        #region Variables
        [SerializeField] private TextMeshProUGUI objectiveText;
        private RideManager _rideManager;
        #endregion

        #region Awake, Start, OnEnable, OnDisable Methods
        // Gets reference to RideManager and add listeners to for ability to update objective text
        private void Start()
        {
            _rideManager = GameObject.FindObjectOfType<RideManager>();
            _rideManager.onRideAccepted.AddListener(DisplayPickUpObj);
            _rideManager.onCustomerPickedUp.AddListener(DisplayDropOffObj);
            _rideManager.onCustomerDroppedOff.AddListener(ClearText);
        }

        // Remove listeners from RideManager
        private void OnDisable()
        {
            _rideManager.onRideAccepted.RemoveListener(DisplayPickUpObj);
            _rideManager.onCustomerPickedUp.RemoveListener(DisplayDropOffObj);
            _rideManager.onCustomerDroppedOff.RemoveListener(ClearText);
        }
        #endregion

        #region Methods
        // Will update the Objective text to show where to pick up client
        void DisplayPickUpObj(CustomerSO so)
        {
            if (so.PickupObjectiveTxt != null)
                objectiveText.text = so.PickupObjectiveTxt;
        }

        // Will update the Objective text to show where to drop off client
        void DisplayDropOffObj(CustomerSO so)
        {
            if (so.DropoffObjectiveTxt != null)
                objectiveText.text = so.DropoffObjectiveTxt;
        }

        // Will update the Objective text to clear the text
        void ClearText(CustomerSO so)
        {
            objectiveText.text = "";
        }
    }
    #endregion
}
