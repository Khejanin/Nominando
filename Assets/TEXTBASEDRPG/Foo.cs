using System;
using UnityEngine;

public class Foo : MonoBehaviour
{
    public Bar m_bar;

    [Serializable]
    public struct Bar
    {
        public int a;
        public float b;
    }
}