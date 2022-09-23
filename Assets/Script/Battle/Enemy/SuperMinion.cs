using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMinion : MonoBehaviour
{
    [SerializeField] GameObject[] AttackPoint = new GameObject[3];
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] GameObject Shiled;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject Minion;
    [SerializeField] int[] patterncount;
    int count;
    bool p4;
    Vector3 curVector;
    int ulticount;
    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        Player = myEnemy.player;
        Invoke("PatternThink",2);
    }
    void PatternThink()
    {
        myEnemy.isMove = true;
        count = 0;
        if (ulticount < 5)
        {
            ulticount++;
            pattern = Random.Range(0,5);
            if (pattern == 0 || pattern >= 3)
            {
                pattern = 0;
                pattern1();
            }
            else if (pattern == 1)
            {
                pattern = 1;
                pattern2();
            }
            else if (pattern == 2)
            {
                pattern = 2;
                pattern3();
            }
        }
        else
        {
            pattern = 3;
            curVector = transform.position;
            myEnemy.BM.Warn();
            Invoke("pattern4", 3f);
            ulticount = 0;
        }
    }
    void pattern1()
    {
        if (count < patterncount[pattern])
        {
            count++;
            StartCoroutine("p1");
            Invoke("pattern1",2);
            if (count == 3)
            {                
                GameObject shil = Instantiate(Shiled, AttackPoint[0].transform.position, transform.rotation);
                shil.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,2)*0.1f, -1) * 200);               
            }
        }
        else
        {
            PatternThink();
        }
    }
    IEnumerator p1()
    { int c = 0;
        while (c<5)
        {
            c++;
            for (int i = 0; i < 5; i++)
            {
                GameObject bul = Instantiate(bullet, AttackPoint[2].transform.position, transform.rotation);
                bul.GetComponent<Rigidbody2D>().AddForce(new Vector2(2 - i, -1)*250);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    void pattern2()
    {
        
        Invoke("PatternThink", 5);
        StartCoroutine("p2");
        myEnemy.isMove = false;
        
    }
    IEnumerator p2()
    {
        int c = 0;
        while (c < 2)
        {
            c++;
            for (int i = 0; i < 2; i++)
            {
                GameObject Mini = Instantiate(Minion, new Vector2(-2+4*i,5.5f), transform.rotation);             
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
    void pattern3()
    {
        Invoke("PatternThink", 2);      
        GameObject S = Instantiate(Shiled, AttackPoint[0].transform.position, transform.rotation);
        S.GetComponent<MinionShiled>().S();
        S.GetComponent<Rigidbody2D>().AddForce(new Vector2(myEnemy.player.transform.position.x-transform.position.x,
        myEnemy.player.transform.position.y - transform.position.y).normalized*300f);
    }
    Vector3 pVector;
    void pattern4()
    {
        if (count < patterncount[pattern])
        {
            p4back = false;
            count++;
            myEnemy.isMove = false;
            p4 = true;
             pVector= myEnemy.player.transform.position ;
        }
        else
        {
            Invoke("PatternThink", 1);
        }
    }
    bool p4back;
    private void Update()
    {
        if (p4)
        {
            if (transform.position != pVector)
            {
                transform.position = Vector3.MoveTowards(transform.position, pVector, Time.deltaTime * 8);
            }
            else
            {
                p4 = false;
                p4back = true;
            }
        }
        if (p4back)
        {
            if (transform.position != curVector)
            {
                transform.position = Vector3.MoveTowards(transform.position, curVector, Time.deltaTime * 3);
            }
            else
            {
                Invoke("pattern4", 1);
                p4back = false;
            }
        }
    }
}
