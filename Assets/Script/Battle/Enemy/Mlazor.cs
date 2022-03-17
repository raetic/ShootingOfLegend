using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlazor : MonoBehaviour
{
    float t;
    float x;
    float y;
    [SerializeField] Enemy myEnemy;
    void Update()
    { 
        t += Time.deltaTime;
        if (t > 0.1f)
        {
            x = myEnemy.transform.position.x + (1.15f);// + Random.Range(-20, 19) * 0.005f);
            y = myEnemy.transform.position.y - 4.6f;//+Random.Range(-20, 19) * 0.005f);          
            
            t = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(x, y), Time.deltaTime * 0.1f);
    }
}
