using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{

    [SerializeField] GameObject Dmgtext;
    [SerializeField] GameObject criDmgtext;
    public float dmg;
    [SerializeField]bool cc;
    [SerializeField] float cctime;
    public bool notDestroy;

    public bool isCri;
    public bool isSkill;
    Player player;
    Ornn Or;
    [SerializeField] bool DravenQ;
    public GameObject DQ;
    [SerializeField] bool ZQ;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Or = GameObject.Find("Ornn").GetComponent<Ornn>();
        int rand = Random.Range(0, 101);
        if (rand <= player.crit)
        {
            if (!isSkill)
            {
                isCri = true;
                dmg *= player.CriDmg;
            }
            if (isSkill && Or.UpgradeItem[14] > 0)
            {
                isCri = true;
                dmg *= player.CriDmg;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArrowBorder")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "tower")
        {
            if (dmg > 0)
            {
                collision.gameObject.GetComponent<Enemy>().onHit(Mathf.FloorToInt(dmg));
                if (isCri)
                {
                    if (Or.UpgradeItem[12] > 0)
                    {
                        player.RunanShoot();
                    }
                    if (Or.UpgradeItem[13] > 0)
                    {
                        GameObject.Find("R").GetComponent<CoolManager>().rCooldown();
                    }
                }
                if (Or.UpgradeItem[4] > 0)
                {
                    dmg += Mathf.FloorToInt(player.maxMp * 0.02f);
                }
                if (!isCri)
                {
                    GameObject gt = Instantiate(Dmgtext, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), GameObject.Find("DmgCanvas").transform);
                    gt.GetComponent<Dmg>().t.text = "" + Mathf.FloorToInt(dmg);
                }
                else
                {
                    GameObject gt = Instantiate(criDmgtext, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), GameObject.Find("DmgCanvas").transform);
                    gt.GetComponent<Dmg>().t.text = "" + Mathf.FloorToInt(dmg);
                }

                if (cc)
                {
                    collision.gameObject.GetComponent<Enemy>().cctime += cctime;
                }
                if (!isSkill)
                {
                    if (Or.UpgradeItem[6] > 0)
                    {
                        player.MWcount++;
                    }
                    if (Or.UpgradeItem[8] > 0)
                    {
                        player.Mp += player.maxMp * 0.02f;
                    }
                    player.Vampire(Mathf.FloorToInt(dmg));
                }
                else
                {
                    if (Or.UpgradeItem[11] > 0)
                    {
                        player.Vampire(Mathf.FloorToInt(dmg));
                    }
                }
                if (DravenQ)
                {
                    dravenQ();
                }
                if (ZQ)
                {
                    GameObject.Find("W").GetComponent<CoolManager>().leftTime -= 0.1f;
                }
                if (!notDestroy)
                    Destroy(gameObject);

            }
        }
    }
    void dravenQ()
    {
        GameObject DravenQ1 = Instantiate(gameObject, transform.position, transform.rotation);
        DravenQ1.GetComponent<PlayerArrow>().dmg = 0;
        DravenQ1.GetComponent<Rotate>().speed = 360;
        DravenQ1.AddComponent<TimeDisappear>().time = 1;
        
        GameObject DravenQ2 = Instantiate(DQ, player.transform.position, Quaternion.Euler(Vector3.zero));
        Vector3 v = DravenQ1.transform.position - DravenQ2.transform.position;
        DravenQ1.GetComponent<Rigidbody2D>().AddForce(v * -50);
    }
}
