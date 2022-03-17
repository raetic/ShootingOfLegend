using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Irel : MonoBehaviour
{
    [SerializeField] GameObject[] Attack = new GameObject[6];
    bool[] isAttack = new bool[6];
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] int[] patterncount;
    int count;
    [SerializeField] GameObject Weapon;
    float weapony = 0;
    bool isUp;
    int ulticount;
    
    [SerializeField] GameObject Square;
    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Invoke("PatternThink", 1);
        SetNormal();

    }
    void Update()
    {
       
           for(int i = 0; i < 6; i++)
        {
            if (isp3[i])
            {
             
                        Attack[i].transform.position = Vector3.MoveTowards(Attack[i].transform.position, Rv[i], 5 * Time.deltaTime);
                   
            }
        }
        
        if (isUp )
        {
            weapony += Time.deltaTime;
            if(weapony>0.5f)        
            isUp = false;
        }
        if (!isUp)
        {
            weapony -= Time.deltaTime;
            if (weapony < -0.5f) 
            isUp = true;
        }
        if (Weapon != null)
        {
                Weapon.transform.position = new Vector3(myEnemy.transform.position.x, myEnemy.transform.position.y + weapony*0.25f, 5);
       
        }
        if (p211)
        {
            Attack[p21].transform.position = Vector3.MoveTowards(Attack[p21].transform.position, p2V[0], 10f * Time.deltaTime);
        }
        if (p222)
        {
            Attack[p22].transform.position = Vector3.MoveTowards(Attack[p22].transform.position, p2V[1], 10f * Time.deltaTime);
        }
        if (!myEnemy.isMove)
        {
            GetComponent<Animator>().SetBool("Q", true);
            if (p3count < 7)
            {
                transform.position = Vector3.MoveTowards(transform.position,Rv[p3count], 25* Time.deltaTime);
                Vector2 v2 =transform.position - Rv[p3count];
                float gd = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0,0,gd));                     
                if (transform.position == Rv[p3count])
                {
                    GetComponent<Animator>().SetBool("Q",true);
                    p3count++;
                }
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 0);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                GetComponent<Animator>().SetBool("Q", false);
                Invoke("PatternThink", 4);
                p3count = 0;
                myEnemy.isMove = true;
            
            }
        }
    }
    bool line;
    void PatternThink()
    {
        
        SetNormal();
        myEnemy.isMove = true;
        count = 0;
        if (ulticount < 5)
        {
            ulticount++;
            pattern = Random.Range(0, 5);
            if (pattern == 0 || pattern == 3)
            {
                pattern = 0;
                pattern1();
            }
            else if (pattern == 1 || pattern == 2)
            {
                StartCoroutine("p2");

            }

           else if (pattern == 4)
            {
                patterncount[pattern] = Random.Range(4, 10);
                Invoke("pattern4", 2f);
            }
        }
        else
        {

            myEnemy.BM.Warn();
            StartCoroutine("p3");

            ulticount = 0;
        }
    }

    void pattern4()
    {
        SetNormal();
        if (count < patterncount[pattern])
        {
            count++;
            for(int i = 0; i < 6; i++)
            {
                Attack[i].transform.parent = GameObject.Find("Ornn").transform;
                Attack[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 + i *0.3f, -2+(i%3)*-1f) * (115+5*count)); ;
            }
            Invoke("pattern4", 1.5f);
        }
        else
        {
            Invoke("PatternThink",2);
        }
    }
    void pattern1()
    {
        SetNormal();
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
    IEnumerator p1()
    {
       

        int c = 0;
        while (c < 6)
        {
            int rand = Random.Range(0, 6);
            while (isAttack[rand])
                rand = Random.Range(0, 6);
            isAttack[rand] = true;
            Attack[rand].transform.parent = GameObject.Find("Ornn").transform;
            Vector2 v = myEnemy.player.transform.position - transform.position;
            Attack[rand].GetComponent<Rigidbody2D>().AddForce(v* 100);
            Attack[rand].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            c++;
            yield return new WaitForSeconds(0.5f);
        }
       
        Invoke("pattern1",2f);
    }
    bool pa2;
    int p21;
    int p22;
    bool p211;
    bool p222;
    Vector3[] p2V = new Vector3[7];
    
    IEnumerator p2()
    {
        float p2x = Random.Range(-3, 0);
        pa2 = true;
        int c = 0;
        while (c < 4)
        {
            if (c < 2)
            {
                if (c == 0)
                {
                    p21 = Random.Range(0, 6);
                    Attack[p21].transform.parent = GameObject.Find("Ornn").transform;
                    isAttack[p21] = true;
                    Attack[p21].GetComponent<EnemyArrow>().dmg = 0;
                    Attack[p21].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
                    p211 = true;
                    
                    p2V[c] = new Vector2(p2x,myEnemy.player.transform.position.y);
                }

                else if (c == 1)
                {
                    p22 = Random.Range(0, 6);
                    while (p21 == p22)
                        p22 = Random.Range(0, 6);
                    p222 = true;
                    isAttack[p22] = true;
                    Attack[p22].transform.parent = GameObject.Find("Ornn").transform;
                    Attack[p22].GetComponent<EnemyArrow>().dmg = 0;
                    Attack[p22].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
                    p2V[c] = new Vector2(p2x+3, myEnemy.player.transform.position.y);
                }

               
                c++;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                Vector2 v2 = Attack[p21].transform.position - Attack[p22].transform.position;
                float gd = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
                GameObject sq = Instantiate(Square, (Attack[p21].transform.position + Attack[p22].transform.position)*0.5f, Quaternion.Euler(0,0,gd));
                sq.transform.localScale = new Vector3(v2.x,0.1f,1);
                p211 = false;
                p222 = false;
                int rand = Random.Range(0, 6);
                while (isAttack[rand])
                    rand = Random.Range(0, 6);
                isAttack[rand] = true;
                Attack[rand].transform.parent = GameObject.Find("Ornn").transform;
                Vector2 v = myEnemy.player.transform.position - transform.position;
                Attack[rand].GetComponent<Rigidbody2D>().AddForce(v * 60);
                Attack[rand].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                c++;
                yield return new WaitForSeconds(0.5f);
            }
            
        }
      
        Invoke("PatternThink", 1.5f);
    }
    void SetNormal()
    {
        line = false;
        for (int i = 0; i < 6; i++)
        {
            if (i < 3)
            {
                Attack[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 20));
                Attack[i].transform.position = new Vector3(myEnemy.transform.position.x - 0.5f, myEnemy.transform.position.y - 0.7f + 0.7f * i,5);
            }
            else
            {
                Attack[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -20));
                Attack[i].transform.position = new Vector3(myEnemy.transform.position.x + 0.5f, myEnemy.transform.position.y - 0.7f + 0.7f * (i - 3),5);
                
            }
            isAttack[i] = false;
            Attack[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Attack[i].transform.parent = Weapon.transform;
            Attack[i].GetComponent<EnemyArrow>().dmg = 10;
            Attack[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            isp3[i] = false;
        }
    }
    
    Vector3[] Rv = new Vector3[7];
    bool[] isp3 = new bool[6];
    int p3count = 0;
    int[] p3ss = new int[6];
    IEnumerator p3()
    {
        int rand = Random.Range(0, 2);
        int c = 0;
        int[] randomint1 = new int[3];
        int[] randomint2 = new int[3];
        bool[] isbool1 = new bool[3];
        bool[] isbool2 = new bool[3];
        for(int i = 0; i < 3; i++)
        {
            randomint1[i] = Random.Range(0, 3);
            while (isbool1[randomint1[i]])
            {
                randomint1[i] = Random.Range(0, 3);
            }
            isbool1[randomint1[i]] = true;
            randomint2[i] = Random.Range(0, 3);
            while (isbool2[randomint2[i]])
            {
                randomint2[i] = Random.Range(0, 3);
            }
            isbool2[randomint2[i]] = true;
        }
        while (c < 6)
        {                                              
            isAttack[c] = true;
            isp3[c] = true;
            
            if (rand == 0) rand--;
            float randY;
            float x;
            if (c % 2 == 0)
            {
                x = -2.5f +randomint1[c/2]*2.5f;
                randY = 3.5f*rand;
                if (rand > 0) randY += 0.5f;
            }
            else
            {
                x = 2.5f - +randomint2[c / 2] * 2.5f;
                randY = -3.5f * rand;
                if (randY > 0) randY += 0.5f;
            }
            Rv[c] = new Vector3(x,randY, 5);
            Attack[c].transform.parent = GameObject.Find("Ornn").transform;
            Attack[c].GetComponent<EnemyArrow>().dmg = 0;
            Attack[c].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            c++;
                yield return new WaitForSeconds(1f);
        }
        myEnemy.isMove = false;
        p3count = 0;
        GetComponent<Animator>().SetBool("Q", true);
        Rv[6] = Rv[0];   
    }
}
