using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MInion : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    float t;
    Enemy myEnemy;
    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        myEnemy.isMove = false;
    }
    private void Update()
    {
        if (!myEnemy.isMove)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime *-0.7f);
            if (transform.position.y < 1)
            {
                myEnemy.isMove = true;
            }
        }
        if (myEnemy.isMove)
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                t = 0;
                GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
                bul.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 200);
            }
        }
    }
}
