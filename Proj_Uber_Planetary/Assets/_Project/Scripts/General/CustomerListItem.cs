using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UberPlanetary.Phone.Applications;
using UberPlanetary.ScriptableObjects;

namespace UberPlanetary.Phone.Application_Feature
{
    public class CustomerListItem : BaseApplicationFeature
    {
        public Image customerFace;
        public TextMeshProUGUI customerName;
        public CustomerSO customerSO;
        public NavigableListProvider navListProvider;
        public UberApplication uberApp;

        private void Awake()
        {
            uberApp = FindObjectOfType<UberApplication>();
            OnEnter.AddListener(ButtonClicked);
        }
        public void Init(CustomerSO customerSO)
        {

        }
        public void ButtonClicked()
        {
            uberApp.TryAcceptNewCustomer(customerSO);
        }
    }
}

