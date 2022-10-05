using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject[] tutorialUI1;
    public GameObject tutorialUI2;
    public GameObject tutorialUI3;
    public GameObject tutorialUI4;
    public GameObject tutorialPanel;
    public GameObject StageInfo;
    public GameCenter gameCenter;

    int index = 0;
    bool bBtnDown = false;

    public void Start()
    {
        index = 0;
    }


    public void TutorialNextBtnDown()
    {
        bBtnDown = true;
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_button);
    }

    public void ShowTutorial()
    {
        switch (gameCenter.Stage)
        {
            case 0: { tutorialUI1[0].SetActive(true); } break;
            case 1: { tutorialUI2.SetActive(true); } break;
            case 2: { tutorialUI3.SetActive(true); } break;
            case 3: { tutorialUI4.SetActive(true); } break;
        }
    }

    private void Update()
    {
        

          if (bBtnDown)
        {
            if (gameCenter.Stage == 0)
            {
                if (index < 2)
                {
                    tutorialUI1[index].SetActive(false);
                    tutorialUI1[index + 1].SetActive(true);
                    index++;
                }
                else
                {
                    tutorialUI1[index].SetActive(false);
                    tutorialPanel.gameObject.SetActive(false);
                    StageInfo.GetComponent<StageInfoUI>().ShowStageInfo();
                }
                bBtnDown = false;
            }

            else  if(gameCenter.Stage == 1)
            {
                tutorialUI2.SetActive(false);
                tutorialPanel.gameObject.SetActive(false);
                StageInfo.GetComponent<StageInfoUI>().ShowStageInfo();

                bBtnDown = false;
            }

            else if (gameCenter.Stage == 2)
            {
                tutorialUI3.SetActive(false);
                tutorialPanel.gameObject.SetActive(false);
                StageInfo.GetComponent<StageInfoUI>().ShowStageInfo();

                bBtnDown = false;
            }

            else if (gameCenter.Stage == 3)
            {
                tutorialUI4.SetActive(false);
                tutorialPanel.gameObject.SetActive(false);
                StageInfo.GetComponent<StageInfoUI>().ShowStageInfo();

                bBtnDown = false;
            }
        }
     
       // if (tutorialPanel.activeSelf) Time.timeScale = 0.0f;
       // else Time.timeScale = 1;
    }
       
    
}
