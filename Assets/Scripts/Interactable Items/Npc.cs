using UnityEngine;

public class Npc : Interactibale
{

    #region Variables

    [Header("Dialog")]
    [SerializeField]
    private DialogWindow _dialogWindow;
    [SerializeField]
    private Sprite _profilIcon;
    [SerializeField]
    private string _npcName;
    [SerializeField]
    private string[] _npcDialog;

    #endregion

    #region Unity Func
    private void Update()
    {
        ShowInteractionButton();
        CheckPlayerInteraction();
    }

    #endregion

    #region Built Func

    protected override void InteractWithPlayer()
    {
        GameManager.Instance.PlayerIsInteracting = true;
        ActivateDialogWindow();
    }

    private void ActivateDialogWindow()
    {
        _dialogWindow.ProfilName.text = _npcName;
        _dialogWindow.ProfilImage.sprite = _profilIcon;
        _dialogWindow.PagesText = _npcDialog;
        GameManager.Instance.PlayerCannotTurn = true;
        _dialogWindow.gameObject.SetActive(true);
    }

    #endregion

}
