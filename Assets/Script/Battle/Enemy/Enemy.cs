using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp;
    bool isDie;
    public int type; //type==1 ->근접형(y축 움직임 o) 2->원거리형(y축 움직임 x)
    public GameObject player;
    public int moveX;
    public int moveY;
    public float speed;
    public int MoveDmg;
    public bool isMove;
    public BattleManager BM;
    public float cctime;
  public bool NotBoss;
    public Vector3 goVector;
    public bool havetogo;
    float moveThinkTime;
    public string enemyName;
    public int XayahFcount;
    public float XayahFtime;
    public void onHit(int dmg)
    {
        if (dmg == 0) return;
        Hp -= dmg;
        if (Hp < 0) Die();
    }
    public void Die()
    {
        isDie = true;
        if (!NotBoss)
        {
            BM.nextStage();
        }
            Destroy(gameObject);
       
    }
    private void Start()
    {
        BM = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        player = GameObject.FindWithTag("Player");
        MoveThink();
        if(!NotBoss)
        Hp += BM.stage * 50;
    }
    private void Update()
    {
        if (cctime >= 0)
        {
            cctime -= Time.deltaTime;
        }
        else
        {
            if (isMove)
                transform.position = new Vector2(transform.position.x + moveX * Time.deltaTime * speed, transform.position.y + moveY * Time.deltaTime * speed);
            if (havetogo)
            {
                transform.position = Vector3.MoveTowards(transform.position, goVector, speed * Time.deltaTime);
            }
            if (moveThinkTime > 1)
            {
                moveThinkTime = 0;
                MoveThink();
            }
            moveThinkTime += Time.deltaTime;
        }
        if (XayahFcount > 0&&XayahFcount<3)
        {
            XayahFtime -= Time.deltaTime;
            if (XayahFtime < 0) XayahFcount = 0;
        }
        if (XayahFcount > 3)
        {
            XayahFtime = 0;
            XayahFcount = 0;
            cctime = 3;
        }
    }
    public void MoveThink()
    {
       
        moveX = Random.Range(0, 2);
        if (moveX == 0) moveX--;
        if (type == 1)
        {
            moveY = Random.Range(-1, 2);
         
        }
        if (type == 2)
        {
            moveY = Random.Range(-1, 2);
         

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {if(MoveDmg!=0)
            player.GetComponent<Player>().onHit(MoveDmg);
        }
        else if (collision.gameObject.tag == "Border")
        {

            if (collision.gameObject.tag == "Border")
            {
                if (collision.gameObject.name == "Topborder" && type == 1 || collision.gameObject.name == "Topborder" && type == 2)
                {
                    moveThinkTime = 0;
                    moveY = -1;
                }
                if (enemyName == "Sol" && type == 0&& collision.gameObject.name == "Topborder")
                {
                    moveY = 0;
                }
                if (collision.gameObject.name == "Leftborder")
                {
                    moveThinkTime = 0;
                    moveX = 1;
                }
                if (collision.gameObject.name == "Rightborder")
                {

                    moveThinkTime = 0;
                    moveX = -1;
                }
                if (collision.gameObject.name == "Bottomborder" && type == 1)
                {
                    moveThinkTime = 0;
                    moveY = 1;
                }
                if (collision.gameObject.name == "Bottomborder2" && type == 2)
                {
                    moveThinkTime = 0;
                    moveY = 1;
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
   
       if (collision.gameObject.tag == "Border")
        {
            if (collision.gameObject.name == "Topborder" && type == 1|| collision.gameObject.name == "Topborder" && type == 2)
            {
                moveThinkTime = 0;
                moveY = -1;
            }
            
            if (collision.gameObject.name == "Leftborder")
            {
                moveThinkTime = 0;
                moveX = 1;
            }
            if (collision.gameObject.name == "Rightborder")
            {

                moveThinkTime = 0;
                moveX = -1;
            }
            if (collision.gameObject.name == "Bottomborder" && type == 1)
            {
                moveThinkTime = 0;
                moveY = 1;
            }
            if (collision.gameObject.name == "Bottomborder2" && type == 2)
            {
                moveThinkTime = 0;
                moveY = 1;
            }
        }
    }
}