using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController_Gameplay : MonoBehaviour
{
    public static InputController_Gameplay instance { get; private set; }

    PlayerInput playerInput = null;
    public Pawn controllingPawn = null;

    private void Awake()
    {
        Application.targetFrameRate = 144;
        instance ??= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        if (context.performed)
        {
            controllingPawn.inputVector_moving.x = inputVector.x;
            controllingPawn.inputVector_moving.z = inputVector.y;
        }
        else if (context.canceled)
        {
            controllingPawn.inputVector_moving.x = 0;
            controllingPawn.inputVector_moving.z = 0;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controllingPawn.animator.SetTrigger("Jump");
            controllingPawn.GetComponent<Rigidbody>().AddForce(
                new Vector3(0, controllingPawn.jumpForce, 0),
                ForceMode.Impulse
            );
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controllingPawn.animator.SetBool("ContinuousFire", true);
        }
        if (context.canceled)
        {
            controllingPawn.animator.SetBool("ContinuousFire", false);
        }
    }
    public void UseAbility0(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_Character)controllingPawn).UseAbility(0);
        }
    }
    public void UseAbility1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_Character)controllingPawn).UseAbility(1);
        }
    }
    public void UseAbility2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_Character)controllingPawn).UseAbility(2);
        }
    }
    public void UseAbility3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_Character)controllingPawn).UseAbility(3);
        }
    }
    public void Dodge(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_PlayerCharacter)controllingPawn).UseAbility(4);
        }
    }
    public void LevelUpAbility0(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_PlayerCharacter)controllingPawn).InjectSkillPoint(0);
        }
    }
    public void LevelUpAbility1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_PlayerCharacter)controllingPawn).InjectSkillPoint(1);
        }
    }
    public void LevelUpAbility2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_PlayerCharacter)controllingPawn).InjectSkillPoint(2);
        }
    }
    public void LevelUpAbility3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ((PP.Game.Pawn_PlayerCharacter)controllingPawn).InjectSkillPoint(3);
        }
    }
    public void ShowUpSystemMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GlobalTimer.Pause();
            GameObject.Find("UICanvas").transform.Find("SystemMenu").gameObject.SetActive(true);

            GetComponent<PlayerInput>().SwitchCurrentActionMap("SystemMenu");
        }
    }
}
