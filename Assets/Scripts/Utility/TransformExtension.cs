using UnityEngine;

public static class TransformExtension
{
    public static void SetZeroPositionRotation(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    
    public static void SetParentAndZeroPositionRotation(this Transform transform, Transform parent)
    {
        transform.SetParent(parent);
        transform.SetZeroPositionRotation();
    }
}