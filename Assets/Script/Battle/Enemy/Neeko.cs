using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neeko : MonoBehaviour
{
    [SerializeField] GameObject[] AttackPoint = new GameObject[2];
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] GameObject E;
    [SerializeField] GameObject N;
    [SerializeField] GameObject P;
    [SerializeField] GameObject Q;
    [SerializeField] int[] patterncount;
    int count;
    Vector3 curVector;
    [SerializeField] GameObject NeekoFriend;
    [SerializeField] GameObject Rgreen;
    [SerializeField] GameObject Rpurple;

    bool rgmode;
    bool rpmode;
    int ulticount;
    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        Player = myEnemy.player;    
        Invoke("PatternThink", 1);
    }
    void PatternThink()
    {
        
        p3 = false;
        myEnemy.isMove = true;
        count = 0;
        if (ulticount < 5)
        {
            ulticount++;
            pattern = Random.Range(0, 4);
            if (pattern == 0 || pattern == 1 || pattern == 2)
            {
                pattern = 0;
                pattern1();
            }
            else if (pattern == 3)
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

        
        }
    }
    void Update()
    {
        if (!myEnemy.isMove) {
            if (p3&&!p3end)
            {
                transform.position = Vector3.MoveTowards(transform.position, goVector, Time.deltaTime*1.8f);
            }
            else if(!p3end)
            {
                if (transform.position.x != 0 || transform.position.y != 3.5f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector2(0, 3.5f), 2*Time.deltaTime);
                }
                else
                {
                    pattern3();
                }
            }
        }

        if (p3end)
        {         
            if (transform.position.x == 0 && transform.position.y == 3.5f)
            {
                p3end = false;
                PatternThink();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(0, 3.5f), 1.5f * Time.deltaTime);                             
            }
        }
        if (rgmode)
        {
            gtime += Time.deltaTime;
            Rgreen.transform.localScale = new Vector3(gtime * 5/0.65f, gtime * 20/ 0.65f, 1);
        }
        else
        {
            Rgreen.transform.localScale = new Vector3(0, 0, 1);
            gtime = 0;
        }
    }
    float gtime;
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
    IEnumerator p1()
    {
        int c = 0;
        int rand2 = Random.Range(0, 5);
        int rand3 = Random.Range(0, 5);
        while (rand3 == rand2)
        {
            rand3 = Random.Range(0, 5);
        }
        while (c < 15)
        {
            c++;

                GameObject bul;
                int rand = Random.Range(0,2);
                if (c %5== rand2)
                    bul = Instantiate(E, AttackPoint[rand].transform.position, transform.rotation);
                else if (c % 5 == rand3)
                    bul = Instantiate(P, AttackPoint[rand].transform.position, transform.rotation);
                else
                {
                    bul = Instantiate(N, AttackPoint[rand].transform.position, transform.rotation); 
                }
            float rand4 = Random.Range(0, 25);
                bul.GetComponent<Rigidbody2D>().AddForce(new Vector2((12.5f - rand4)*0.05f, -1) * 200);
                if (rand4 < 12)
                {
                    bul.transform.localScale = new Vector3(-1, 1, 0);
                }
            
            yield return new WaitForSeconds(0.15f);
        }
    }
    void pattern2()
    {
        if (count < patterncount[pattern])
        {
            count++;
            int randx = Random.Range(-17, 17);
            int randy = Random.Range(-25, 30);
            GameObject ahyeah = Instantiate(Q, new Vector2(randx, randy) * 0.1f, transform.rotation);
            Invoke("pattern2", 1);
        }
        else
        {
            PatternThink();
        }
    }
   
    Vector3 goVector;
    bool p3;
    
  void pattern3()
    {
        p3 = true;
        int r=Random.Range(0,2);
        GameObject nf = Instantiate(NeekoFriend, transform.position, transform.rotation);
        if (r == 0)
        {
            nf.GetComponent<Enemy>().goVector = new Vector3(-1.5f, -3, 0);
            goVector = new Vector3(1.5f, -3, 0);
        }
        else
        {
            nf.GetComponent<Enemy>().goVector = new Vector3(1.5f, -3, 0);
            goVector = new Vector3(-1.5f, -3, 0);
        }
        Invoke("green", 3f);
    }
    void green()
    {
       
        rgmode = true;
        Rgreen.SetActive(true);
        Invoke("purple", 0.65f);
    }
    void purple()
    {
     
        rgmode = false;
        Rgreen.SetActive(false);
        Rpurple.SetActive(true);
        Invoke("turnHome", 0.45f);
    }
    bool p3end;
    void turnHome()
    {
        p3end = true;
        rpmode = false;
        Rpurple.SetActive(false);
    }
}
