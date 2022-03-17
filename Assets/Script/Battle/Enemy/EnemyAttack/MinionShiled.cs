using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionShiled : MonoBehaviour
{
    bool s;
    Rigidbody2D rigid;
    public void S()
    {
        s = true;
            GetComponent<EnemyArrow>().type = 1;
            Invoke("D", 4);
            rigid = GetComponent<Rigidbody2D>();
        
    }
    void D()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bottomborder2"&&s)
        {
            
            float x = rigid.velocity.x;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(x*-1, 300));
        }
    }
}
