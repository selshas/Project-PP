using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController_SystemMenu : MonoBehaviour
{
    int selected = 0;
    private void OnEnable()
    {
        Select(0);
    }

    public void Select(int n)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Animator anim = transform.GetChild(i).GetComponent<Animator>();
            if (i == n)
                anim.SetBool("Selected", true);
            else
                anim.SetBool("Selected", false);
        }
        selected = n;
        Debug.Log($"Select {n}'th Item.");
    }

    public void CursorMoveDown(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Select((selected + 1) % transform.childCount);
    }
    public void CursorMoveUp(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Select((selected - 1 + transform.childCount) % transform.childCount);
    }

    public void ExecuteSelected(InputAction.CallbackContext context)
    {
        if (context.canceled == false) return;

        switch (selected)
        {
            case 0:
                {
                    GlobalTimer.Play();
                    GameObject.Find("UICanvas").transform.Find("SystemMenu").gameObject.SetActive(false);

                    GameObject.Find("InputSystem").GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
                    break;
                }
            case 1:
                {
                    GlobalTimer.Play();
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
                    break;
                }
            case 2:
                {
                    Application.Quit();
                    break;
                }
        }
    }
}
