using TMPro;
using UberPlanetary.Phone.Applications;
using UberPlanetary.ScriptableObjects;
using UnityEngine.UI;

namespace UberPlanetary.Phone.ApplicationFeature
{
    
    //NOTE: Script is in the wrong folder
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

