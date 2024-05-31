using System;

public class MenuBehaviour : ButtonsBehaviour
{
    public static Action OnShowOptionMenu;
    public static Action OnHideOptionMenu;


    public void ShowOptionMenu()
    {
        OnShowOptionMenu?.Invoke();
    }
    public void HideOptionMenu()
    {
        OnHideOptionMenu?.Invoke();
    }
}
