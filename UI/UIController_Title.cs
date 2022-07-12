using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class UIController_Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
