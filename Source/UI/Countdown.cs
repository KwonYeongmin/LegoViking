using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    [HideInInspector]
    public TMP_Text TimeTEXT;

    private float timer;
    private float MaxTime = 3f;
    [HideInInspector]public bool bIsEnded;

    void Update()
    {
       // if (bIsEnded)
       //     return;

       // Check_Timer();

         if (bIsEnded) this.gameObject.SetActive(false);
    }

    private void Check_Timer()
    {
        if (0 < timer)
        {
            timer -= Time.unscaledDeltaTime;
            TimeTEXT.text = ((int)timer + 1).ToString();
        }
        else if (!bIsEnded)
        {
            EndTimer();
            this.gameObject.SetActive(false); // 3,2,1,0 끝나면 이 게임 오브젝트 안보이기
        }
    }

    private void EndTimer()
    {
        timer = 0;
        TimeTEXT.text = ((int)timer).ToString();
        bIsEnded = true;
        // this.GetComponent<AudioSource>().Stop();
        //  StageManager.Instance.ChangeStage();
    }

    public void StartTimer() //여기서 시작해주면 됨
    {
        /*
        timer = MaxTime;
        TimeTEXT.text = ((int)timer).ToString();
        bIsEnded = false;
       */
        bIsEnded = false;
       StartCoroutine( func());
    }


    IEnumerator func()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_countdown);
        for (int i = 0; i < 4; i++)
        {
            string str = null;
            if (i != 3) str = string.Format("{0}", (3 - i).ToString());
            else str = string.Format("Start");

            TimeTEXT.text = str;
          
            
           yield return new WaitForSecondsRealtime(0.7f);
        }
        bIsEnded = true;


    }
}




