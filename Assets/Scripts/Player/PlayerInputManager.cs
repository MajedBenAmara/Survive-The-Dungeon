using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public event Action OnInteraction;
    public void ReadDirection(InputAction.CallbackContext context)
    {
        GameManager.Instance.PlayerControler.Direction = context.ReadValue<Vector2>();
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.InteractionButtonPressed = true;
            OnInteraction?.Invoke();
        }
        if (context.canceled)
        {
            GameManager.Instance.InteractionButtonPressed = false;
        }
    }

    public void PressSkipButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.SkipButtonPresses = true;
        }
        if (context.canceled)
        {
            GameManager.Instance.SkipButtonPresses = false;
        }
    }

    public void ConsumeHealthPotion(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(GameManager.Instance.PlayerStats.NumberOfPotion > 0 && 
                GameManager.Instance.PlayerStats.PlayerCurrentHealth < GameManager.Instance.PlayerStats.PlayerMaxHealth)
            {
                SfxManager.Instance.PlayConsumePotionSfx();
                // deacrease the number of potions
                GameManager.Instance.PlayerStats.NumberOfPotion--;
                // update ui
                GameManager.Instance.UIPotion.UpdateUI(GameManager.Instance.PlayerStats.NumberOfPotion);
                // add health to player
                GameManager.Instance.PlayerStats.HealPlayer();
            }

        }
    }
}
