using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xfeather : MonoBehaviour
{
    bool isFiled;
    GameObject Player;
    bool isCome;
    private void Start()
    {
        GetComponent<PlayerArrow>().notDestroy = true;

    }
    public void come()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        isCome = true;
        GetComponent<PlayerArrow>().dmg = Player.GetComponent<Player>().Dmg * 0.8f;
        gameObject.tag = "PlayerAttack";
    }
    private void Update()
    {
        if (transform.position.y > 4.5f&&!isFiled)
        {
            field();
        }
        if (isCome)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * 20);
        }
    }
    void field()
    {
        isFiled = true;
        GetComponent<PlayerArrow>().dmg = 0;
        GetComponent<PlayerArrow>().isSkill = true;
        gameObject.tag = "feather";
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCome)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().XayahFcount++;
                collision.gameObject.GetComponent<Enemy>().XayahFtime=0.5f;
            }
            if (collision.gameObject == Player)
            {
                Destroy(gameObject);
            }
        }
    }
}
