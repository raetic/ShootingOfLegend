using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReonaW : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
          
            int t = 0;
            if (collision.gameObject.GetComponent<PlayerArrow>().notDestroy)
                t = 1;
            float dmg = collision.gameObject.GetComponent<PlayerArrow>().dmg*0.5f;
            collision.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            collision.gameObject.tag = "EnemyArrow";
            collision.gameObject.AddComponent<EnemyArrow>();
            collision.gameObject.GetComponent<EnemyArrow>().type = t;
            collision.gameObject.GetComponent<EnemyArrow>().dmg = dmg;
            float y = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            collision.gameObject.transform.localScale = new Vector2(1, -1);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -y);
        }
    }
}
