﻿using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary
{
    public class GetRotation : MonoBehaviour , IEventValueProvider<Quaternion>
    {
        public Quaternion GetValue => transform.rotation;
    }
}