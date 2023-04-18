using UnityEngine;

public static class TransformExtension
{
    public static void SetZeroPositionAndRotation(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}