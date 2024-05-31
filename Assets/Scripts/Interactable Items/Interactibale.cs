using UnityEngine;

public class Interactibale : MonoBehaviour
{

    #region Variables
    //private bool _showInteractionRange = true;

    [Header("Interaction Elements")]
    [SerializeField]
    protected GameObject _interactionButton;
    [SerializeField]
    protected float _interactionRange = 2f;

    protected bool _HideInteractionButton = false;
    protected bool _canInteract = true;

    [SerializeField]
    private bool _drawGizmo = false;

    #endregion

    #region Built Func


    protected void ShowInteractionButton()
    {
        if (GameManager.Instance.PlayerTransform.gameObject != null || _canInteract)
        {
            if (CalculateDistanceFromPlayer() <= _interactionRange)
            {

                if (!GameManager.Instance.PlayerIsInteracting && !_HideInteractionButton)
                {
                    _interactionButton.SetActive(true);
                }
                else
                {
                    _interactionButton.SetActive(false);
                }
            }
            else
            {
                _interactionButton.SetActive(false);
                GameManager.Instance.PlayerIsInteracting = false;
            }
        }

    }

    protected float CalculateDistanceFromPlayer()
    {
        return Vector2.Distance(GameManager.Instance.PlayerTransform.position, transform.position);
    }

    // Check if the player interact with player
    protected virtual void CheckPlayerInteraction()
    {
        if(GameManager.Instance.PlayerGameObject != null)
        {
            if (CalculateDistanceFromPlayer() <= _interactionRange)
            {
                if (GameManager.Instance.InteractionButtonPressed && _canInteract)
                {
                    InteractWithPlayer();
                }
            }
        }

        
    }

    // Describe what happen when the player interact with this object
    protected virtual void InteractWithPlayer()
    {

    }

    // Gizmo
    private void OnDrawGizmos()
    {

        if (_drawGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _interactionRange);
        }

    }
    #endregion

}