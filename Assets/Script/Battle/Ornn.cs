using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ornn : MonoBehaviour
{
    Animator Anim;
    public int[] NormalItem=new int[5]; //0->롱소 1->단검 2->치명 3->여눈 4->흡혈
    public int[] UpgradeItem=new int[16];
    Player P;
    public GameObject[] NormalItemPrefebs;
    public GameObject[] ItemPrefebs0;
    public GameObject[] ItemPrefebs1;
    public GameObject[] ItemPrefebs2;
    public GameObject[] ItemPrefebs3;
    public GameObject[] ItemPrefebs4;
    [SerializeField] GameObject takeOne;
    public List<int> curNormalItem = new List<int>();
    [SerializeField] GameObject[] curItemButton;
    [SerializeField] Sprite[] sprties;
    [SerializeField] BattleManager BM;
    [SerializeField] GameObject NoUpgrade;
    [SerializeField] GameObject[] ItemCanvas;
    [SerializeField] Text[] stat;

    /*
     0->죽검 1->크라켄 2->피바 3->무대 4->무라마나 5->구인수 6->몰왕 7->유령무희 8->정수 9->죽음의 무도 10->철갑궁
    11->균열 12->루난 13->나보리 14->보건
     */
    public void SetCharacterStat()
    {
        setItemList();
        GameObject[] Items = GameObject.FindGameObjectsWithTag("Item");
        for(int i = Items.Length - 1; i>=0; i --)
        {
            if (Items[i] != null)
            {
                Destroy(Items[i]);
            }
        }
        P.Dmg = 10 + NormalItem[0];
        P.AS = 1 + 0.1f * NormalItem[1];
        P.crit = NormalItem[2] * 10;
        P.maxMp = 100 + NormalItem[3]*20;
        P.vamp = 10 + 3 * NormalItem[4];
        P.Dmg += UpgradeItem[0] * 2;
        P.Dmg += UpgradeItem[1];
        P.Dmg += UpgradeItem[2];
        P.Dmg += UpgradeItem[3];
        P.Dmg += UpgradeItem[4];
        P.AS += 0.1f * UpgradeItem[1];
        P.AS += 0.1f * UpgradeItem[5] * 2;
        P.AS += 0.1f * UpgradeItem[6];
        P.AS += 0.1f * UpgradeItem[7];
        P.AS += 0.1f * UpgradeItem[8];
        P.vamp += 3 * UpgradeItem[9] * 2;
        P.vamp += 3 * UpgradeItem[10];
        P.vamp += 3 * UpgradeItem[11];
        P.vamp +=3 * UpgradeItem[2];
        P.vamp += 3 * UpgradeItem[6];
        P.crit+= UpgradeItem[12] * 20;
        P.crit+= UpgradeItem[3] * 10;
        P.crit += UpgradeItem[7] * 10;
        P.crit += UpgradeItem[13] * 10;
        P.crit += UpgradeItem[10] * 10;
        P.maxMp += UpgradeItem[14] * 40;
        P.maxMp += UpgradeItem[4] * 20;
        P.maxMp += UpgradeItem[8] * 20;
        P.maxMp += UpgradeItem[11] * 20;
        P.maxMp += UpgradeItem[13] * 20;
        takeOne.SetActive(false);
        gameObject.transform.position = new Vector3(-7.5f, 4, 0);
        BM.ItemSelectTime = false;
     
        BM.newStage();
    }
    private void Start()
    {
        P = GameObject.FindWithTag("Player").GetComponent<Player>();
        Anim = GetComponent<Animator>();
    }
    public void makeSomething()
    {
        Anim.SetTrigger("Make");
        Invoke("MakeEnd", 2f);
    }
    public void MakeEnd()
    {
        Make();
        Anim.SetTrigger("MakeEnd");
       
    }
    public void Make()
    {

        StartCoroutine("MakeNormalItem");

            

    }
    private void Update()
    {
        stat[0].text = "" + P.Dmg;
        stat[1].text = "" + P.vamp;
        stat[2].text = "" + P.AS;
        stat[3].text = "" + P.crit;
        stat[4].text = "" + P.maxMp;
    }
    IEnumerator MakeNormalItem()
    {
        int rand = Random.Range(0, 5);
        int counter = 0;
        int i = 0;
        takeOne.SetActive(true);
        while (counter < 5)
        {if (counter == rand)
            {
                counter++;
                yield return new WaitForSeconds(0.01f); }
            else
            {
                if (i < 2)
                {
                    GameObject G = Instantiate(NormalItemPrefebs[counter], transform.position, transform.rotation, GameObject.Find("DmgCanvas").transform);
                    if (i == 0)
                        G.GetComponent<Item>().v = new Vector2(-400 / 160, 250 / 192);
                    if (i == 1)
                        G.GetComponent<Item>().v = new Vector2(400 / 160, 250 / 192);

                }  
                counter++;
                i++;
                yield return new WaitForSeconds(0.5f);
            }
        }
        
    }
    public void UpItem()
    {
        if (curNormalItem.Count > 0)
        {
            ItemCanvas[0].SetActive(false);
            ItemCanvas[1].SetActive(true);
            for (int i = 0; i < curNormalItem.Count; i++)
            {
                curItemButton[i].SetActive(true);
                curItemButton[i].GetComponent<Image>().sprite = sprties[curNormalItem[i]];
            }
        }
        else
        {
            NoUpgrade.SetActive(true);
            Invoke("NOUPOFF", 2f);

        }
    }
    void NOUPOFF()
    {
        NoUpgrade.SetActive(false);
    }
    int makeType;
    public void MakeUpGradeItem(int k)
    {
        makeType = curNormalItem[k];
        
        for (int i = 0; i < curNormalItem.Count; i++)
        {
            curItemButton[i].SetActive(false);
           
        }
        for(int i = 0; i < curItem.Count; i++)
        {
            if (curItem[i] == makeType)
            {
                curItem.RemoveAt(i);
                break;
            }
        }
        NormalItem[curNormalItem[k]]--;
        curNormalItem.RemoveAt(k);
        Anim.SetTrigger("Make");
        Invoke("MakeEnd2", 2f);
    }
    void setItemList()
    {
        for(int i = 0; i < curItem.Count; i++)
        {
            ItemObj[i].GetComponent<Image>().sprite = itemImages[curItem[i]];
            ItemObj[i].SetActive(true);
        }
        for(int i = curItem.Count; i < 9; i++)
        {
            ItemObj[i].SetActive(false);
        }
    }
    [SerializeField] Sprite[] itemImages;
    [SerializeField] GameObject[] ItemObj;
    public List<int> curItem = new List<int>();
    public void MakeEnd2()
    {
        int rand = Random.Range(0, 2);
        if (rand < 1)
        {
            Anim.SetTrigger("MakeEnd");
            SetCharacterStat();
        }
        else
        {
            Make2();
            Anim.SetTrigger("MakeEnd");
        }

    }
    public void Make2()
    {
        StartCoroutine("MakeUpgradeItem");
    }
    IEnumerator MakeUpgradeItem()
    {
        int rand = Random.Range(0, 5);
        int counter = 0;
        int i = 0;
        while (counter < 5)
        {
            if (counter == rand)
            {
                counter++;
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                GameObject G = null;
                if (i < 2) { 
                if (makeType == 0)
                    G = Instantiate(ItemPrefebs0[counter], transform.position, transform.rotation, GameObject.Find("DmgCanvas").transform);
                if (makeType == 1)
                    G = Instantiate(ItemPrefebs1[counter], transform.position, transform.rotation, GameObject.Find("DmgCanvas").transform);
                if (makeType == 2)
                    G = Instantiate(ItemPrefebs2[counter], transform.position, transform.rotation, GameObject.Find("DmgCanvas").transform);
                if (makeType == 3)
                    G = Instantiate(ItemPrefebs3[counter], transform.position, transform.rotation, GameObject.Find("DmgCanvas").transform);
                if (makeType == 4)
                    G = Instantiate(ItemPrefebs4[counter], transform.position, transform.rotation, GameObject.Find("DmgCanvas").transform);
                if (i == 0)
                    G.GetComponent<Item>().v = new Vector2(-400 / 160, 250 / 192);

                if (i == 1)
                    G.GetComponent<Item>().v = new Vector2(400 / 160, 250 / 192);
            }
                counter++;
                i++;
                yield return new WaitForSeconds(0.5f);
            }
        }
        takeOne.SetActive(true);
    }
}
