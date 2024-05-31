using UnityEngine;

public class OptionMenuContainer : MonoBehaviour
{

    [SerializeField]
    protected GameObject _optionMenu;

    internal bool OptionMenuIsActive = false;

    private static OptionMenuContainer _instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        MenuBehaviour.OnHideOptionMenu += HideOptionMenu;
        MenuBehaviour.OnShowOptionMenu += ShowOptionMenu;
    }

    private void ShowOptionMenu()
    {
        _optionMenu.SetActive(true);
        OptionMenuIsActive = _optionMenu.activeInHierarchy;
    }
    public void HideOptionMenu()
    {
        _optionMenu.SetActive(false);
        OptionMenuIsActive = _optionMenu.activeInHierarchy;
    }
}
