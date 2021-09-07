using UnityEngine;

public static class CanvasGroupExtensions
{
    public static void SetVisible(this CanvasGroup canvas, bool visible)
    {
        if (visible)
            ShowCanvasGroup(canvas);
        else
            HideCanvasGroup(canvas);
    }
    public static void HideCanvasGroup(this CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }
    public static void ShowCanvasGroup(this CanvasGroup canvas, bool interactable = true)
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = interactable;
        canvas.interactable = interactable;
    }

    public static void DisableInteraction(this CanvasGroup canvas)
    {
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
