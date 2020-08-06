using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParticleCount : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    [SerializeField] private float maxParticles;
    [SerializeField] private float minStartSpeed;
    [SerializeField] private float maxStartSpeed;
    private float _currentParticles;
    private float _currentStartSpeed;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

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
