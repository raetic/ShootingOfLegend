using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sol : MonoBehaviour
{
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] int[] patterncount;
    int count;
    int ulticount;
    [SerializeField] GameObject[] YS;
    [SerializeField] GameObject p;
    [SerializeField] GameObject q;
    [SerializeField] GameObject r;
    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        Player = myEnemy.player;
        Invoke("PatternThink", 1);
        ysspeed = 50;
        circler = 1.5f;
        wantR = 1.5f;
        wantspeed = 50;
        myEnemy.enemyName = "Sol";
        //StartCoroutine("MovingSinCos");
    }
    public IEnumerator MovingSinCos()
    {       
        while (true)
        {
            for (int th = 0; th < 360; th++)
            {
                var rad = Mathf.Deg2Rad * th;
                var x = 3 * Mathf.Sin(rad);
                var y = 3 * Mathf.Cos(rad);
                YS[0].transform.position = transform.position+new Vector3(x, y);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    float circler;
    int speed;
   float th = 0;
    float ysspeed;
    float wantR;
    float wantspeed;
    void Update()
    {
        th += Time.deltaTime*ysspeed;
        if (th < 360)
        { for (int i = 0; i < 3; i++)
            {
                var rad = Mathf.Deg2Rad * (th+(i*120));
                var x = circler * Mathf.Sin(rad);
                var y = circler * Mathf.Cos(rad);
                YS[i].transform.position = transform.position + new Vector3(x, y);
                YS[i].transform.rotation = Quaternion.Euler(0, 0, (th + (i * 120)) * -1);
            }
        }
        else
        {
            th = 0;
        }
        if (Mathf.Abs(wantR - circler) > 0.01f)
        {
            if (wantR > circler)
            {
                circler += Time.deltaTime * 5f;

            }
            else if (wantR < circler)
            {
                circler -= Time.deltaTime * 5f;
            }
        }
        if (Mathf.Abs(wantspeed- ysspeed) > 0.01f)
        {
            if (wantspeed > ysspeed)
            {
                ysspeed += Time.deltaTime * 100f;

            }
            else if (wantspeed < ysspeed)
            {
                ysspeed -= Time.deltaTime * 100f;
            }
        }
        if (px != null)
        {
            pxsize += Time.deltaTime*0.75f;
            px.transform.localScale = new Vector2(pxsize, pxsize);
        }
        else
        {
            pxsize = 0;
        }
    }
    float pxsize;
    void PatternThink()
    {

        
        myEnemy.isMove = true;
        count = 0;
        if (ulticount < 3)
        {
            ulticount++;
            pattern = Random.Range(2, 4);
            if (pattern == 0 || pattern == 1 || pattern == 2)
            {
                pattern = 0;
                pattern1();
            }
            else if (pattern == 3)
            {
                myEnemy.type = 2;
                pattern = 1;
                wantspeed = 150;
                wantR = 4;
                Invoke("pattern2", 1);
            }
        }
        else
        {
            ulticount = 0;
            myEnemy.isMove = false;
            myEnemy.BM.Warn();
            StartCoroutine("p3");

        }
    }
    GameObject px;
  IEnumerator p1()
    {
        int c = 0;
         px= Instantiate(q, transform.position, transform.rotation);
        px.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 35);
        while (c < 8)
        {
            c++;
            GameObject pxx = Instantiate(p, transform.position, Quaternion.Euler(0,0,270));
            pxx.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 350);
            if (px != null)
            {
                int x=0;
                if (px.transform.position.x > myEnemy.player.transform.position.x)
                {
                    x = -20;
                }
                if (px.transform.position.x < myEnemy.player.transform.position.x)
                {
                    x = 20;
                }
                px.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 20*c);
                px.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, 0));
            }
            yield return new WaitForSeconds(0.3f);
        }
        Invoke("pattern1", 3f);
    }
    void pattern1()
    {
        if (count < patterncount[pattern])
        {
            count++;
            StartCoroutine("p1");
            
        }
        else
        {
            PatternThink();
        }
    }
    void pattern2()
    {
        if (count < patterncount[pattern])
        {
            myEnemy.speed = 2;
            myEnemy.type = 2;
            count++;
            wantR = 4;
            Invoke("pattern2", 1);
        }
        else
        {
            myEnemy.speed = 1.5f;
            myEnemy.type = 0;
            wantspeed = 50;
            wantR = 1.5f;
            myEnemy.moveY = 1;
            Invoke("PatternThink", 2);
        }
    }
IEnumerator p3()
    { int t = 0;
        while (t < 2)
        {
            t++;
            yield return new WaitForSeconds(1);
        }
        int c = 0;
        GameObject robj=null;
        while (c < 20)
        {
            if (c % 2 == 0)
            {
                float rand = Random.Range(-2, 3);
                robj = Instantiate(r, new Vector3(transform.position.x,transform.position.y-0.5f,0),Quaternion.Euler(0,0,rand*10-transform.position.x*15));
                yield return new WaitForSeconds(0.5f);
                c++;
            }
            else
            {
                robj.transform.GetChild(1).gameObject.SetActive(true);
                c++;
                yield return new WaitForSeconds(0.3f);
            }
          
           
        }
        Invoke("PatternThink", 2);
    }
}
