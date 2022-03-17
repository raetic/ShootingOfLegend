using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeekoQ : MonoBehaviour
{
    Animator Anim;
    int counter = 0;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Invoke("Evolution",1.5f);
    }
    void Evolution()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        Anim.SetTrigger("Attack");
        gameObject.AddComponent<EnemyArrow>();
        GetComponent<EnemyArrow>().type = 1;
        GetComponent<EnemyArrow>().dmg = 15;
        int rand = Random.Range(0, 2);
        if (rand != 1)
        {
            Invoke("disa", 0.5f);
        }
        else
        {
            Invoke("moreEvolution", 0.5f);
        }
       
    }
    void moreEvolution()
    {
        counter++;
        transform.localScale = new Vector2(1 + 0.2f * counter, 1 + 0.2f * counter);
        int rand = Random.Range(0, counter+2);
        if (rand != 1)
        {
            Invoke("disa", 0.5f);
        }
        else
        {
            Invoke("moreEvolution", 0.5f);
        }
    }
    void disa()
    {
        Destroy(gameObject);
    }
}
