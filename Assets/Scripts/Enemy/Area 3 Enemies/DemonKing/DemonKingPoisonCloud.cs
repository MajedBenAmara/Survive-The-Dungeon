using System.Collections;
using UnityEngine;

public class DemonKingPoisonCloud : RangIndicator
{

    [SerializeField]
    private PoisonCloud _poisonCloud;
    // Update is called once per frame

    private void Start()
    {
        _outerRange.transform.localScale = new Vector2(2 * MaxRange, 2 * MaxRange);
        StartCoroutine(IndicateRange());
    }

    public override IEnumerator IndicateRange()
    {
        float t = 0f;
        float rate = (1 / _scalingDuration) * _scalingSpeed;

        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            _innerRange.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, t);
            yield return null;
        }
    }

    void Update()
    {
        if(_innerRange.localScale.x == 1 )
        {
            Instantiate(_poisonCloud, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
