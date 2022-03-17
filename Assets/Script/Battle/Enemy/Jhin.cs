using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jhin : MonoBehaviour
{
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] int[] patterncount;
    int count;
    int ulticount;
    [SerializeField] GameObject normalp;
    [SerializeField] GameObject normalp4;
    [SerializeField] GameObject attackpoint;
    [SerializeField] GameObject w;
    [SerializeField] GameObject[] ColBack;
    [SerializeField] GameObject col;
    [SerializeField] GameObject col4;
    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        Player = myEnemy.player;
        Invoke("PatternThink", 1);
    }
    void PatternThink()
    {

        myEnemy.isMove = true;
        count = 0;
        if (ulticount < 4)
        {
            if (pattern == 1) pattern = 0;
            else
            {
                pattern = Random.Range(0, 2);
            }
            if (pattern == 0)
            {
                pattern = 0;
                pattern1();

            }
            else if (pattern == 1)
            {
                pattern = 1;
                pattern2();
            }
        }
        else
        {
            ulticount = 0;
            myEnemy.isMove = false;
            myEnemy.BM.Warn();
            p3start=true;
            Invoke("pattern3", 2);

        }
    }
    bool p3start;
    void Update()
    {
        if (!myEnemy.isMove&&p3start)
        {
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(0, 10, 0), 5 * Time.deltaTime);
            if(transform.position==new Vector3(0, 10, 0))
            {
                ColBack[0].SetActive(true);
                ColBack[1].SetActive(true);
            }
        }
        if (!myEnemy.isMove && !p3start)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,4, 0), 5 * Time.deltaTime);
            if (transform.position == new Vector3(0, 10, 0))
            {
                ColBack[0].SetActive(true);
                ColBack[1].SetActive(true);
            }
        }

    }
    int colCount;
    void pattern3()
    {
        GameObject p;
        colCount++;
        Vector2 v = new Vector2((myEnemy.player.transform.position - transform.position).normalized.x, 0);
        Vector2 v2 =myEnemy.player.transform.position - transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        if (colCount < 4)
        {
            p = Instantiate(col, new Vector3(0,7,0), Quaternion.Euler(new Vector3(0,0,angle-95)));
            p.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1000);
            p.GetComponent<Rigidbody2D>().AddForce(v*1500);
            Invoke("pattern3", 1f);
        }
        else if (colCount == 4)
        {
            p = Instantiate(col4, new Vector3(0, 7, 0), Quaternion.Euler(new Vector3(0, 0, angle+90)));
            p.GetComponent<Rigidbody2D>().AddForce(Vector2.down *1500);
            p.GetComponent<Rigidbody2D>().AddForce(v *2000);
            Invoke("pattern3", 1f);
        }
        else
        {
            colCount = 0;
            p3start = false;
            ColBack[0].SetActive(false);
            ColBack[1].SetActive(false);
            Invoke("PatternThink", 1.5f);
        }
    }
    void pattern2()
    {
     
        GameObject jw = Instantiate(w, attackpoint.transform.position, transform.rotation,transform);
        Invoke("PatternThink", 0.5f);
    }
    void pattern1()
    {
        if (count < patterncount[pattern])
        {
            GameObject p;
            if (count == 3)
            {
                 p = Instantiate(normalp4, attackpoint.transform.position, transform.rotation);
                p.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1000);      
            }
            else
            {
                p = Instantiate(normalp, attackpoint.transform.position, transform.rotation);
                p.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 800);
            }
            p.GetComponent<Rigidbody2D>().AddForce(new Vector2(myEnemy.player.transform.position.x - transform.position.x,0) * 100);
            Invoke("pattern1", 1.5f);
            count++;
        }
        else
        {
            ulticount++;
            Invoke("PatternThink",1f);
        }
    }

}
