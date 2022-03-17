using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{

    public float dmg;
    public int type; //type=>1 안지워짐
    public int NoD;
    public bool isStun;
    public float StunTime;
    private void Awake()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArrowBorder"&&type==0)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {if(dmg!=0)
            collision.gameObject.GetComponent<Player>().onHit(Mathf.FloorToInt(dmg));
            if (isStun)
            {
              
                collision.gameObject.GetComponent<Player>().OnStun(StunTime);
            }
            if(type==0)
            Destroy(gameObject);
        }
    }
}
