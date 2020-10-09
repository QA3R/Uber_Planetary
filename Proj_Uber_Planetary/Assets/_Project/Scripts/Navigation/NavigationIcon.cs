using System;
using UberPlanetary.Core;
using UberPlanetary.Player.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Navigation
{
    public class NavigationIcon : MonoBehaviour , ILandmarkIcon
    {
        private Camera _camera;
        private PlayerController _player;
        private ILandmark target;
        [SerializeField] private Vector3 offset;

        private Vector2 xMinMax, yMinMax;
        public Image iconImage { get; set; }
        public Color iconColor
        {
            get => iconImage.color;
            set => iconImage.color = value;
        }

        private void Awake()
        {
            iconImage = GetComponent<Image>(); 
            target = GetComponentInParent<ILandmark>();
            xMinMax.x = iconImage.GetPixelAdjustedRect().width / 2f;
            xMinMax.y = Screen.width - xMinMax.x;
            
            yMinMax.x = iconImage.GetPixelAdjustedRect().height / 2f;
            yMinMax.y = Screen.height - yMinMax.x;
            
        }

        private void Start()
        {
            _camera = Camera.main;

            _player = FindObjectOfType<PlayerController>();
        }

        private void FixedUpdate()
        {
            UpdateIconPosition();
        }

        public void UpdateIconPosition()
        { 
            Vector2 pos = _camera.WorldToScreenPoint(target.GetTransform.position + offset);
            
            pos.x = Mathf.Clamp(pos.x, xMinMax.x, xMinMax.y);
            pos.y = Mathf.Clamp(pos.y, yMinMax.x, yMinMax.y);
            
            if(Vector3.Dot((target.GetTransform.position - _player.transform.position), _player.transform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = xMinMax.y;
                }
                else
                {
                    pos.x = xMinMax.x;
                }
            }
            iconImage.transform.position = pos;
        }
    }
}