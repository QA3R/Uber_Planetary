using TMPro;
using UnityEngine;
using UberPlanetary.Phone.Applications;
using UberPlanetary.ScriptableObjects;
using UnityEngine.UI;

namespace UberPlanetary.Phone.ApplicationFeature
{
    
    //NOTE: Script is in the wrong folder
    public class CustomerListItem : BaseApplicationFeature
    {
        [SerializeField] Image customerFace;
        [SerializeField] TextMeshProUGUI customerName;


        private CustomerSO _customerSO;
        private NavigableListProvider _navListProvider;
        private UberApplication _uberApp;

        private void Awake()
        {
            //_uberApp = FindObjectOfType<UberApplication>();
            //_navListProvider = _uberApp.ListProvider;
            OnEnter.AddListener(ButtonClicked);
        }
        public void Init(CustomerSO customerData, UberApplication app)
        {
            _uberApp = app;
            _navListProvider = app.ListProvider;
            _customerSO = customerData;
            customerFace.sprite = customerData.CustomerFace;
            customerName.text = customerData.CustomerName;
            _navListProvider.AddToList(this);
        }
        public void ButtonClicked()
        {
            if (_uberApp.TryAcceptNewCustomer(_customerSO))
            {
                _navListProvider.RemoveFromList(this);
                OnEnter.RemoveListener(ButtonClicked);
                Destroy(gameObject);
            }
        }
    }
}

