using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParticleCount : MonoBehaviour
{
    //Private members
    private ParticleSystem _particleSystem;
    private float _currentParticles;
    private float _currentStartSpeed;
    
    //Exposed fields in inspector
    [SerializeField] private float maxParticles;
    [SerializeField] private float minStartSpeed;
    [SerializeField] private float maxStartSpeed;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Expects a value between 0-1 and remaps it to valid range for Particle system properties
    /// </summary>
    /// <param name="val"></param>
    public void SetCount(float val)
    {
        _currentParticles = val.Remap(0, 1, 0, maxParticles);
        _currentStartSpeed = val.Remap(0, 1, minStartSpeed, maxStartSpeed);
        
        var particleSystemEmission = _particleSystem.emission;
        particleSystemEmission.rateOverTime = _currentParticles;
        var particleSystemMain = _particleSystem.main;
        particleSystemMain.startSpeed = _currentStartSpeed;
    }
}
