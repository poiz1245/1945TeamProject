using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Lazor : MonoBehaviour
{
    ParticleSystem ps;
    List<ParticleSystem.Particle> inseide = new List<ParticleSystem.Particle>();
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        Debug.Log("aa");
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inseide);

        foreach (var player in inseide)
        {
            Debug.Log("aa");
        }
    }
}
