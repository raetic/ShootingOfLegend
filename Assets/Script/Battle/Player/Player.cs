using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float AS;
    public float Dmg;
    public float Hp;
    public float maxHp;
    public float Mp;
    public float maxMp;
    public int crit;
    public int vamp;
    public int[] SkillMp=new int[4];
    [SerializeField] GameObject NormalAttack;
    [SerializeField] Vector3 attackV = new Vector3(0, 0.5f, 1);
    BattleManager BM;
    float StartTime;
    Animator Anim;
    bool isHit;
    public bool[] skill = new bool[4];
    public bool isShoot;
    [SerializeField] Image HpBar;
    [SerializeField] Image MpBar;
    [SerializeField] GameObject redShiled;
   public int redShiledAmount;
    public Ornn or;
    public float CriDmg;
    public float GIStime;
    int GIScount;
    public int MWcount;
    public float speed;
   public bool isStun;
    float StunTime;
    public bool ShiledOn;
   public GameObject Runan;
    [SerializeField] GameObject Runanarrow;
    public bool nextStageOn;
    public Sprite[] skillImages;
    public float[] skillCoolTime;
    bool isPower;
    
   public void nextStage()
    {
        Mp = maxMp;
        nextStageOn = true;
    }
    public void OnStun(float t)
       {if (isPower) return;
        if(or.UpgradeItem[7]==0)
        StunTime = t;
        else
        {
            int rand = Random.Range(0, 2);
            if (rand == 0) StunTime =t;
        }
    }
    public void RunanShoot()
    {
        GameObject A = Instantiate(Runanarrow, Runan.transform.position, transform.rotation);
        A.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400);
        A.GetComponent<PlayerArrow>().dmg = Dmg*0.5f;
    }
    void Start()
    {
        Anim = GetComponent<Animator>();
        BM = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        or = GameObject.Find("Ornn").GetComponent<Ornn>();
        CriDmg = 2;
        speed = 6.5f;
        HpBar = GameObject.Find("HPfull").GetComponent<Image>();
        MpBar = GameObject.Find("MPfull").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StunTime > 0)
        {
            
            isStun = true;
            StunTime -= Time.deltaTime;

        }
        else isStun = false;
        if (!BM.ItemSelectTime)
        {
            if (StunTime <= 0&&!isPower)
                StartTime += Time.deltaTime;
        }
        if (StartTime >= 1 / AS) {
          
            Invoke("AttackAnim", 0.15f);
            StartTime = 0;
            Anim.SetTrigger("Attack");
        }
        if (Mp < maxMp)
        {
            Mp += Time.deltaTime * 0.05f*maxMp;
            if (Mp > maxMp)
                Mp = maxMp;
        }
        HpBar.fillAmount = Mathf.Lerp(HpBar.fillAmount, (float)(Hp / maxHp), 3* Time.deltaTime);
        MpBar.fillAmount = Mathf.Lerp(MpBar.fillAmount, (float)(Mp / maxMp), 3 * Time.deltaTime);
        if (redShiledAmount > 0) redShiled.SetActive(true);
        else redShiled.SetActive(false);
        if (or.UpgradeItem[5] >0)
        {
            GIStime += Time.deltaTime;
            if (GIScount < 10&&GIStime>10)
            {
                AS += 0.1f;
                GIStime = 0;
            }
        }
        if (MWcount > 30)
        {
            MWcount = 0;
            Hp += maxHp * 0.1f;
            speed = 8;
            AS++;
            Invoke("Mwoff", 3);
        }
    }
    void Mwoff()
    {
        AS--;
        speed = 6.5f;
    }
    void AttackAnim()
    {
        Shoot();

    }
    void Shoot()
    {
        isShoot = true;
       
    }
   public void onHit(int dmg)
    {if (BM.ItemSelectTime) return;
        if (isPower) return;
        if (!isHit)
        {
            if (redShiledAmount <= 0)
            {
                Hp -= dmg;
                if (Hp <= maxHp * 0.3f && or.UpgradeItem[10]>0 && !ShiledOn)
                {
                    ShiledOn = true;
                    redShiledAmount = Mathf.FloorToInt(maxHp*0.3f);
                }
                Hit();
                if (Hp <= 0)
                {
                    Die();
                }
            }
            else
            {
                redShiledAmount -= dmg;
                if (redShiledAmount < 0)
                {                   
                    isHit = false;
                    onHit(redShiledAmount * -1);
                    redShiledAmount = 0;
                }
            }
        }
    }
    

    public void Die()
    {
        gameObject.SetActive(false);
        BM.reviveOn();
    }
    public void Hit()
    {
        isHit = true;
        Invoke("HitOff", 0.5f);
        BM.Hit();
    }
    public void HitOff()
    {
        isHit = false;
    }
    public void PowerOn(float t)
    {
        isPower = true;
        Invoke("PowerOff", t);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
    }
    void PowerOff()
    {
        isPower = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }
    public void Vampire(int a)
    {

        Hp += a * vamp * 0.01f;
        if (Hp >= maxHp)
        {
            if (or.UpgradeItem[2] == 0)
                Hp = maxHp;
            else
            {
                redShiledAmount += Mathf.FloorToInt(Hp - maxHp);
                if (redShiledAmount >= 50)
                    redShiledAmount = 50;
                Hp = maxHp;
            }
        }
    }
}
