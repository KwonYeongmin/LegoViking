using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{

    List<Enemy> Enemies = new List<Enemy>();
    List<EnemyHUD> EnemiesHUD = new List<EnemyHUD>();
    List<EnemyAttack> EnemyAttacks = new List<EnemyAttack>();

    public bool bIsEnemyClear {  get; private set; }
    public int Stage {  get; private set; }


    private void Awake()
    {
        bIsEnemyClear = true;
    }


    // =================================
    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        //Debug.Log("Enemy Count : "+Enemies.Count);
        bIsEnemyClear = false;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        if (Enemies.Count == 0) bIsEnemyClear = true;
    }

    public int GetEnemyNum()
    {
        return Enemies.Count;
    }
    // =================================

    public void AddEnemyHUD(EnemyHUD hud)
    {
        EnemiesHUD.Add(hud);
    }

    private void DestroyEnemyHUD()
    {
        foreach (EnemyHUD hud in EnemiesHUD)
            Destroy(hud.gameObject);
    }

    public void NextStage()
    {
        if (!bIsEnemyClear) return;
           
        GameObject.FindWithTag("GameCenter").GetComponent<GameCenter>().NextStage();
    }

    // =================================
    public void AddEnemyAttacks(EnemyAttack attack)
    {
        EnemyAttacks.Add(attack);
    }

    public void RemoveEnemyAttacks(EnemyAttack attack)
    {
        EnemyAttacks.Remove(attack);
    }


    // =================================

    public void Reset()
    {
        //enemies
        Enemies.Clear();
        //
        DestroyEnemyHUD();
        //hud
        EnemiesHUD.Clear();
    }

    // ================================
    public void SetStage(int stage)
    {
        Stage = stage;
    }
}
