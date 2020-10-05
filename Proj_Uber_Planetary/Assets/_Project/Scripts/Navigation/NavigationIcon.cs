using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Navigation
{
    public class NavigationIcon : MonoBehaviour , ILandmarkIcon
    {
        public Image iconImage { get; set; }
        public Color iconColor { get; set; }
        public void UpdatePosition()
        {
            //throw new System.NotImplementedException();
        }
    }
}