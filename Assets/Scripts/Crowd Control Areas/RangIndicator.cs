using System.Collections;
using UnityEngine;

public class RangIndicator : MonoBehaviour
{

    #region Variables

    [Header("Range Indicator")]
    public float MaxRange;
    [SerializeField]
    protected float _scalingDuration;
    [SerializeField]
    protected float _scalingSpeed;
    [SerializeField]
    protected Transform _innerRange;
    [SerializeField]
    protected SpriteRenderer _outerRange;

    #endregion

    #region Built Func

    // increase the scale of the range indicator sprite unitil it hit it's max value
    public virtual IEnumerator IndicateRange()
    {
        float t = 0f;
        float rate = (1 / _scalingDuration) * _scalingSpeed;

        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            _innerRange.localScale = Vector2.Lerp(Vector2.zero, new Vector2(2 * MaxRange, 2 * MaxRange), t);
            yield return null;
        }
    }

    public void ActivateOuterRange()
    {
        transform.localScale = new Vector2(2 * MaxRange, 2 * MaxRange);
        _outerRange.enabled = true;
    }

    #endregion

}
