using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{

    #region Variables

    private Animator _anim;
    private float time;

    #endregion

    #region Unity Func
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.PlayerTookDamage)
        {
            _anim.Play("hit");
            SfxManager.Instance.PlayPlayerGettingHitSfx();
            bool animCond = _anim.GetCurrentAnimatorStateInfo(0).IsName("hit") &&
            _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f;
            if (animCond)
            {
                GameManager.Instance.PlayerTookDamage = false;
            }
        }
        else if (GameManager.Instance.PlayerControler.Direction != Vector2.zero && !GameManager.Instance.PlayerCannotTurn)
        {
            _anim.Play("run");
            if ((Time.time - SfxManager.Instance.PlayerSfxClipLength("Knight Walking") >= time))
            {
                SfxManager.Instance.PlayWalkingSfx();
                time = Time.time;
            }
        }
        else
        {
            _anim.Play("idle");
        }
    }

    #endregion

}
