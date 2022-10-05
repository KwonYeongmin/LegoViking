using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    protected EnemyColorType colortype = EnemyColorType.GREY;

    public void SetColorType(EnemyColorType colorType) { colortype = colorType; }
}
