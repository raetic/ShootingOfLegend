using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draven : MonoBehaviour
{
    Player myPlayer;
    float skill1time;
    [SerializeField] GameObject normalAttack;
    [SerializeField] Vector3[] normalV;
    [SerializeField] GameObject Q;
    [SerializeField] GameObject rAttack;
    int craCount;
    int skill1count;
    public GameObject run;
    public float skill2time;
    

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
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
            skill1count = 0;
            GetComponent<Animator>().SetBool("roll2", false);
            GetComponent<Animator>().SetBool("roll1", false);
           
        }
        if (myPlayer.isShoot)
        {
            if (myPlayer.or.UpgradeItem[1] == 0)
                shoot();
            else
            {
                crashoot();
            }
        }
       
        if (skill2time > 0)
        {
            skill2time -= Time.deltaTime;
            if (skill2time <= 0)
            {
                speedDown();
            }
        }
    }
    void shoot()
    {
        if (skill1count < 1)
        {
            GameObject A = Instantiate(normalAttack, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
            myPlayer.isShoot = false;
            A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg;
        }
        else
        {
            GameObject A = Instantiate(Q, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
            myPlayer.isShoot = false;
            A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg*2f;
            if (skill1count == 1)
            {
                GetComponent<Animator>().SetBool("roll1", false);
            }
            else if (skill1count == 2)
            {
                GetComponent<Animator>().SetBool("roll2", false);
            }
            skill1count--;

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
            if (skill1count < 1)
            {
                GameObject A = Instantiate(normalAttack, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
                myPlayer.isShoot = false;
                A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg * 1.5f;
                A.GetComponent<Transform>().transform.localScale *= 1.5f;
                A.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                craCount = 0;
            }
            else
            {
                GameObject A = Instantiate(Q, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
                myPlayer.isShoot = false;
                
                if (skill1count == 1)
                {
                    GetComponent<Animator>().SetBool("roll1", false);
                }
                else if (skill1count == 2)
                {
                    GetComponent<Animator>().SetBool("roll2", false);
                }
                A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg * 3f;
                A.GetComponent<Transform>().transform.localScale *= 1.5f;
                A.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                skill1count--;
                craCount = 0;
            }
          
        }
    }
    public void skill1()
    { if (skill1count < 2)
        { skill1count++; }
        if (skill1count == 1)
        {
            GetComponent<Animator>().SetBool("roll1", true);
        }
        else if(skill1count==2)
        {
            GetComponent<Animator>().SetBool("roll2", true);
        }

    }
    void skill2()
    {
        skill2time = 2;
        speedUp();
    }
    void skill3()
    {
        GameObject A = Instantiate(rAttack, transform.position, transform.rotation);
        A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400);
        A.GetComponent<PlayerArrow>().dmg *= myPlayer.Dmg * 0.12f;
    }
    void speedUp()
    {
        myPlayer.AS += 0.5f;
        myPlayer.speed += 3f;
        run.SetActive(true);
    }
    void speedDown()
    {
        run.SetActive(false);
        myPlayer.AS -= 0.5f;
        myPlayer.speed -= 3f;
    }
    void skill2Clear()
    {
     
            GameObject.Find("W").GetComponent<CoolManager>().CoolClear();
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag=="PlayerAttack"&&collision.gameObject.GetComponent<PlayerArrow>().dmg==0&&
            collision.gameObject.GetComponent<PlayerArrow>().DQ)
        {

            skill1();
            Destroy(collision.gameObject);
            skill2Clear();
        }
      
    }
  
}
