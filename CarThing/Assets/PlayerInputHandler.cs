using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInputThing PlayerInputThing;
    public PlayerScript PlayerScript;

    private void Awake()
    {
        PlayerInputThing = new PlayerInputThing();
    }
    private void OnEnable()
    {
        EnableInput();
    }
    private void OnDisable()
    {
        DisableInput();
    }

    private void Forward(InputAction.CallbackContext context)
    {
        PlayerScript.SetaccelerationInput(context.ReadValue<float>());
    }
    private void StopForward(InputAction.CallbackContext context)
    {
        PlayerScript.SetaccelerationInput(0f);
    }
    private void Steering(InputAction.CallbackContext context)
    {
        PlayerScript.SetSteeringInput(context.ReadValue<float>());
    }
    private void StopSteering(InputAction.CallbackContext context)
    {
        PlayerScript.SetSteeringInput(0f);
    }
    private void Break(InputAction.CallbackContext context)
    {

    }
    private void HandBreak(InputAction.CallbackContext context)
    {

    }

    private void EnableInput()
    {
        PlayerInputThing.Player.Forward.Enable();
        PlayerInputThing.Player.Forward.performed += Forward;
        PlayerInputThing.Player.Forward.canceled += StopForward;
        PlayerInputThing.Player.Steering.Enable();
        PlayerInputThing.Player.Steering.performed += Steering;
        PlayerInputThing.Player.Steering.canceled += StopSteering;
        PlayerInputThing.Player.Break.Enable();
        PlayerInputThing.Player.Break.performed += Break;
        PlayerInputThing.Player.HandBreak.Enable();
        PlayerInputThing.Player.HandBreak.performed += HandBreak;
    }
    private void DisableInput()
    {
        PlayerInputThing.Player.Forward.Disable();
        PlayerInputThing.Player.Forward.performed -= Forward;
        PlayerInputThing.Player.Forward.canceled -= StopForward;
        PlayerInputThing.Player.Steering.Disable();
        PlayerInputThing.Player.Steering.performed -= Steering;
        PlayerInputThing.Player.Steering.canceled -= StopSteering;
        PlayerInputThing.Player.Break.Disable();
        PlayerInputThing.Player.Break.performed -= Break;
        PlayerInputThing.Player.HandBreak.Disable();
        PlayerInputThing.Player.HandBreak.performed -= HandBreak;
    }
}
