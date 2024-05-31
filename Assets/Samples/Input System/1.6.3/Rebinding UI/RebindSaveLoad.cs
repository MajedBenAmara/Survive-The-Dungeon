using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
    public InputActionAsset actions;

    public void OnDisable()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }
}
