using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameCenter : MonoBehaviour
{
    #region class
    // ����
    [SerializeField]    private EnemySpawner enemySpawner;
    [SerializeField]    private EnemyAttackSpawner attackSpawner;
    [SerializeField]    private ItemSpawnerSelector itemSpawner;
    // UI
    [SerializeField] private TimerUI Timer;
    [SerializeField] private StageInfoUI StageInfo;
    [SerializeField] private TutorialUI tutorial;

    [SerializeField]    private SceneManagement sceneManager;
 
    [SerializeField]    public Viking viking;
    public  ReadTextFile StageSettingData;
    #endregion

    public int Stage;
    public int MaxStage;
    public EnemyType Type = EnemyType.Enemy_Missile;
    public EnemyColorType ColorType = EnemyColorType.GREY;

   


    private void Start()
    {
        ReadTextFile StageSettingData = new ReadTextFile();

        EnemyManager.Instance.gameObject.SetActive(true);
        Init();

        Stage = 0;
        EnemyManager.Instance.SetStage(Stage);


        ResetStage();
        SetStageValue();
        CreateStageGameobject();

    }


    private void Init()
    {
        MaxStage = StageSettingData.GetMaxStage();
    }


    public void NextStage()
    {
        if (!EnemyManager.Instance.bIsEnemyClear) return;
        Stage++;

        EnemyManager.Instance.SetStage(Stage);

        ResetStage();
        SetStageValue();
        CreateStageGameobject();
    }

    // csv���� �޾ƿ� �����ͷ� ���������� �´� UI, Enemy,Item, Viking���� �������ִ� �޼����̴�.
    // Setting
    private void SetStageValue()
    {
        if (Stage > MaxStage) sceneManager.ChangeScene("GameClear");

        // Enemy
        Type = StageSettingData.GetStageSettingInfo(Stage).Type;
        ColorType = StageSettingData.GetStageSettingInfo(Stage).ColorType;
        // Item
        itemSpawner.SetItemIndex(StageSettingData.GetStageSettingInfo(Stage).ItemIndex);
        // Viking
        viking.SetSpeed(StageSettingData.GetStageSettingInfo(Stage).VikingSpeed[0]);
       
        // UI
        StageInfo.SetStageInfo(Stage);
        Timer.StartTimer();
        //BFM
        SetBGM();
    }

    // ���������� �´� UI, Enemy�� �������ִ� �޼����̴�.
    // Spawner
    private void CreateStageGameobject()
    {
        //UI
        if (Stage == 0)
        {
            tutorial.tutorialPanel.SetActive(true);
            tutorial.ShowTutorial();
        }
        else if (Stage != (MaxStage + 1))
            StageInfo.ShowClearInfo();


        //EnemySpawner
        if (Stage % 4 != 3)
            enemySpawner.InstantiateEnemy(Type, ColorType);
        else
            enemySpawner.InstantiateEnemies(Type);
    }

    // ���� �������� ���� ���� ������ �������ִ� �޼����̴�. 
    private void ResetStage()
    {
        viking.Reset();
        EnemyManager.Instance.Reset();
    }



    #region SettingBGM

    // ���������� �°� ��������� �������ִ� �޼����̴�.
    private void SetBGM()
    {
        switch (Stage)
        {
            case 0: SoundManager.Instance.PlayBGM(SoundList.Stage1); break;
            case 3: SoundManager.Instance.PlayBGM(SoundList.Stage1_Boss); break;
            case 4: SoundManager.Instance.PlayBGM(SoundList.Stage2); break;
            case 7: SoundManager.Instance.PlayBGM(SoundList.Stage2_Boss); break;
            case 8: SoundManager.Instance.PlayBGM(SoundList.Stage3); break;
            case 11: SoundManager.Instance.PlayBGM(SoundList.Stage3_Boss); break;
            case 12: SoundManager.Instance.PlayBGM(SoundList.Stage4); break;
            case 15: SoundManager.Instance.PlayBGM(SoundList.Stage4); break;
            default: return;
        }
    }
    #endregion


    
}
