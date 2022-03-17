using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zed : MonoBehaviour
{
    [SerializeField] GameObject[] AttackPoint = new GameObject[2];
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] GameObject S;
    [SerializeField] GameObject sha;
    [SerializeField] GameObject shae;
    [SerializeField] GameObject shar;
    [SerializeField] int[] patterncount;
    int count;
    int ulticount;

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
        if (ulticount < 5)
        {
            ulticount++;
            pattern = Random.Range(0, 4);
            if (pattern == 0 || pattern == 3)
            {

                pattern1();
            }
            else if (pattern == 1 || pattern == 2)
            {
                StartCoroutine("p2");

            }
        }
        else
        {
            myEnemy.BM.Warn();
            StartCoroutine("p3");
            ulticount = 0;
        }

      
    }
 
    
    float rgf;
    float rpf;
    void pattern1()
    {
       
          
            StartCoroutine("p1");
  
           
        
    }
    IEnumerator p1()
    {
        int c = 0;
        int rand = Random.Range(5, 10);
        GameObject shadow = Instantiate(sha, new Vector3(Random.Range(-25, 25) * 0.1f, Random.Range(0, 30) * 0.1f, 0), transform.rotation);
        Shadow shadowobj = shadow.GetComponent<Shadow>();
        while (c < 15)
        {
            c++;
            if (c == rand)
            {
                Vector3 sv = shadow.transform.position;
                shadow.transform.position = transform.position;
                transform.position = sv;
            }
            GameObject bul=Instantiate(S,AttackPoint[0].transform.position,transform.rotation);
            Vector3 v =  myEnemy.player.transform.position- transform.position;
            bul.GetComponent<Rigidbody2D>().AddForce(new Vector2(v.normalized.x,-1) * 300);
            if(shadowobj!=null)
            shadowobj.Shoot(S, new Vector2(v.normalized.x, -1));
            yield return new WaitForSeconds(0.55f);
        }
        Invoke("PatternThink",2);
    }
    IEnumerator p2()
    {
        int c = 0;
        int rand = Random.Range(5, 10);
        GameObject shadow = Instantiate(shae, transform.position, transform.rotation);
        shadow.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 300);
        while (c < 10)
        {
            c++;
           
            GameObject bul = Instantiate(S, AttackPoint[0].transform.position, transform.rotation);
            Vector3 v = myEnemy.player.transform.position - transform.position;
            bul.GetComponent<Rigidbody2D>().AddForce(new Vector2(v.normalized.x, -1) * 300);
            yield return new WaitForSeconds(0.4f);
        }
        Invoke("PatternThink", 2);
    }
    IEnumerator p3()
    {
        int c = 0;
        int rand = Random.Range(5, 10);
        transform.position = new Vector3(0, 4.5f, 0);
        GameObject[] Shadows = new GameObject[5];
        int excep=0;
        myEnemy.isMove = false;
        while (c < 20)
        {
            if (c <5)
            {
                Shadows[c] = Instantiate(shar, new Vector2(-2 + c, 3.5f), transform.rotation);
                c++;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                c++;
                if (excep == 0)
                {
                    excep = 1;
                }
                else if (excep == 4)
                {
                    excep = 3;
                }
                else
                { int r = Random.Range(0, 2);
                    if (r == 0) r--;
                    excep += r;
                }
                for (int i = 0; i < 5; i++)
                {
                    if (i != excep)
                    {
                        GameObject bul = Instantiate(S, Shadows[i].transform.position, transform.rotation);
                        bul.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 200);
                        bul.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
              
               yield return new WaitForSeconds(0.6f);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            Destroy(Shadows[i]);
        }
            transform.position = new Vector3(0, 3, 0);
        myEnemy.isMove = true;
        Invoke("PatternThink", 5);
    }
}
