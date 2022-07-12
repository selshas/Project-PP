using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventListener_PC : MonoBehaviour
{
    [SerializeField]
    public UnityEvent gunfire = null;
    [SerializeField]
    public UnityEvent spawnGunShield = null;
    [SerializeField]
    public UnityEvent despawnGunShield = null;
    [SerializeField]
    public UnityEvent spawnTetherMine = null;
    [SerializeField]
    public UnityEvent spawnMedDrone = null;
    [SerializeField]
    public UnityEvent releaseMedDrone = null;
    [SerializeField]
    public UnityEvent callBombardment = null;

    public void Gunfire() { gunfire?.Invoke(); }
    public void PlaceGunShield() { spawnGunShield?.Invoke(); }
    public void DespawnGunShield() { despawnGunShield?.Invoke(); }
    public void SpawnTetherMine(AnimationEvent e) 
    {
        if (e.intParameter == 1)
            spawnTetherMine?.Invoke();
    }
    public void SpawnMedDrone(AnimationEvent e)
    {
        if (e.intParameter == 1)
            spawnMedDrone?.Invoke();
    }

    public void ReleaseMedDrone(AnimationEvent e)
    {
        if (e.intParameter == 1) 
            releaseMedDrone?.Invoke(); 
    }
    public void CallBombardment(AnimationEvent e)
    {
        if (e.intParameter == 1)
            callBombardment?.Invoke(); 
    }
}
