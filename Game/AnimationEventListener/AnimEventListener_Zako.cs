using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventListener_Zako : MonoBehaviour
{
    [SerializeField]
    public UnityEvent gunfire = null;
    public void Gunfire() { gunfire?.Invoke(); }
}
