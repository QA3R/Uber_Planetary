using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Core.Interfaces
{
    public interface ILandmark
    {
        ILandmarkIcon LocationIcon { get; set; }
        void OnLocationReached();
        event Action OnReached;
        Transform GetTransform { get; }
        
        string LandmarkStringID { get;}
        int LandmarkIntID { get;}
        IGeneralLandmark parentLandmark { get;}

        void ActivateLandmark();
        UnityEvent ActivationEvent { get;}
    }

    public interface IGeneralLandmark : ILandmark
    {
        List<ILandmark> landmarkGrouping { get; set; }
    }

    public interface ILandmarkIcon
    {
        Image iconImage { get; set; }
        Color iconColor { get; set; }

        void ToggleImage();

        void UpdateIconPosition();
    }
}