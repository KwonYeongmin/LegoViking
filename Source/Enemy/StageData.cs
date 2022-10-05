using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    public StageData() { }
    public int Stage;
    public EnemyType Type;
    public EnemyColorType ColorType;
    public int ItemIndex;
    public float[] VikingSpeed = new float[3];
}
