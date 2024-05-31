using UnityEngine;

public class RerollChest : Interactibale
{
    [SerializeField]
    private Sprite _chestOpenSprite;
    [SerializeField]
    private Sprite _chestEmptySprite;
    [SerializeField]
    private GameObject _cointNotification;
    private bool _canAddpoints = true;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        ShowInteractionButton();
        if (_canAddpoints)
            CheckPlayerInteraction();
        if(_spriteRenderer.sprite.name == "chestOpenSprite" && CalculateDistanceFromPlayer() > _interactionRange)
        {
            _spriteRenderer.sprite = _chestEmptySprite; 
        }
    }
    protected override void InteractWithPlayer()
    {
        GameManager.Instance.PlayerStats.RerollPoints++;
        Instantiate(_cointNotification,
            new Vector2(GameManager.Instance.PlayerTransform.position.x, 
            GameManager.Instance.PlayerTransform.position.y + 1f), Quaternion.identity);
        SfxManager.Instance.PlayCoinSfx();
        _canAddpoints = false;
        _spriteRenderer.sprite = _chestOpenSprite;
    }
}
