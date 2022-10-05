using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpawner : MonoBehaviour
{
    private EnemyType Type;
    private EnemyColorType ColorType;


    [Header("미사일 관련 변수")]
    public GameObject[] Missiles;
    public float RangeX = 2.0f;
    public float RangeZ = 3.0f;
    public float Interval { get; private set; }

    [Header("화살 관련 변수")]
    public GameObject Arrow;
    public float arrow_range = 3.0f;

    [Header("표창 관련 변수")]
    public GameObject Dagger;
    public float dagger_range = 2.0f;

    private Vector3 SpawnPoint;

    

    private void Awake()
    {
        SpawnPoint = new Vector3(0,23,0);
    }

 


    public void Initialized(EnemyType type, EnemyColorType colorType,float interval)
    {
        Type = type;
        ColorType = colorType;
        Interval = interval;
    }


    EnemyAttack Attack;
    GameObject AttackPrefab;


    GameObject GetAttackPrefab()
    {
        GameObject prefab = null;
        switch (Type)
        {
            case EnemyType.Enemy_Missile: { prefab = Missiles[(int)ColorType]; } break;
            case EnemyType.Enemy_Arrow: { prefab = Arrow; } break;
            case EnemyType.Enemy_Dagger: { prefab = Dagger; } break;
        }

        return prefab;
    }

    private void CreateAttack(GameObject prefab, bool bLastStage)
    {
        ChangePositionRandom();
        AttackPrefab = Instantiate(prefab, SpawnPoint, GetRot());
        EnemyManager.Instance.AddEnemyAttacks(AttackPrefab.GetComponent<EnemyAttack>());
    }


    public void InstantiateAttack()
    {
        CreateAttack(GetAttackPrefab(), true);
    }


    private IEnumerator Start()
    {
        while (true)
        {
            InstantiateAttack();
            yield return new WaitForSeconds(Interval);
        }
    }

    private void ChangePositionRandom()
    {
        SpawnPoint
            = new Vector3(Random.Range(-RangeX, RangeX)  , SpawnPoint.y, Random.Range(-RangeZ, RangeZ));
    }

    Quaternion GetRot()
    {
        Quaternion rot = new Quaternion(0,0,0,0);

        float[] direction = new float[4];
        direction[0] = 0;
        direction[1] = 90;
        direction[2] = -90;
        direction[3] = 180.0f;

        switch (Type)
        {
            case EnemyType.Enemy_Missile: { rot = Quaternion.Euler(180.0f, 0, 0); } break;
            case EnemyType.Enemy_Arrow: { rot = Quaternion.Euler(0, 0, 0); } break;
            case EnemyType.Enemy_Dagger: { rot = Quaternion.Euler(90.0f, direction[Random.Range(0, 4)], 0); } break;
        }
        return rot;
    }
    /*  

    IEnumerator Start()
    {

        float[] direction = new float[4];
        direction[0] = 0;
        direction[1] = 90;
        direction[2] = -90;
        direction[3] = 180.0f;

        while (true)
        {
          
            yield return new WaitForSeconds(interval);

            switch (Type)
            {
                case EnemyType.Enemy_Missile:
                    {
                        rot= Quaternion.Euler(180.0f, 0, 0);

                        if (EnemyManager.Instance.Stage % 4 != 3)
                        {
                            InstantiateAttack(Missiles[(int)ColorType]);

                        }

                        else
                        {
                            InstantiateAttack(Missiles[Random.Range(0, 3)]);

                        }
                    }
                    break;
                case EnemyType.Enemy_Arrow:
                    {
                        rot = Quaternion.Euler(0, 0, 0);

                        if (EnemyManager.Instance.Stage % 4 != 3)
                        {
                            InstantiateAttack(Arrow);
                            obj.GetComponent<Arrow>().SetColorType(ColorType);
                            obj.GetComponent<Arrow>().InitializeState();
                        }
                        else
                        {
                            InstantiateAttack(Arrow);
                            obj.GetComponent<Arrow>().SetColorType ((EnemyColorType)(Random.Range(0, 3)));
                            obj.GetComponent<Arrow>().InitializeState();// = (EnemyColorType)(Random.Range(0, 3));
                        }

                    } break;
                case EnemyType.Enemy_Dagger:
                    {
                        rot = Quaternion.Euler(90.0f, direction[Random.Range(0, 4)], 0);
                        if (stageManager.Stage % 4 != 3)
                        {
                            InstantiateAttack(Dagger);
                            obj.GetComponent<Dagger>().colortype = stageManager.ColorType;
                            obj.GetComponent<Dagger>().InitializeState();// = stageManager.ColorType;
                        }
                        else
                        {
                            InstantiateAttack(Dagger);
                            obj.GetComponent<Dagger>().colortype = (EnemyColorType)(Random.Range(0, 3));
                            obj.GetComponent<Dagger>().InitializeState();// = stageManager.ColorType;
                        }

                    }
                    break;
                case EnemyType.Enemy_Boss:
                    {
                        if (stageManager.Stage % 4 != 3)
                        {
                            int index = Random.Range(0, 3);
                            if (index == 0)
                            {
                                rot = Quaternion.Euler(180.0f, 0, 0);
                                InstantiateAttack(Missiles[stageManager.Stage % 4]);
                              
                            }
                            else if (index == 1)
                            {
                                rot = Quaternion.Euler(0, 0, 0);
                                InstantiateAttack(Arrow);
                                obj.GetComponent<Arrow>().colortype = stageManager.ColorType;
                                obj.GetComponent<Arrow>().InitializeState();// = stageManager.ColorType;

                            }
                            else if (index == 2)
                            {
                                rot = Quaternion.Euler(90.0f, direction[Random.Range(0, 4)], 0);
                                InstantiateAttack(Dagger);
                                obj.GetComponent<Dagger>().colortype = stageManager.ColorType;
                                obj.GetComponent<Dagger>().InitializeState();
                            }
                        }
                        else
                        {
                            int index_ = Random.Range(0, 3);

                          //  if (!stageManager.enemiesAttacklist.Contains(index_))
                            {
                                InstantiateAttack(Missiles[index_]);
                                int index = Random.Range(0, 3);
                                if (index == 0)
                                {
                                    rot = Quaternion.Euler(180.0f, 0, 0);
                                    InstantiateAttack(Missiles[Random.Range(0,3)]);
                                }
                                else if (index == 1)
                                {
                                    rot = Quaternion.Euler(0, 0, 0);
                                    InstantiateAttack(Arrow);
                                    obj.GetComponent<Arrow>().colortype = (EnemyColorType)(Random.Range(0, 3));
                                    obj.GetComponent<Arrow>().InitializeState();
                                }
                                else if (index == 2)
                                {
                                    rot = Quaternion.Euler(90.0f, direction[Random.Range(0, 4)], 0);
                                    InstantiateAttack(Dagger);
                                    obj.GetComponent<Dagger>().colortype = (EnemyColorType)(Random.Range(0, 3));
                                    obj.GetComponent<Dagger>().InitializeState();
                                }
                            }
                                
                        }
                    }
                    break;      
            }
            
        }
    }
    
    GameObject obj;

    public void InstantiateAttack(GameObject prefab )
    {
        ChangePositionRandom();

        obj = Instantiate(prefab, transform_.position, rot);
        obj.transform.parent = GameObject.Find("Deck").transform;
    }

    

 */



}
