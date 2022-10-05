using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  enum EnemyColorType{GREY, BLUE, YELLOW};
public enum EnemyType { Enemy_Missile, Enemy_Arrow, Enemy_Dagger, Enemy_Boss };

public class Enemy : MonoBehaviour
{
    public int DefaultHP { get; private set; }
    public int HP { get; private set; }

    public GameObject HUDInstance;
    private EnemyHUD HUD;

    public GameObject AttackSpawnerInstance;
    private EnemyAttackSpawner AttackSpawner;

    public EnemyType Type { get; private set; }
    public EnemyColorType ColorType { get; private set; }

    public Sprite EnemtProfileImg;

    private int MoveIndex=0;

    void Start()
    {
        //HUD 
        HUD = Instantiate(HUDInstance, new Vector2(960, 640), Quaternion.identity).GetComponent<EnemyHUD>();
        EnemyManager.Instance.AddEnemyHUD(HUD);
        HUD.Initialized(this);
        HUD.InfoImage.sprite = EnemtProfileImg;

        // 
        AttackSpawner = Instantiate(AttackSpawnerInstance, new Vector3(0,23,0), this.transform.rotation).GetComponent<EnemyAttackSpawner>();
       if(EnemyManager.Instance.Stage % 4 == 3) AttackSpawner.Initialized(Type,ColorType,3f);
       else AttackSpawner.Initialized(Type, ColorType, 1f);
    }


    private void Update()
    {
        if (bSetEnd) Dead();

        time += Time.deltaTime * (speed);


        switch (EnemyManager.Instance.Stage % 4)
        {
            case 1: { Move(0); } break;
            case 2: { Move(1); } break;
            case 3: { Move(1); } break; 
        }
    }

    private float time = 0;
    public float speed =1.0f;
    private float HorizontalValue = 10.0f;
    private float VerticalValue = 3.0f;

  

    private void Move(int DirectionIndex)
    {
        switch (DirectionIndex)
        {
            case 0:
                transform.position = new Vector3(Mathf.Sin(time) * HorizontalValue, this.transform.position.y, this.transform.position.z);
                break;
            case 1:
                transform.position = new Vector3(this.transform.position.x, Mathf.Sin(time) * VerticalValue + 9, this.transform.position.z);
                break;
        }

    }

    public void initialized(EnemyType type,int colorType)
    {
        Type = type;
        ColorType = (EnemyColorType)colorType;

        SetHP();
        HP= DefaultHP;
    }

    private bool bSetEnd=false;

    void SetHP()
    {
        ReadMonsterData monsterHPData = new ReadMonsterData();
        DefaultHP = monsterHPData.GetHPData((int)Type, (int)ColorType);
        
        bSetEnd = true;
    }


    public void TakeDamage(int value)
    {
        HP = (HP - value) > 0 ? HP - value : 0;
        SoundManager.Instance.PlaySE(SoundList.Sound_monster_hit, transform.position);
    }

    public void Dead()
    {
        if (HP > 0) return;
        
        EnemyManager.Instance.RemoveEnemy(this);
        EnemyManager.Instance.NextStage();

        SoundManager.Instance.PlaySE(SoundList.Sound_monster_death, transform.position);

        Destroy(AttackSpawner.gameObject);
        Destroy(this.gameObject);
    }
}
