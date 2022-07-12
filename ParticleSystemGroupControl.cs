using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemGroupControl : MonoBehaviour
{
    List<ParticleSystem> list_particleSystems = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponentsInChildren<ParticleSystem>(true, list_particleSystems);
    }

    public void Play()
    {
        foreach (ParticleSystem ps in list_particleSystems)
        {
            ps.Play();
        }
    }
    public void Stop()
    {
        foreach (ParticleSystem ps in list_particleSystems)
        {
            ps.Stop();
        }
    }
}
