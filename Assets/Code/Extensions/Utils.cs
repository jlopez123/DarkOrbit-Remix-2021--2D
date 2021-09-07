using UnityEngine;

public static class Utils
{
    public static float AngleBetweenVectors(Vector2 target, Vector2 position)
    {
        Vector3 aux = (target - position).normalized;
        float angle = 180 - (Mathf.Atan2(aux.y, aux.x) * 180f / Mathf.PI);
        //Mathf.Rad2Deg

        if (angle < 0)
            angle += 360;
        return angle;
    }
}
