public class Potion : Interactibale
{
    private void Update()
    {
        ShowInteractionButton();
        CheckPlayerInteraction();
    }

    protected override void InteractWithPlayer()
    {
        SfxManager.Instance.PlayGrabItemSfx();
        // Increase the number of potion the player has
        GameManager.Instance.PlayerStats.NumberOfPotion++;
        // update th UI to reflect that
        GameManager.Instance.UIPotion.UpdateUI(GameManager.Instance.PlayerStats.NumberOfPotion);
        // remove the potion from the scene
        gameObject.SetActive(false);

    }
}       
