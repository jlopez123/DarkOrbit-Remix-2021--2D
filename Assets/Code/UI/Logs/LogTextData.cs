using UnityEngine;

public struct LogTextData
{
    public string Message;
    public Color MessageColor;
    public float Duration;
    public LogTextData(string message, Color color, float duration)
    {
        Message = message;
        MessageColor = color;
        Duration = duration;
    }
}
