using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class GraphicExtensions
{
    public static T ChangeAlpha<T>(this T graphic, float newAlpha) where T : Graphic
    {
        var color = graphic.color;
        color.a = newAlpha / 255f;
        graphic.color = color;
        return graphic;
    }

    // usar DOTween ...
    public static IEnumerator FadeCoroutine<T>(this T graphic, float start, float end, float duration) where T : Graphic
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            graphic.ChangeAlpha(Mathf.Lerp(0f, 255f, timer / duration));
            yield return null;
        }
    }

}
