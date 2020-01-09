using System;
using UnityEngine;

public static class UIHelperClass
{
    public static void ShowPanel(GameObject gameObject, bool show)
    {
        gameObject.transform.localScale =
            new Vector3(Convert.ToSingle(show), Convert.ToSingle(show), Convert.ToSingle(show));
    }
}