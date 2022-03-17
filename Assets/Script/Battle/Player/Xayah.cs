using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xayah : MonoBehaviour
{
    Player myPlayer;
    [SerializeField] GameObject normalAttack;
    [SerializeField] Vector3[] normalV;
    [SerializeField] GameObject Q;
    [SerializeField] GameObject rAttack;
    int craCount;
    int passivecount;
    public GameObject run;
    public float skill1time;
    [SerializeField] GameObject W;


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
            passivecount = 0;   
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

        if (skill1time > 0)
        {
            skill1time -= Time.deltaTime;
            if (skill1time <= 0)
            {
                speedDown();
            }
        }
    }
    void shoot()
    {
      
        GameObject A = Instantiate(normalAttack, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
        myPlayer.isShoot = false;
        A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg;
        if (skill1time > 0)
            A.AddComponent<Xfeather>();
        else if (passivecount > 0)
        {
            A.AddComponent<Xfeather>();
            passivecount--;
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
         
            GameObject A = Instantiate(normalAttack, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
            myPlayer.isShoot = false;
            A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg * 1.5f;
            A.GetComponent<Transform>().transform.localScale *= 1.5f;
            A.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            craCount = 0;
            if (skill1time > 0)
                A.AddComponent<Xfeather>();
            else if (passivecount > 0)
            {
                A.AddComponent<Xfeather>();
                passivecount--;
            }

        }
    }
    public void skill1()
    {
        skill1time = 4;
        speedUp();
        

    }
    void skill2()
    {
        GameObject[] feathers = GameObject.FindGameObjectsWithTag("feather");
        for(int i = 0; i < feathers.Length; i++)
        {
            Destroy(feathers[i].GetComponent<TimeDisappear>());
            feathers[i].GetComponent<Xfeather>().come();
        }
        passivecount += 3;
        if (passivecount > 5) passivecount = 5;
    }
    void skill3()
    {
        myPlayer.PowerOn(1);

        Invoke("skill3Shoot", 0.5f);
      
        passivecount += 3;
        if (passivecount > 5) passivecount = 5;
    }
    void skill3Shoot()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject A = Instantiate(normalAttack, transform.position, transform.rotation);
            A.GetComponent<Rigidbody2D>().AddForce(new Vector2((i - 2) * 0.1f, 1) * 450);
            A.transform.rotation = Quaternion.Euler(0, 0, (i - 4) * -5);
            A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg * 0.5f;
            A.GetComponent<PlayerArrow>().isSkill = true;
            A.AddComponent<Xfeather>();
        }
    }
    void speedUp()
    {
        myPlayer.AS += 0.7f;
     
        W.SetActive(true);
    }
    void speedDown()
    {
        W.SetActive(false);
        myPlayer.AS -= 0.7f;
     
    }

}
