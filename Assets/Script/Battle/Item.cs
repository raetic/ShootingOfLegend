using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  
    [SerializeField] int i;
    [SerializeField] int type;
    [SerializeField] GameObject text;
    public bool isUI;
    
    bool isSwitch;
    public Vector3 v;
    void Start()
    {
        if (!isUI)
        {
            text.transform.parent = GameObject.Find("DmgCanvas").transform;
            text.transform.position = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != v&&!isUI)
        {
            transform.position = Vector3.MoveTowards(transform.position, v, 1f * Time.deltaTime);
        }
    }

    public void GetNormalItem(int i)
    {
        Ornn o = GameObject.Find("Ornn").GetComponent<Ornn>();
        o.NormalItem[i]++;
        o.curNormalItem.Add(i);
        o.curItem.Add(i);
        o.SetCharacterStat();
    }
    public void GetUpgradeItem(int i)
    {
        int rand = Random.Range(0, 100);
        Ornn o = GameObject.Find("Ornn").GetComponent<Ornn>();
      
            o.UpgradeItem[i]++;
            o.curItem.Add(i + 5);
        
        o.SetCharacterStat();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player"&&!isUI)
        {
           
            if (type == 0)
            {
                GetNormalItem(i);
                Destroy(gameObject);
            }
            if (type == 1)
            {
                GetUpgradeItem(i);
                Destroy(gameObject);
            }
        }
    }
}
