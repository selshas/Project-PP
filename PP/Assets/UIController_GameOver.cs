using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController_GameOver : MonoBehaviour
{
    public GameObject screen;
    private void OnEnable()
    {
        StartCoroutine(PausingDelay());
    }

    IEnumerator PausingDelay()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        GlobalTimer.Pause();
        screen.SetActive(true);
    }

    private void Update()
    {
    }
}
