using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    public static float GetLeft(this RectTransform rt)
    {
        // rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        return rt.offsetMin.x;
    }

    public static float GetRight(this RectTransform rt)
    {
        // rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        return rt.offsetMax.x * -1;
    }

    public static float GetTop(this RectTransform rt)
    {
        // rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        return rt.offsetMax.y * -1;
    }

    public static float GetBottom(this RectTransform rt)
    {
        // rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        return rt.offsetMin.y;
    }
}