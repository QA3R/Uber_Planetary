using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Phone.Applications
{
    public class ToggleOutline : MonoBehaviour
    {
        private GameObject _go;
        private Image _image;
        private Material _material;
        [SerializeField] private string outlineActivePropertyTag;
        
        private void OnEnable()
        {
            _go = this.gameObject;
            _image = _go.GetComponent<Image>();
            _material = Instantiate(_image.material);
            this._image.material = _material;
            SetOutlineTo(0);
        }

        // Sets the outline to true or false basically
        public void SetOutlineTo(float val)
        {
            _material?.SetFloat(outlineActivePropertyTag, val);
        }
    }
}