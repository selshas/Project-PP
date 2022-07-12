using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventListener_TetherMine : MonoBehaviour
{
    [SerializeField]
    public UnityEvent deployed = null;
    [SerializeField]
    public UnityEvent triggered = null;
    [SerializeField]
    public UnityEvent terminated = null;
    
    public void Deployed() { deployed?.Invoke(); }
    public void Triggered() { triggered?.Invoke(); }
    public void Terminated() { terminated?.Invoke(); }
}
