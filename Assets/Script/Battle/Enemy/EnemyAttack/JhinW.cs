using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhinW : MonoBehaviour
{
    [SerializeField] GameObject w;
    [SerializeField] GameObject w1;
    float t;
    bool GetA;
    void Update()
    {
        t += Time.deltaTime;
        if (t < 1)
        {
            w.transform.localScale = new Vector3(0.5f - t * 0.5f, 3, 0);
            w1.transform.localScale = new Vector3(0.5f - t * 0.5f, 3, 0);
        }
        if (t > 1&&!GetA)
        {
            GetA = true;
            getA();
        }
        if (t > 1.3f)
        {
            Destroy(gameObject);
        }
    }
    void getA()
    {
        
        gameObject.AddComponent<EnemyArrow>();
        gameObject.GetComponent<EnemyArrow>().dmg = 10;
        gameObject.GetComponent<EnemyArrow>().isStun = true;
        gameObject.GetComponent<EnemyArrow>().type = 1;
        gameObject.GetComponent<EnemyArrow>().StunTime = 2;
        w1.transform.localScale = new Vector3(0.5f , 3, 0);
        w1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        gameObject.tag = "EnemyArrow";
      
    }
}
