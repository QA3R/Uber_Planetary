using TMPro;
using UberPlanetary.Core.ExtensionMethods;
using UberPlanetary.Core.Interfaces;
using UberPlanetary.Player.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Navigation
{
    /// Updates the Navigation Icon based on its position relative to the camera in the scene.
    /// Also Exposes properties of the icon to be modified at runtime
    public class NavigationIcon : MonoBehaviour , ILandmarkIcon
    {
        //private members
        private Camera _camera;
        private PlayerController _player;
        private ILandmark target;
        private Vector2 _xMinMax, _yMinMax;
        private float _distanceToPlayer;

        //exposed fields
        [SerializeField] private Vector3 offset;
        [SerializeField] private TextMeshProUGUI distanceValueHolder;
        [SerializeField] private Vector2 heightMinMax;
        [SerializeField] private Vector2 distanceMinMax;
        [SerializeField] private float iconScreenPadding;
        [SerializeField] private Canvas parentCanvas;

        //public properties
        public Image iconImage { get; set; }
        public Color iconColor
        {
            get => iconImage.color;
            set => iconImage.color = value;
        }

        private string distanceValueText
        {
            get => distanceValueHolder.text;
            set => distanceValueHolder.text = value;
        }

        private float canvasScaleFactor => parentCanvas.scaleFactor;

        private bool IsActive => iconImage.enabled;

        public float IconScreenPadding
        {
            get => iconScreenPadding * canvasScaleFactor;
            set => iconScreenPadding = value;
        }

        private void Awake()
        {
            iconImage = GetComponent<Image>(); 
            target = GetComponentInParent<ILandmark>();
            _xMinMax.x = iconImage.GetPixelAdjustedRect().width / 2f;
            _xMinMax.y = Screen.width - _xMinMax.x;
            
            _yMinMax.x = iconImage.GetPixelAdjustedRect().height / 2f;
            _yMinMax.y = Screen.height - _yMinMax.x;
        }

        private void Start()
        {
            _camera = Camera.main;

            _player = FindObjectOfType<PlayerController>();
        }
        
        private void FixedUpdate()
        {
            if(!IsActive) return;
            _distanceToPlayer = Vector3.Distance(target.GetTransform.position, _player.transform.position);
            UpdateIconPosition();
            UpdateDistance();
        }
        
        private void UpdateDistance()
        {
            distanceValueText = "("+ (int)_distanceToPlayer + "m)";
        }

        public void ToggleImage()
        {
            iconImage.enabled = !iconImage.isActiveAndEnabled;
            distanceValueHolder.gameObject.SetActive(!distanceValueHolder.gameObject.activeSelf);
        }

        //Map the icon's position on the canvas based on the camera and world offset.
        public void UpdateIconPosition()
        { 
            Vector3 yOffset = new Vector3(0,_distanceToPlayer.Remap(distanceMinMax.x, distanceMinMax.y, heightMinMax.x, heightMinMax.y));
            Vector2 pos = _camera.WorldToScreenPoint(target.GetTransform.position + offset + yOffset);
            
            pos.x = Mathf.Clamp(pos.x, _xMinMax.x + IconScreenPadding, _xMinMax.y - IconScreenPadding);
            pos.y = Mathf.Clamp(pos.y, _yMinMax.x + IconScreenPadding, _yMinMax.y - IconScreenPadding);
            float forwardDotLandmark = Vector3.Dot((target.GetTransform.position - _player.transform.position),
                _player.transform.forward);
            if(forwardDotLandmark < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = _xMinMax.y - IconScreenPadding;
                }
                else
                {
                    pos.x = _xMinMax.x + IconScreenPadding;
                }
            }
            iconImage.transform.position = pos;
        }
    }
}