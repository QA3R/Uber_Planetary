using System;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Phone.Applications
{
    public class TestApplication : MonoBehaviour , IPhoneApplication
    {
        private Material _material;
        [SerializeField] private string outlineActivePropertyTag;
        [SerializeField] private string outlineColorPropertyTag;
        [SerializeField][ColorUsage(true, true)] private Color normalColor, highlightColor;
        private void Awake()
        {
            _material = GetComponent<Image>().material;
            _material.SetFloat(outlineActivePropertyTag, 0);
            _material.SetColor(outlineColorPropertyTag, normalColor);
        }

        public void Enter()
        {
            Debug.Log("Entering :" + gameObject.name);
        }

        public void Exit()
        {
            Debug.Log("Exiting :" + gameObject.name);
        }

        public void OnSelect()
        {
            //throw new System.NotImplementedException();
            Debug.Log("<color=blue>HIGHLIGHTED </color>" + gameObject.name);
            _material.SetFloat(outlineActivePropertyTag, 1);
            _material.SetColor(outlineColorPropertyTag, highlightColor);
        }

        public void OnDeselect()
        {
            //throw new System.NotImplementedException();
            Debug.Log("<color=red>UNHIGHLIGHTED </color>" + gameObject.name);
            _material.SetFloat(outlineActivePropertyTag, 0);
            _material.SetColor(outlineColorPropertyTag, normalColor);
        }


        public void DisplayNotification()
        {
            Debug.Log("Displaying Notification for :" + gameObject.name);
        }
    }
}
