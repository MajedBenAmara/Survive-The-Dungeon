using TMPro;
using UnityEngine;

public class DungeonDoor : Interactibale
{
    [SerializeField]
    private RectTransform _notification;

    [SerializeField]
    private TextMeshProUGUI _notificationText;
    private Animator _notAnimator;

    private bool _doItOnce = false;

    protected void Start()
    {
        _notAnimator = _notification.GetComponent<Animator>();
        _notAnimator.Play("Door_Notification_Idle");

    }

    private void Update()
    {
        ShowInteractionButton();
        CheckPlayerInteraction();
    }

    protected override void CheckPlayerInteraction()
    {
        if (GameManager.Instance.PlayerGameObject != null)
        {
            if (CalculateDistanceFromPlayer() <= _interactionRange)
            {
                _notificationText.text = "Not Enough Keys " + GameManager.Instance.PlayerStats.NumberOfKeys + "/3";
                if (GameManager.Instance.InteractionButtonPressed)
                {
                    if(GameManager.Instance.PlayerStats.NumberOfKeys == 3)
                    {
                        if (!_doItOnce)
                        {
                            GameManager.Instance.ClearingIU.ActivatePostClearUI(true);
                            GameManager.Instance.PlayerCannotTurn = true;
                            _doItOnce = true;
                        }
                    }
                    _notAnimator.Play("Door_Notification_Appear");
                }
            }
            else
            {
                if (_notAnimator.GetCurrentAnimatorStateInfo(0).IsName("Door_Notification_Appear") &&
                        _notAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9)
                {
                    _notAnimator.Play("Door_Notification_Desappear");
                }
            }


        }


    }
}