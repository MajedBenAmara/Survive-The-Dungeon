using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionButton : MonoBehaviour
{
    private PlayerInput _playerInputAction;

    [SerializeField]
    private TextMeshProUGUI _buttonText;

    private void OnEnable()
    {
        _playerInputAction = GameManager.Instance.PlayerInputManager.GetComponent<PlayerInput>();
        _buttonText.text = _playerInputAction.currentActionMap.FindAction("Interaction").GetBindingDisplayString();
    }
}
