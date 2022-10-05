using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;

public class ReadTextFile : MonoBehaviour
{
    public List<StageData> stageSetting;

    private void Awake()
    {
        ReadStageSetting();
    }

    private void ReadStageSetting()
    {
        int count = 0;

        TextAsset textFile = Resources.Load("StageSetting") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null) break;

            StageData stageData = new StageData();
            count++;

            if (count == 1) continue;

            stageData.Stage = int.Parse(line.Split(',')[0]);
            stageData.Type = (EnemyType)int.Parse(line.Split(',')[1]);
            stageData.ColorType = (EnemyColorType)int.Parse(line.Split(',')[2]);
            stageData.ItemIndex = int.Parse(line.Split(',')[3]);
            for (int i = 0; i < 3; i++) stageData.VikingSpeed[i] = (float.Parse(line.Split(',')[i + 4]));
            stageSetting.Add(stageData);
        }
        stringReader.Close();
    }

    public StageData GetStageSettingInfo(int index)
    {
        return stageSetting[index];
    }

    public int GetMaxStage()
    {
        return (stageSetting.Count-1);
    }
}
