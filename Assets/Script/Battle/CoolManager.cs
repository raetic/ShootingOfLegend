using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolManager : MonoBehaviour
{
    [SerializeField]Image image;
     Button butt;
    Image SkillImage;
    public float coolTime;
    public bool isClicked;
    public float leftTime;
    public float speed;
    public Player player;
    int[] needMp = new int[4];
    [SerializeField] int SkillNumber;
    
    private void Start()
    {
       
    }
    public void coolManagerStart()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        SkillImage = GetComponent<Image>();
        SkillImage.sprite = player.skillImages[SkillNumber];
        transform.GetChild(0).GetComponent<Image>().sprite = SkillImage.sprite;
        coolTime = player.skillCoolTime[SkillNumber];
        leftTime = player.skillCoolTime[SkillNumber];
        needMp = player.SkillMp;
        butt = GetComponent<Button>();
    }
    public void rCooldown()
    {
       
        leftTime *= 0.8f;
      
    }
    public void CoolClear()
    {
        leftTime = 0.1f;
      
    }
    private void Update()
    {
        if (isClicked)
            if (leftTime > 0)
            {
                leftTime -= Time.deltaTime * speed;
                if (leftTime < 0)
                {
                    leftTime = 0;
                    if (butt)
                        butt.enabled = true;
                    isClicked = true;
                }

                float ratio = leftTime / coolTime;
                image.fillAmount = ratio;

            }
            else
            {
                isClicked = false;
            }
        if (!isClicked)
        {
           // Debug.Log(needMp[SkillNumber]);

            if (needMp[SkillNumber] > player.Mp||player.isStun)
            {
                butt.enabled = false;
                SkillImage.color = new Color(0.2f, 0.2f, 0.2f);
            }
            else
            {
                butt.enabled = true;
                SkillImage.color = new Color(1,1,1);
            }           
        }
    }

    public void StartCoolTime()
    {
        leftTime = coolTime;
        isClicked = true;
        if (butt)
            butt.enabled = false; // 버튼 기능을 해지함.
    }
    public void skill1()
    {
        player.skill[1] = true;
        player.Mp -= needMp[1];
    }
    public void skill2()
    {
        player.skill[2] = true;
        player.Mp -= needMp[2];
    }
        public void skill3()
    {
        player.skill[3] = true;
        player.Mp -= needMp[3];
    }
}

