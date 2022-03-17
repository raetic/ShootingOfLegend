using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisappear : MonoBehaviour
{
    public float time;
    void Start()
    {
        Invoke("d", time);
    }
    void d()
    {
        Destroy(gameObject);
    }
}
