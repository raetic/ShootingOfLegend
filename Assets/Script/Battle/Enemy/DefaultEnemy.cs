using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{
    Enemy myEnemy;
    GameObject Player;
    int pattern;
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
            if (pattern == 0 || pattern == 1 || pattern == 2)
            {
                pattern = 0;

            }
            else if (pattern == 3)
            {
                pattern = 1;

            }
        }
        else
        {
            ulticount = 0;
            myEnemy.isMove = false;
            myEnemy.BM.Warn();


        }
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

}
