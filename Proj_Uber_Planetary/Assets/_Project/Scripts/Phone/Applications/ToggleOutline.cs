﻿using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Phone.Applications
{
    public class ToggleOutline : MonoBehaviour
    {
        private Material _material;
        [SerializeField] private string outlineActivePropertyTag;

        private void Awake()
        {
            _material = GetComponent<Image>().material;
            SetOutlineTo(0);
        }

        // Sets the outline to true or false basically
        public void SetOutlineTo(float val)
        {
            _material.SetFloat(outlineActivePropertyTag, val);
        }
    }
}