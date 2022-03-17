using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReonaR : MonoBehaviour
{
    [SerializeField] GameObject realR;
    SpriteRenderer spr;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    float t;
    void Update()
    {
        t += Time.deltaTime*1.3f;
        if (t < 1)
        {
            transform.localScale = new Vector3(t, t, 1);
        }
        if (t >= 1)
        {
            realR.SetActive(true);
            spr.color = new Color(1, 1, 1, 0);
            realR.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1+1/1.3f - t/1.3f);
        }
    
    }
}
