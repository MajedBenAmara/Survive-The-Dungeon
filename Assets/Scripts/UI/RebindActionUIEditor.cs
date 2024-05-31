using UnityEngine;
using UnityEngine.InputSystem;

public class ResetDeviceBinding : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _inputActions;
    [SerializeField]
    private string _targetControlScheme;


    // Reset the binding for all controls(Keyboard and gamepad)
    public void ResetAllBinding()
    {
        foreach (InputActionMap map in _inputActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
    }

    // Reset the binding for a specific controler
    public void ResetControlSchemeBinding()
    {
        foreach (InputActionMap map in _inputActions.actionMaps)
        {
            foreach (InputAction action in map.actions)
            {
                action.RemoveBindingOverride(InputBinding.MaskByGroup(_targetControlScheme));
            }
        }
    }
}
