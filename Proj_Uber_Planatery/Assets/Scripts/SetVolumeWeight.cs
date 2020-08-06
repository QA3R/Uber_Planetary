using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetVolumeWeight : MonoBehaviour
{
    private Volume _volume;

    private void Awake()
    {
        _volume = GetComponent<Volume>();
    }

    public void SetVolume(float amount)
    {
        _volume.weight = amount;
    }
}
