using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHUD : MonoBehaviour
{
    private int DefaultHP;
 
    // HPSlider
    public Slider HPSlider;
    public Image HPImg;
    public Sprite[] HPSprites;
    //HPText
    public TMP_Text HP_TXT;
    
    //ProfileImage
    //public Sprite[] InfoSprites; //3°³
    public Image InfoImage;
    public GameObject deadImg;
    //ProfileStageInfo
    public TMP_Text StageInfo_TXT;


    int Stage;
    EnemyType Type = EnemyType.Enemy_Missile;
    Enemy enemySC;
    bool bEndSetting=false;

    

    public void Initialized(Enemy enemy)
    {
        enemySC = enemy;

        Debug.Log("Stage : " + EnemyManager.Instance.Stage);

        if (EnemyManager.Instance.Stage % 4 == 3)
        {
            if (enemySC.ColorType == EnemyColorType.GREY) { HPImg.color = new Color32(255, 255, 255, 255); }
            else if (enemySC.ColorType == EnemyColorType.BLUE)
            {
                this.gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(-572.0f, 355.0f, 0);
                HPImg.color = new Color32(255, 255, 255, 255);
            }
            else if (enemySC.ColorType == EnemyColorType.YELLOW)
            {
                this.gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition
                  = new Vector3(-572.0f, 245.0f, 0);
                HPImg.color = new Color32(255, 131, 81, 255);
            }
        }


        // HP
        DefaultHP = enemySC.DefaultHP;
      
        //Type
        Type = enemySC.Type;

        // hp image
        HPImg.sprite = HPSprites[(int)enemySC.ColorType];
    
        bEndSetting = true;

        SetProfile();
    }

    void Update()
    {
        if(bEndSetting)  UpdateHP();
    }

    private void UpdateHP()
    {
        int hp = enemySC.HP;
        HPSlider.value = (float)hp / (float)DefaultHP;
        HP_TXT.text = string.Format("{0}{1}{2}", hp.ToString(), " / ", DefaultHP.ToString());

        if (hp <= 0) deadImg.SetActive(true);
    }


    private void SetProfile()
    {
        InfoImage.SetNativeSize();

        string type= "Missile";

        switch (Type)
        {
            case EnemyType.Enemy_Missile: { type = "Missile"; } break;
            case EnemyType.Enemy_Arrow: { type = "Arrow"; } break;
            case EnemyType.Enemy_Dagger: { type = "Dagger"; } break;
            case EnemyType.Enemy_Boss: { type = "Boss"; } break;
        }

        StageInfo_TXT.text= string.Format("Level {0}.{1}" , (int)enemySC.ColorType+1, type);
    }
}
