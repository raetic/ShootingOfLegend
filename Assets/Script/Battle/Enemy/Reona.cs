using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reona : MonoBehaviour
{
    Enemy myEnemy;
    GameObject Player;
    int pattern;
    [SerializeField] int[] patterncount;
    int count;
    int ulticount;
    [SerializeField] GameObject p;
    [SerializeField] GameObject r;
    [SerializeField] GameObject AttackPoint;
    [SerializeField] GameObject w;
    private void Start()
    {
        myEnemy = GetComponent<Enemy>();
        Player = myEnemy.player;
        Invoke("PatternThink", 1);
    }
    int prepattern;
    void PatternThink()
    {
        w.SetActive(false);
        myEnemy.isMove = true;
        GetComponent<Animator>().SetBool("isW", false);
        count = 0;
  
        if (ulticount < 3)
        {
            ulticount++;
            if (prepattern == 2)
                pattern = 0;
            else
            pattern = Random.Range(0, 3);
            
            if (pattern == 0 || pattern == 1)
            {
                prepattern = 1;
                pattern = 0;
                pattern1();
            }
            else if (pattern == 2)
            {
                prepattern = 2;
                pattern = 1;
                Invoke("pattern2", 1);
                GetComponent<Animator>().SetBool("isW", true);
             
            }
        }
        else
        {
            prepattern = 3;
            ulticount = 0;
            myEnemy.isMove = false;
            myEnemy.BM.Warn();
            Invoke("pattern3", 1.5f);

        }
    }
    void pattern2()
    {
        w.SetActive(true);
        Invoke("PatternThink", 5f);
    }
    void pattern1()
    {
        if (count < patterncount[pattern])
        {
            count++;
            StartCoroutine("p1");
            Invoke("pattern1", 1.5f);
        }
        else
        {
            PatternThink();
        }
    }
    IEnumerator p1()
    {
        int c = 0;
        GameObject[] ps = new GameObject[3];
        Vector3 v = AttackPoint.transform.position;
        while (c < 6)
        {
            if (c < 3)
            {
                ps[c] = Instantiate(p, v, Quaternion.Euler(0, 0, -30 + 30 * c));
                c++;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                ps[c - 3].GetComponent<Rigidbody2D>().AddForce(new Vector2((c - 4)*0.5f, -1) * (450+50*count));
                c++;
                yield return new WaitForSeconds(0.3f-0.2f*count);
            }

        }

    }
    void pattern3()
    {
        GameObject R = Instantiate(r, myEnemy.player.transform.position, transform.rotation);
        Invoke("PatternThink",3);
    }
}
