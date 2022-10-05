using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies_Missile;
    public GameObject[] Enemies_Arrow;
    public GameObject[] Enemies_Dagger;
    public GameObject[] Enemies_Boss;

    private GameObject enemy;
    public Transform transformPoint;

     public EnemyType Type { get; private set; }


    private void CreateEnemy(EnemyType type,GameObject enemyPrefab, int index,bool bIsAlone)
    {
        Vector3 pos = transformPoint.position + new Vector3(10 * (index - 1), 0, 0);

        if (bIsAlone) enemy = Instantiate(enemyPrefab, transformPoint);
        else enemy = Instantiate(enemyPrefab, pos, transformPoint.rotation);
        
        EnemyManager.Instance.AddEnemy(enemy.GetComponent<Enemy>());
      
    }


    public void InstantiateEnemies(EnemyType enemyType)
    {
        for (int i = 0; i < 3; i++)
        {
            
            GameObject enemyObj = GetEnemyPrefab(enemyType, (EnemyColorType)i);
            if (enemyObj == null) return;
            CreateEnemy(enemyType,enemyObj, i,false);
            
            enemy.GetComponent<Enemy>().initialized(enemyType, i);
        }
    }

    private GameObject GetEnemyPrefab(EnemyType enemyType, EnemyColorType enemyColorType)
    {

        int index = (int)enemyColorType;
        GameObject enemyObj;
        switch (enemyType)
        {
            case EnemyType.Enemy_Missile: enemyObj = Enemies_Missile[index]; break;
            case EnemyType.Enemy_Arrow: enemyObj = Enemies_Arrow[index]; break;
            case EnemyType.Enemy_Dagger: enemyObj = Enemies_Dagger[index]; break;
            case EnemyType.Enemy_Boss: enemyObj = Enemies_Boss[index]; break;
            default: return null;
        }
        return enemyObj;
    }
    public void InstantiateEnemy(EnemyType enemyType, EnemyColorType enemyColorType)
    {
        GameObject enemyObj = GetEnemyPrefab(enemyType, enemyColorType);
        if (enemyObj == null) return;
        CreateEnemy(enemyType,enemyObj, (int)enemyColorType,true);
        enemy.GetComponent<Enemy>().initialized(enemyType, (int)enemyColorType);
    }


    /*
    public void InstantiateEnemies_Old()
    {
        

        switch (stageManager.Type)
        {
            case EnemyType.Enemy_Missile:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transformPoint.position.x + 10 * (i - 1), transformPoint.position.y, transformPoint.position.z);
                        enemy = Instantiate(Enemies_Missile[i], pos, transformPoint.rotation);
                        enemy.transform.parent = transformPoint;
                        enemy.GetComponent<Enemy>().initialized(i);
                        stageManager.AddEnemies(enemy);
                    }
                }
                break;
            case EnemyType.Enemy_Arrow:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transformPoint.position.x + 10 * (i - 1), transformPoint.position.y, transformPoint.position.z);
                        enemy = Instantiate(Enemies_Arrow[i], pos, transformPoint.rotation);
                        enemy.transform.parent = transformPoint;
                        enemy.GetComponent<Enemy>().initialized(i);
                        stageManager.AddEnemies(enemy);
                    }
                }
                break;
            case EnemyType.Enemy_Dagger:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transformPoint.position.x + 10 * (i - 1), transformPoint.position.y, transformPoint.position.z);
                        enemy = Instantiate(Enemies_Dagger[i], pos, transformPoint.rotation);
                        enemy.transform.parent = transformPoint;
                        enemy.GetComponent<Enemy>().initialized(i);
                        stageManager.AddEnemies(enemy);
                    }
                }
                break;
            case EnemyType.Enemy_Boss:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transformPoint.position.x + 10 * (i - 1), transformPoint.position.y, transformPoint.position.z);
                        enemy = Instantiate(Enemies_Boss[i], pos, transformPoint.rotation);
                        enemy.transform.parent = transformPoint;
                        enemy.GetComponent<Enemy>().initialized(i);
                        stageManager.AddEnemies(enemy);
                    }
                }
                break;
        }
    }

    public void InstantiateEnemy_Old()
    {
        transformPoint = GameObject.Find("enemyPoint").transform;
        EnemyColorType colorType = stageManager.ColorType;
        EnemyType type = stageManager.Type;
        switch (type)
        {
            case EnemyType.Enemy_Missile: { enemyPrefab = Enemies_Missile[(int)colorType]; } break;
            case EnemyType.Enemy_Arrow: {  enemyPrefab = Enemies_Arrow[(int)colorType]; } break;
            case EnemyType.Enemy_Dagger: {  enemyPrefab = Enemies_Dagger[(int)colorType]; } break;
            case EnemyType.Enemy_Boss: {  enemyPrefab = Enemies_Boss[(int)colorType]; } break;
        }
   
        enemy = Instantiate(enemyPrefab, transformPoint);
        enemy.transform.parent = transformPoint;
        enemy.GetComponent<Enemy>().initialized((int)colorType);
    }

*/
}
