using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [HideInInspector]public TMP_Text TimeTEXT;
    Timer timer = new Timer();
    // public InterfaceTimer interfaceTimer;
    public SceneManagement SceneManager;
    public int Min { get; private set; }
    public int Sec { get; private set; }
    float Speed = 5;
    public GameCenter gameCenter;
    public Viking viking;
    private void Awake()
    {
        Min = 0;
        Sec = 0;
    }
    private void Start()
    {
       // Speed = stageManager.viking.speed;
    }

    void Update()
    {
       if (Min >= 3)
            SceneManager.ChangeScene("GameOver");

        timer.UpdateTimer();
        UpdateUI();
        SetSpeed();
    }

    public void StartTimer()
    {
        timer.ResetTimer();
        timer.StartTimer();
    }


     //
    string Rec;
    void SaveRec(string rec)
    {
        Rec = rec;
    }

    public string GetRec()
    {
        return Rec;
    }
     //

    private void UpdateUI()
    {
        Min = (int)(timer.GetTimer() / 60 % 60);
        Sec = (int)(timer.GetTimer() % 60);

        if (Min < 10 && Sec < 10)
            SaveRec(string.Format("0{0} : 0{1}", Min, Sec)); 
        else if (Min < 10 && Sec>=10)
            SaveRec(string.Format("0{0} : {1}", Min,Sec));

        TimeTEXT.text = GetRec();
    }
    private bool bSettingFinished = true;
   
    void SetSpeed()  //StageManager.Instance.Stage ¿Å±â±â
    {
        switch (gameCenter.Stage)
        {
            case 0: {UpdateSpeed(1.1f,1.2f,1.0f);} break; //1-1
            case 1:{ UpdateSpeed( 1.3f, 1.2f, 1.1f);} break;//1-2
            case 2: {  UpdateSpeed(1.3f, 1.4f, 1.2f);} break;//1-3
            case 3: { UpdateSpeed(1.6f, 1.6f, 1.4f); } break;//1-4
            case 4: { UpdateSpeed(1.2f, 1.3f, 1.1f); } break;//2-1
            case 5: { UpdateSpeed(1.4f, 1.3f, 1.2f); } break;//2-2
            case 6: { UpdateSpeed(1.4f, 1.5f, 1.3f); } break;//2-3
            case 7: { UpdateSpeed(1.7f, 1.7f, 1.5f); } break;//2-4
            case 8: { UpdateSpeed(1.3f, 1.4f, 1.2f); } break; //3-1
            case 9: { UpdateSpeed(1.5f, 1.4f, 1.3f); } break;//3-2
            case 10: { UpdateSpeed(1.5f, 1.6f, 1.4f); } break;//3-3
            case 11: { UpdateSpeed(1.8f, 1.8f, 1.6f); } break;//3-4
            case 12: { UpdateSpeed(1.4f, 1.5f, 1.3f); } break;//4-1
            case 13: { UpdateSpeed(1.6f, 1.5f, 1.4f); } break; //4-2
            case 14: { UpdateSpeed(1.6f, 1.7f, 1.5f); } break;//4-3
            case 15: { UpdateSpeed(1.9f, 1.9f, 1.7f); } break;//4-4
        }
    }

    private void UpdateSpeed(float value1, float value2, float value3)
    {
        if (Min >= 1 && Min < 2) { viking.SetSpeed(value1); }
        else if (Min >= 2) { viking.SetSpeed(value2); }
        else { viking.SetSpeed (value3); }
    }

}
