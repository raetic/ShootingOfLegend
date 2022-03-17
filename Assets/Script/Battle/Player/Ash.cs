using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ash : MonoBehaviour
{
    Player myPlayer;
    float skill1time;
    [SerializeField] GameObject normalAttack;
    [SerializeField] Vector3[] normalV;
    [SerializeField] GameObject wAttack;
    [SerializeField] GameObject rAttack;
    int craCount;
    
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
        if (myPlayer.isShoot)
        {
            if(myPlayer.or.UpgradeItem[1]==0)
            shoot();
            else
            {
                crashoot();
            }
        }
            if (skill1time >= 0)
        {
            skill1time -= Time.deltaTime;
                       
        }
    }
    void shoot()
    {
        GameObject A = Instantiate(normalAttack, transform.position + new Vector3(0,0.5f,0), transform.rotation);
        A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
        myPlayer.isShoot = false;
        A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg;
        if (skill1time > 0)
        {
            GameObject A2 = Instantiate(normalAttack, transform.position + normalV[0], transform.rotation);
            GameObject A1 = Instantiate(normalAttack, transform.position + normalV[1], transform.rotation);
            A2.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
            A2.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg*0.5f;
            A1.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
            A1.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg*0.5f;

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
            A.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg*1.5f;
            A.GetComponent<Transform>().transform.localScale = new Vector2(1.5f, 1.5f);
            A.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            if (skill1time > 0)
            {
                GameObject A2 = Instantiate(normalAttack, transform.position + normalV[0], transform.rotation);
                GameObject A1 = Instantiate(normalAttack, transform.position + normalV[1], transform.rotation);
                A2.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
                A2.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg*1.5f*0.5f;
                A1.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
                A1.GetComponent<PlayerArrow>().dmg = myPlayer.Dmg*1.5f*0.5f;
                A1.GetComponent<Transform>().transform.localScale = new Vector2(1.5f, 1.5f);
                A1.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                A2.GetComponent<Transform>().transform.localScale = new Vector2(1.5f, 1.5f);
                A2.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            }
            craCount = 0;
        }
    }
    void skill1()
    {
        skill1time = 3;
        
    }
    void skill2()
    {
        for(int i = 0; i < 9; i++)
        {
            GameObject A = Instantiate(wAttack, transform.position + normalV[2], transform.rotation);
            A.GetComponent<Rigidbody2D>().AddForce(new Vector2((i-4)*0.1f,1)*300);
            A.transform.rotation = Quaternion.Euler(0, 0, (i - 4) * -5);
            A.GetComponent<PlayerArrow>().dmg *= myPlayer.Dmg * 0.1f;
        }
    }
    void skill3()
    {
        GameObject A = Instantiate(rAttack, transform.position + normalV[2], transform.rotation);
        A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
        A.GetComponent<PlayerArrow>().dmg *= myPlayer.Dmg * 0.1f;
    }
  
}
