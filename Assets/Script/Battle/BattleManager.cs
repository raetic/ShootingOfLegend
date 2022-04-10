using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject hitI;
    [SerializeField] TextMeshProUGUI WarnText;
    [SerializeField] TextMeshProUGUI Timer;
   public int stage;
    [SerializeField] GameObject ornn;
    [SerializeField] GameObject ItemPopUp;
    public bool ItemSelectTime;
    public Ornn or;
    Player player;
    bool[] IsMatch = new bool[16];
    [SerializeField] GameObject[] boss;
    GameObject pobj;
    [SerializeField] GameObject reviveButton;
    int battlecount;
    [SerializeField] Text stageT;
    Data data;
    [SerializeField] GameObject[] players;
    void Awake()
    {
        string path = Path.Combine(Application.persistentDataPath, "Data.json");
        if (File.Exists(path))
        {
            string battleData = File.ReadAllText(path);
            data = JsonUtility.FromJson<Data>(battleData);

        }
        
        or = ornn.GetComponent<Ornn>();
        GameObject playerobj = Instantiate(players[data.curCharacter], new Vector3(0, -3, 0), transform.rotation);
        player =  GameObject.FindWithTag("Player").GetComponent<Player>();
        pobj = GameObject.FindWithTag("Player");
        stage = 1;
        stageT.text ="stage:" + stage;
        Summon();
       GameObject[] skills= GameObject.FindGameObjectsWithTag("Skill");
        for(int i = 0; i < 3; i++)
        {
            skills[i].GetComponent<CoolManager>().coolManagerStart();
        }
    }
    public void Summon()
    {
        GameObject B = Instantiate(boss[0], new Vector2(0, 3.5f), transform.rotation);
    }
    public void reviveOn()
    {
        reviveButton.SetActive(true);

    }
    public void revive()
    {
        pobj.SetActive(true);
        pobj.transform.position = new Vector3(0, -4, -5);
        player.Hp = player.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goHome()
    {
        SceneManager.LoadScene(0);
    }
    public void nextStage()
    {
        GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] EnemyArrows = GameObject.FindGameObjectsWithTag("EnemyArrow");
        for (int i = Enemys.Length - 1; i >= 0; i--)
        {
           
            if (Enemys[i] != null) Destroy(Enemys[i]);
        }
        for (int i = EnemyArrows.Length - 1; i >= 0; i--)
        {

            if (EnemyArrows[i] != null) Destroy(EnemyArrows[i]);
        }
        GameObject[] towers = GameObject.FindGameObjectsWithTag("tower");
        for (int i = towers.Length - 1; i >= 0; i--)
        {

            if (towers[i] != null) Destroy(towers[i]);
        }
        if (or.UpgradeItem[0] > 0) or.NormalItem[0]+=1;
        if (or.UpgradeItem[9] > 0) player.Hp += player.maxHp * 0.2f;
        stage++;
        ItemSelectTime = true;
        ornn.transform.position = new Vector3(0, 3.5f, 0);
        ItemPopUp.SetActive(true);
        player.nextStage();
    }
    public void newStage()
    {
        stageT.text = "stage:" + stage;
        if (or.UpgradeItem[3]>0)
        {
            player.CriDmg = 2.5f;
        }
        if (or.UpgradeItem[12]>0)
        {
            player.Runan.SetActive(true);
        }
        player.ShiledOn = false;
        player.redShiledAmount = 0;
        StartCoroutine("startStage");
    }
    IEnumerator startStage()
    {   
        int c = 5;
        while (c > 0)
        {
            GameObject[] Items = GameObject.FindGameObjectsWithTag("Item");
            for (int i = Items.Length - 1; i >= 0; i--)
            {
                if (Items[i] != null)
                {
                    Destroy(Items[i]);
                }
            }
            Timer.text = "" + c;
            yield return new WaitForSeconds(1);
            c--;
        }
        if (battlecount > boss.Length - 2)
        {
            for(int i = 1; i < boss.Length; i++)
            {
                IsMatch[i] = false;
            }
            battlecount = 0;
        }
        int rand = Random.Range(1, boss.Length);
        while (IsMatch[rand])
        {
            rand = Random.Range(1, boss.Length);
        }
        battlecount++;
        GameObject[] Itemss = GameObject.FindGameObjectsWithTag("Item");
        for (int i = Itemss.Length - 1; i >= 0; i--)
        {
            if (Itemss[i] != null)
            {
                Destroy(Itemss[i]);
            }
        }
        IsMatch[rand] = true;
        GameObject B = Instantiate(boss[rand], new Vector2(0, 3.5f), transform.rotation);
        Timer.text = "";
    }
    public void Hit()
    {
        hitI.SetActive(true);
        Invoke("HitOff", 0.5f);
    }
    void HitOff()
    {
        hitI.SetActive(false);
    }
    public void Warn()
    {
        StartCoroutine("StartWarn");
    }
    IEnumerator StartWarn()
    {
        int c = 0;
        while (c < 7)
        {
            
            if (c == 0)
            {
                WarnText.text = "W";
            }
            if (c == 1)
            {
                WarnText.text = "WA";
            }
            if (c == 2)
            {
                WarnText.text = "WAR";
            }
            if (c == 3)
            {
                WarnText.text = "WARN";
            }
            if (c == 4)
            {
                WarnText.text = "";
            }
            if (c == 5)
            {
                WarnText.text = "WARN";
            }
            if (c == 6)
            {
                WarnText.text = "";
            }
            c++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
