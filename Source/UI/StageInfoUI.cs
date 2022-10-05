using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class StageInfoUI : MonoBehaviour
{
    public GameObject StageInfo;
   
    [SerializeField] private GameObject CountDown; // text
    [SerializeField] private GameObject Tutorial; // text
    [SerializeField] private GameObject tutorialUI; // text
    [SerializeField] private GameObject PauseWindow; // text
    [SerializeField] private GameObject TimeUi; // text


    [Header("Stage Infomation")]
    [SerializeField] private TextMeshProUGUI stage_num;
    [SerializeField] private TextMeshProUGUI stage_attack;
    [SerializeField] private TextMeshProUGUI stage_enemy;
    [SerializeField] private float DelayTime;

    [Header("Clear")]
    [SerializeField] private GameObject Clear;
    [SerializeField] private TextMeshProUGUI record;
    //

    // == timer
    UnscaledTimer StageInfoTimer = new UnscaledTimer();
    UnscaledTimer ClearTimer = new UnscaledTimer();

    bool bIsPlaying = true;
    public List<Stagetext> stageText;

    public GameCenter gameCenter;


    private void Awake()
    {
        stageText = new List<Stagetext>();
        ReadTextFile();
        SetStageInfo(0);
    }

    // 1-1 start : tutorial -> stageInfo;
    // ±× ¿Ü : StageClear -> StageInfo -> countdown

    void Update()
    {
        SetTimeScale();
        UpdateTimer();
        EndClearUI();
        EndStageInfoUI();
    }

    void UpdateTimer()
    {
        if (StageInfo.activeSelf)
            StageInfoTimer.UpdateTimer();

        if (Clear.activeSelf)
            ClearTimer.UpdateTimer();
    }

    void EndClearUI()
    {
        if (ClearTimer.TimeOut())
        {
            Clear.SetActive(false);
            if (gameCenter.Stage >= 4 && gameCenter.Stage <= 15) ShowStageInfo();
            else if (gameCenter.Stage != 0 && gameCenter.Stage < 4)
            {
                tutorialUI.GetComponent<TutorialUI>().tutorialPanel.SetActive(true);
                tutorialUI.GetComponent<TutorialUI>().ShowTutorial();
            }

            ClearTimer.SetTimeOut(false);
        }
    }
    void EndStageInfoUI()
    {
        if (StageInfoTimer.TimeOut())
        {
            StageInfo.SetActive(false);
            CountDown.SetActive(true);
            CountDown.GetComponent<Countdown>().StartTimer();
            StageInfoTimer.SetTimeOut(false);
        }
    }
    void SetTimeScale()
    {
        if (bIsPlaying) Time.timeScale = 1;
        else Time.timeScale = 0;

        if (StageInfo.activeSelf || Clear.activeSelf || Tutorial.activeSelf || CountDown.activeSelf || PauseWindow.activeSelf) bIsPlaying = false;
        else if (!StageInfo.activeSelf && !Clear.activeSelf && !Tutorial.activeSelf && !CountDown.activeSelf && !PauseWindow.activeSelf) bIsPlaying = true;
    }

    void ReadTextFile()
    {
        stageText.Clear();

        TextAsset textFile = Resources.Load("StageInfo") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine(); 

            if (line == null) break;

            Stagetext stageData = new Stagetext();
            stageData.stage_num = line.Split(',')[0];
            stageData.stage_attack = line.Split(',')[1];
            stageData.stage_enemy = line.Split(',')[2];
            stageText.Add(stageData);
        }
        stringReader.Close();
    }

    private int PrevMin;
    private int PrevSec;
    private string PrevRec;

    public void SavePrevTimer(string rec)
    {
        PrevRec = rec;
    }

    public void ShowClearInfo()
    {
        Clear.SetActive(true);
        record.text = PrevRec;
        ClearTimer.StartTimer(DelayTime);
    }

    public void ShowStageInfo()
    {
        StageInfo.SetActive(true);
        StageInfoTimer.StartTimer(DelayTime);
       // Debug.Log("ShowStageInfo_DelayTime");
    }

    public void SetStageInfo(int index)
    {
        stage_num.text = "Stage " + stageText[index].stage_num;
        stage_attack.text = stageText[index].stage_attack;
        stage_enemy.text = stageText[index].stage_enemy;
    }

    public void ShowClear()
    {
        Clear.SetActive(true);
    }


    
}

// ================= timer ===================
public class UnscaledTimer
 {
    private float timer = 0;
    private bool timeOut = false;
    public bool TimeOut() { return timeOut; }
    public void SetTimeOut(bool b) {  timeOut = b; }
    private bool bTimer = false;
    private float DelayTime;


    public void StartTimer(float delayTime)
    {
        DelayTime = delayTime;
        timer = 0; //reset
        bTimer = true;
    }

    public void UpdateTimer()
    {
        if (bTimer) 
            timer += Time.unscaledDeltaTime;

        if (timer >= DelayTime) timeOut = true;
    }
    public float GetTimer()
    { return (float)timer; } 
};
