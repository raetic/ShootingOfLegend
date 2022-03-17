using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinga: MonoBehaviour
{
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] int[] patterncount;
    int count;
    int ulticount;
    [SerializeField] GameObject[] p;
    [SerializeField] GameObject[] tower;
    [SerializeField] GameObject[] w;
    [SerializeField] GameObject[] AttackPoint;
    int towerCount = 0;

    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        Player = myEnemy.player;
        Invoke("PatternThink", 1);
        StartCoroutine("maketower");
    }
    IEnumerator maketower()
    {
        yield return new WaitForSeconds(5);
        while (true)
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag("tower");
            if (towers.Length < 3)
            {
                towerCount++;
                if (towerCount % 3 == 0)
                {
                    Instantiate(tower[1], AttackPoint[3].transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(tower[0], AttackPoint[3].transform.position, transform.rotation);
                }
            }
            yield return new WaitForSeconds(6.3f);
        }
    }
    void PatternThink()
    {
        myEnemy.isMove = true;
        count = 0;
        if (ulticount < 10)
        {
            ulticount++;
            pattern = Random.Range(0, 3);
            if (pattern == 0 || pattern == 1)
            {
                pattern = 0;
                pattern1();
            }
            else if (pattern == 2)
            {
                pattern = 1;
                pattern2();
            }
        }
        else
        {
            ulticount = 0;
            
            myEnemy.BM.Warn();
            Invoke("pattern3",1);
            pattern = 2;
        }
    }

    void pattern1()
    {
      
            GameObject p1 = Instantiate(p[0], AttackPoint[0].transform.position, transform.rotation);
            GameObject p2 = Instantiate(p[1], AttackPoint[1].transform.position, transform.rotation);
            int Rand = Random.Range(-30, 30);
            p1.GetComponent<Rigidbody2D>().AddForce(new Vector2(60+Rand, -350));
            Rand = Random.Range(-30, 30);
            p2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-60+Rand, -350));
       
           Invoke("PatternThink",1);
       
    }
    void pattern2()
    {

        
            StartCoroutine("p2");
       
    }
    void pattern3()
    {

        if (count < patterncount[pattern])
        {
            count++;
            StartCoroutine("p3");
        }
        else
        {
            PatternThink();
        }
    }
    IEnumerator p2()
    {
        int t = 0;
        GameObject[] mis = new GameObject[5];
        for(int i = 0; i < 5; i++)
        {
            mis[i] = Instantiate(w[0], AttackPoint[2].transform.position, Quaternion.Euler(new Vector3(0, 0, -60 + 30 * i)));
            mis[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-20 + 10 * i, -40));
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i);
            mis[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(0.1f);
        while (t < 10)
        {
            t++;
            for (int i = 0; i < 5; i++)
            {             if(mis[i]!=null)
                mis[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-10 + 5 * i, -20)*4);
            }
            yield return new WaitForSeconds(0.1f);
        }
        PatternThink();
    }
    IEnumerator p3()
    {
        int t = 0;
        GameObject[] mis = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            mis[i] = Instantiate(w[1], AttackPoint[2].transform.position, Quaternion.Euler(new Vector3(0, 0, -60 + 30 * i)));
            mis[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-20 + 10 * i, -40));
        }
        while (t < 4)
        {
            t++;
            for (int i = 0; i < 5; i++)
            {
                if (mis[i] != null)
                    mis[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-10 + 5 * i, -20) * 6);
            }
            yield return new WaitForSeconds(0.1f);
        }
        pattern3();
    }
}
