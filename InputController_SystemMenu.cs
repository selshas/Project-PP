using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
   
public class InputController_SystemMenu : MonoBehaviour
{
    public void ResumeGameplay(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GlobalTimer.Play();

            GameObject.Find("UICanvas").transform.Find("SystemMenu").gameObject.SetActive(false);
            
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }
    }
}
