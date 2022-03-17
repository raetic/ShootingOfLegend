using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeri : MonoBehaviour
{
    Player myPlayer;
    [SerializeField] GameObject normalAttack;
    [SerializeField] GameObject Q;
    [SerializeField] GameObject rAttack;
    int craCount;
    float startTime;
    LineRenderer lr;
    [SerializeField] GameObject Dmgtext;
    [SerializeField] float rtime;
    [SerializeField] GameObject e;
    [SerializeField] GameObject r;
    float etime;
    float curx;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponent<Player>();
        myPlayer.skillCoolTime[1] = 1 / myPlayer.AS;
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Q").GetComponent<CoolManager>().coolTime=1/myPlayer.AS;
        if (myPlayer.skill[1])
        {
            skill1();
            myPlayer.skill[1] = false;
        }
        if (myPlayer.skill[2])
        {
            myPlayer.skill[2] = false;
            skill2();
        }
        if (myPlayer.skill[3])
        {
            myPlayer.skill[3] = false;
            skill3();
        }
        if (myPlayer.nextStageOn)
        {
            myPlayer.nextStageOn = false;
         
        }
        startTime += Time.deltaTime;
        if (startTime > 3)
        {
            startTime = 0;
            passive();
        }

        if (etime >0)
        {
            etime -= Time.deltaTime;
            if (transform.position.x > curx)
            {
                e.transform.localPosition = new Vector3(0.25f, 0, 0);
                e.transform.localScale = new Vector3(-1, 1, 0);
            }
            else
            {
                e.transform.localPosition = new Vector3(-0.25f, 0, 0);
                e.transform.localScale = new Vector3(1, 1, 0);
            }
            if(etime<0)
                eoff();
        }

        if (rtime > 0) {
            rtime -= Time.deltaTime;
            if (rtime <= 0)
            {
                skill3off();
            }
        }
      
    }
    void eoff()
    {
        myPlayer.speed -= 20;
        e.SetActive(false);
    }
    void passive()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemys.Length; i++)
        {
            if (!enemys[i].GetComponent<Enemy>().NotBoss)
            {
                enemys[i].GetComponent<Enemy>().onHit(Mathf.FloorToInt(3*myPlayer.Dmg));
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, enemys[i].transform.position);
                Invoke("lroff", 0.3f);
                GameObject gt = Instantiate(Dmgtext, enemys[i].transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), GameObject.Find("DmgCanvas").transform);
                gt.GetComponent<Dmg>().t.text = "" + Mathf.FloorToInt(3 * myPlayer.Dmg);
            }
        }
    }
    void lroff()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
    }
    void shoot()
    {
        for (int i = 0; i < 7; i++)
        {
            float x = Random.Range(-15, 15) * 0.01f;
            float y=Random.Range(-50,50)*0.01f;
            GameObject A = Instantiate(normalAttack, transform.position + new Vector3(x,y, 0), transform.rotation);
            A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
            A.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, 0) * 200);
            myPlayer.isShoot = false;
            A.GetComponent<PlayerArrow>().dmg = Mathf.RoundToInt(myPlayer.Dmg*0.13f);

        }

    }
    
    void crashoot()
    {
        craCount++;
        if (craCount % 3 != 0)
        {
            shoot();
        }
        else
        {

            for (int i = 0; i < 7; i++)
            {
                float x = Random.Range(-15, 15) * 0.01f;
                float y = Random.Range(-50, 50) * 0.01f;
                GameObject A = Instantiate(normalAttack, transform.position + new Vector3(x, y, 0), transform.rotation);
                A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
                A.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, 0) * 200);
                myPlayer.isShoot = false;
                A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg * 0.15f;
                A.GetComponent<PlayerArrow>().dmg *= 1.5f;
                A.GetComponent<Transform>().transform.localScale *= 1.5f;
                A.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            }
          
            craCount = 0;
           

        }
    }
    public void skill1()
    {
        if (rtime <= 0)
        {
            if (myPlayer.or.UpgradeItem[1] == 0)
                shoot();
            else
            {
                crashoot();
            }
        }
        else
        {
            if (myPlayer.or.UpgradeItem[1] == 0)
                StartCoroutine("Rnormal");
            else
            {
                craCount++;
                if (craCount % 3 != 0)
                {
                    StartCoroutine("Rnormal");
                }
                else
                {
                    StartCoroutine("Rcra");
                    craCount = 0;
                }
            }
        }


    }
    void skill2()
    {
        e.SetActive(true);
        etime =0.5f;
        myPlayer.speed += 20;
        curx = transform.position.x;
    }
    void skill3()
    {
        rtime += 5;
        myPlayer.AS += 0.7f;
        myPlayer.speed += 3;

    
    }
    void skill3off()
    {
        myPlayer.AS -= 0.7f;
        myPlayer.speed -= 3;
    }
  
   IEnumerator Rnormal()
    {
        int c = 0;
        while (c < 3)
        {
            float x = Random.Range(-15, 15) * 0.01f;          
            GameObject A = Instantiate(r, transform.position + new Vector3(x, 0, 0), transform.rotation);
            A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400);
            A.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, 0) * 200);
            myPlayer.isShoot = false;
            A.GetComponent<PlayerArrow>().dmg = Mathf.RoundToInt(myPlayer.Dmg * 0.8f);
            c++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Rcra()
    {
        int c = 0;
        while (c < 3)
        {
            float x = Random.Range(-15, 15) * 0.01f;
            GameObject A = Instantiate(r, transform.position + new Vector3(x, 0, 0), transform.rotation);
            A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400);
            A.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, 0) * 200);
            myPlayer.isShoot = false;
            A.GetComponent<PlayerArrow>().dmg = Mathf.RoundToInt(myPlayer.Dmg * 0.8f);
            c++;
            A.GetComponent<PlayerArrow>().dmg *= 1.5f;
            A.GetComponent<Transform>().transform.localScale *= 1.5f;
            A.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
