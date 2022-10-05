using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 회복, 총알, 스피드업, 폭탄, 무적
    public enum ItemType { Heal = 0, Bullet = 1, Speedup = 2, Grenade = 3, Invincible = 4 };

    [Header("아이템 타입")]
    public ItemType itemType = ItemType.Heal;

    [Header("회복 아이템 변수")]
    [Tooltip("회복 증가 계수")]
    public float heal_Recovery = 20; //현재 체력의 20%만큼 회복
    
    [Header("탄창 아이템 변수")]
    [Tooltip("탄창 증가 개수")]
   public int recovery_bullet = 10;

    [Header("이동속도 증가 아이템 변수")]
    [Tooltip("이동속도 증가 계수")]
    public int buff_speedup =20 ;
    [Tooltip("아이템 사용 시간")]
    public float duration_speedup =5.0f ;
    

    [Header("무적 아이템 변수")]
    [Tooltip("아이템 사용 시간")]
    public float duration_invincible = 5.0f;

    [Header("파괴시간")]
    public float Lifetime = 5.0f;

    private void Start()
    {
         Destroy(this.gameObject, Lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            switch (itemType)
            {
                case ItemType.Heal: { UseHealItem(other); } break;
                case ItemType.Bullet: { UseBulletItem(other); } break;
                case ItemType.Speedup: { UseSpeedupItem(other); } break;
                case ItemType.Grenade: { UseBombItem(other); } break;
                case ItemType.Invincible: { UseInvincibleItem(other); } break;
            }

            Destroy(this.gameObject);
        }
    }

  


    private void UseHealItem(Collider other)
    {
        //Debug.Log("회복 아이템 사용");
        //float hp = other.gameObject.GetComponent<Player>().HP;
        //hp += (heal_Recovery*0.01f);
        //Debug.Log("회복량 : " +  hp);
        //other.gameObject.GetComponent<Player>().AddHP(Mathf.RoundToInt(heal_Recovery));
        other.gameObject.GetComponent<Player>().AddHP((int)heal_Recovery);
    }


    private void UseBulletItem(Collider other)
    {
        //Debug.Log("총알 아이템 사용");
        other.gameObject.GetComponent<Player>().AddAmmo(recovery_bullet);
    }


    private void UseSpeedupItem(Collider other)
    {
        //Debug.Log("스피드업 아이템 사용");
        float speed = other.gameObject.GetComponent<Movement>().defaultMoveSpeed;
        speed += speed * (buff_speedup*0.01f);
        other.gameObject.GetComponent<Player>().AddSpeedUp(speed, duration_speedup);
    }


    private void UseBombItem(Collider other)
    {
       // Debug.Log("수류탄 아이템 사용");
        other.gameObject.GetComponent<Player>().AddGrenade();
    }

    private void UseInvincibleItem(Collider other)
    {
       // Debug.Log("무적 아이템사용");
        other.gameObject.GetComponent<Player>().AddInvincible(duration_invincible);
    }
}

public class Timer
{
    private float timer;
    private bool bIsTimerStop = false;

    public void StartTimer() { bIsTimerStop = false; }
    public void StopTimer() { bIsTimerStop = true; }
    public void ResetTimer() { timer = 0; }
    public float GetTimer() { return timer; }
    public bool GetTimerStopState() { return bIsTimerStop; } // true면 stop, false면 stopX
    public void UpdateTimer()
    {
        if (bIsTimerStop) return;
        timer += Time.deltaTime;
    }
}



/*
   Timer countdownTimer = new Timer();
   [HideInInspector]  public TMP_Text TimeTEXT;

   private void Start()
   {
       StartTimer();
   }

   void StartTimer() 
   {
       countdownTimer.ResetTimer();
       countdownTimer.StartTimer();
   }
   private void FixedUpdate()
   {
       countdownTimer.UpdateTimer();
   }

   void Update()
   {

       int countdown = 3-(int)(countdownTimer.GetTimer());
       TimeTEXT.text = countdown.ToString();


       if (countdownTimer.GetTimer() >= 3)
       {
           this.gameObject.SetActive(false);
           countdownTimer.ResetTimer();
       }
   }
   */
