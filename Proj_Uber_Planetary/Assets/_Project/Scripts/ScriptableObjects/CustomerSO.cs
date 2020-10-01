using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UberPlanetary.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Customer", menuName = "ScriptableObjects/Create Customer", order = 1)]
    public class CustomerSO : ScriptableObject
    {


        [Space(10)]
        [Header("Private fields")]
        [SerializeField]
        private string customerName;
        [SerializeField]
        private Sprite customerFace;
        [SerializeField]
        private float customerMood;

        
        public string CustomerName => customerName;
 
        public Sprite CustomerFace => customerFace;

        public float CustomerMood => customerMood;
       
    }
}

