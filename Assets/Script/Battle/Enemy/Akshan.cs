using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akshan : MonoBehaviour
{
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] int[] patterncount;
    int count;
    int ulticount;
    [SerializeField] GameObject q;
    [SerializeField] GameObject p;
    [SerializeField] GameObject r;
    [SerializeField] GameObject e;
    Vector3[] point = new Vector3[2];
    LineRenderer lr;
    bool p3mode;
    float ackSpeed = 0;
    float ShootTime = 0;
    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        Player = myEnemy.player;
        Invoke("PatternThink", 1);
        point[0] =new Vector3(-3, 5, 0);
        point[1] = new Vector3(3, 5, 0);
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
    }
    float th;
    float circleR;
    Vector3 va=new Vector3(0,2.5f);
    void Update()
    {
        if (!myEnemy.isMove && !p3mode)
        {
            transform.position = Vector3.MoveTowards(transform.position, va, Time.deltaTime);
        }
        if(p3mode){           
            if (ackSpeed < 100)
            {
                ackSpeed += Time.deltaTime * 30;
            }

            if (isL == 1)
            {
                th -= Time.deltaTime * ackSpeed;
            }
            else
            {
                th += Time.deltaTime * ackSpeed;
            }
           
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, point[isLs]);
            var rad = Mathf.Deg2Rad * th;
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
                    transform.position = point[isLs] + new Vector3(y, x);


            ShootTime += Time.deltaTime;
            if (ShootTime > 0.2f)
            {
                ShootTime = 0;
                p3();
            }
        }
    }
    void p3()
    {
        Vector3 v = myEnemy.player.transform.position - transform.position;
     
        GameObject bul = Instantiate(e,transform.position, Quaternion.Euler(new Vector3(0,0, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg+90)));
      
        bul.GetComponent<Rigidbody2D>().AddForce(new Vector2(v.normalized.x, -1) * 300);
    }
    void PatternThink()
    {
     
        myEnemy.isMove = true;
        count = 0;       
        
            pattern = Random.Range(1, 6);
       
            if (pattern == 0 || pattern == 1)
            {
                StartCoroutine("p1");
            }
            else if (pattern == 2)
            {
                pattern = 1;
                StartCoroutine("p2");
            }
            else if (pattern >2)
            {
                lr.startWidth = 0.05f;
                lr.endWidth = 0.05f;
                myEnemy.isMove = false;
                Invoke("pattern3", 0.5f);
              
            }
        
   
    }
    int isL;
    int isHigh;
    int isLs;
    void pattern4()
    {

    }
    void pattern3()
    {
       
        GetComponent<Animator>().SetBool("isE", true);
        if (transform.position.x < 0) isL = 0;
        else isL = 1;
        if (transform.position.y < 2.5f) isHigh = 0;
        else isHigh = 1;
        p3mode = true;
        if (isL == 1 && isHigh == 1)
        {
            isLs = 0;
        }
        else if(isL==0&&isHigh==1)
        {
            isLs = 1;
        }
        else
        {
            isLs = isL;
        }
        GetComponent<Animator>().SetBool("isE", true);
        circleR = Vector2.Distance(point[isLs], transform.position);
        Vector2 v2 = transform.position - point[isLs] ;
        if (isL == 0)
        {
           
            th = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
            
        }
        else
        {
            th = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg ;
        }
       
        ackSpeed = 0;
    }
  IEnumerator p2()
    {
        int c = 0;
        int rand = Random.Range(2, 5);
        while (c < rand)
        {
            c++;
            GameObject nq = Instantiate(q, transform.position, transform.rotation);
            nq.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 300);
            yield return new WaitForSeconds(1);
        }
        Invoke("PatternThink", 1);
    }
    IEnumerator p1()
    {
        int c = 0;
        int rand = Random.Range(5, 10)*2;
        while (c < rand)
        {
            c++;
            if (c == 8)
            {
                GameObject nq = Instantiate(q, transform.position, transform.rotation);
                nq.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 300);
                nq.transform.localScale = new Vector3(1, 1, 1);
            }
            if (c % 2 == 1)
            {
                GameObject nq = Instantiate(p, transform.position, transform.rotation);
                nq.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 400);
                nq.GetComponent<Rigidbody2D>().AddForce(new Vector2( Random.Range(-200, 200),0));
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                GameObject nq = Instantiate(p, transform.position, transform.rotation);
                nq.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 420);
                nq.GetComponent<Rigidbody2D>().AddForce(new Vector2( Random.Range(-200, 200),0));
                yield return new WaitForSeconds(0.7f);
            }
          
        }
        Invoke("PatternThink", 1);
    }
    void pattern1()
    {
        if (count < patterncount[pattern])
        {
            count++;
            StartCoroutine("p1");
            Invoke("pattern1", 2);
        }
        else
        {
            PatternThink();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
      if (collision.gameObject.tag == "Border"&&!myEnemy.isMove)
        { 

            if (collision.gameObject.tag == "Border")
            {
                if (collision.gameObject.name == "Topborder")
                {
                    myEnemy.isMove = true;
                    p3mode = false;
                    Invoke("PatternThink", 2);
                    GetComponent<Animator>().SetBool("isE", false);
                    lr.SetPosition(0, Vector3.zero);
                    lr.SetPosition(1, Vector3.zero);
                }
            
                if (collision.gameObject.name == "Leftborder")
                {
                    myEnemy.isMove = true;
                    p3mode = false;
                    Invoke("PatternThink", 2);
                    GetComponent<Animator>().SetBool("isE", false);
                    lr.SetPosition(0, Vector3.zero);
                    lr.SetPosition(1, Vector3.zero);
                }
                if (collision.gameObject.name == "Rightborder")
                {

                    myEnemy.isMove = true;
                    p3mode = false;
                    Invoke("PatternThink", 2);
                    GetComponent<Animator>().SetBool("isE", false);
                    lr.SetPosition(0, Vector3.zero);
                    lr.SetPosition(1, Vector3.zero);
                }
              
              
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Border" && !myEnemy.isMove)
        {

            if (collision.gameObject.tag == "Border")
            {
                if (collision.gameObject.name == "Topborder")
                {
                    myEnemy.isMove = true;
                    p3mode = false;
                    Invoke("PatternThink", 2);
                    GetComponent<Animator>().SetBool("isE", false);
                }

                if (collision.gameObject.name == "Leftborder")
                {
                    myEnemy.isMove = true;
                    p3mode = false;
                    Invoke("PatternThink", 2);
                    GetComponent<Animator>().SetBool("isE", false);
                }
                if (collision.gameObject.name == "Rightborder")
                {

                    myEnemy.isMove = true;
                    p3mode = false;
                    Invoke("PatternThink", 2);
                    GetComponent<Animator>().SetBool("isE", false);
                }
               

            }
        }
    }
}
