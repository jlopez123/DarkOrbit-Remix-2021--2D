public static class FloatExtensions
{
    public static float GetRelativeAngle(this float angle)
    {
        return (angle > 180) ? angle - 360 : angle;
    }
}
