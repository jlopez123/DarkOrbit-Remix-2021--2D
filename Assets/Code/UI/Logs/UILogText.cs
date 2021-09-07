using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILogText : PooledMonoBehaviour
{
    [SerializeField]
    private Text _logText;

    private float _lifeTime;

    public void Configure(LogTextData logData)
    {
        _logText.text = logData.Message;
        _logText.color = logData.MessageColor;
        _lifeTime = logData.Duration;
        StartCoroutine(HideAfterDelay());
    }

    // usar DOTween
    private IEnumerator HideAfterDelay()
    {
        _logText.ChangeAlpha(0f);
        //show
        yield return StartCoroutine(_logText.FadeCoroutine(0f, 255f, 0.2f));

        yield return new WaitForSeconds(_lifeTime);

        _logText.ChangeAlpha(0f);

        ReturnToPool(0f);
    }
}