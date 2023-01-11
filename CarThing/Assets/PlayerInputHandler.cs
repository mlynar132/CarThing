using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInputThing PlayerInputThing;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Forward(InputAction.CallbackContext context)
    {

    }


    private void EnableInput()
    {
        PlayerInputThing.Player.Forward.Enable();
        PlayerInputThing.Player.Forward.performed += Forward;
        PlayerInputSystem.Player.Move.Enable();
        PlayerInputSystem.Player.Move.performed += Move;
        PlayerInputSystem.Player.Shoot.Enable();
        PlayerInputSystem.Player.Shoot.performed += Shoot;
    }
    private void DisableInput()
    {
        PlayerInputSystem.Player.Move.Disable();
        PlayerInputSystem.Player.Move.performed -= Move;
        PlayerInputSystem.Player.Shoot.Disable();
        PlayerInputSystem.Player.Shoot.performed -= Shoot;
    }
}
