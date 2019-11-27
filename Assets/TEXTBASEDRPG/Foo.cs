using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foo : MonoBehaviour
{
    [System.Serializable]
    public struct Bar { public int a; public float b; }
    public Bar m_bar;

}
