using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missile : EnemyAttack
{
    [SerializeField]
    EnemyColorType colorType = EnemyColorType.GREY;

    [Header("중앙 피해량")]
    private int Missile_damage_center = 0;
    private int[] Damage_center;

    [Header("외곽 피해량")]
    private int Missile_damage_edge = 0;
    private int[] Damage_Edge;

    [Header("낙하 속도")]
    [SerializeField]
    private float Missile_fall_speed = 1.0f ; //미사일 낙하속도

    [Header("피해 범위")]
    [SerializeField]
    private float Missile_range_edge = 3.0f; //미사일 외곽 피해범위
    [SerializeField]
    private float Missile_range_center = 2.0f; //미사일 중앙 피해범위

    [Header("lifetime")]
    [SerializeField]
    private float lifeTime = 10.0f;
    
    private Rigidbody rigid;

    public GameObject FX;

    private void Awake()
    {
        colorType =  colorType;
        InitializeState(); //미사일 단계별 피해량 설정
    }

    private void InitializeState()
    {
        int center = 6;
        int edge = 3;

        Damage_center = new int[3];
        Damage_Edge = new int[3];

        for(int i = 0; i < 3; i++)
        {
            Damage_center[i] = center;
            Damage_Edge[i] = edge;

            center += 2;
            edge += 1;
        }

         Missile_damage_center = Damage_center[(int)(colorType)];
         Missile_damage_edge = Damage_Edge[(int)(colorType)];
    }

    private void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
        SoundManager.Instance.PlaySE(SoundList.Sound_missile_explosion, this.transform.position);
    }

    private void Update()
    {
        Move();
        JudgeEdgePlayer(Missile_damage_edge);
        JudgeEdgePlayer(Missile_damage_center);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Deck") ) //바이킹 갑판에 부딪히면 파괴
        {
            GameObject obj= Instantiate(FX, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f, this.gameObject.transform.position.z)
                                                , this.gameObject.transform.rotation);
            Destroy(obj.gameObject, 2f);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.GetComponent<Player>()) // 플레이어와 부딪히면
        {
            GiveDamage(collision.gameObject.GetComponent<Collider>(), Damage_center[(int)colorType]); //미사일 중앙 범위 피해
            Debug.Log("중심");

            Destroy(this.gameObject);
        }
    }

    private bool bDamage =false;

    private void GiveDamage(Collider collision,int damge)
    {
      if(!bDamage)  collision.gameObject.GetComponent<Player>().TakeDamage(damge);
        bDamage = true;
        Instantiate(FX, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y +5f , collision.gameObject.transform.position.z)
                                                ,collision.gameObject.transform.rotation);
        SoundManager.Instance.PlaySE(SoundList.Sound_missile_hit,this.transform.position);
    }


    private void JudgeEdgePlayer(int range) 
    {
        Collider[] collidersEdge = Physics.OverlapSphere(transform.position, Missile_range_edge);

        foreach (Collider collider in collidersEdge)
        {
            if (collider.gameObject.GetComponent<Player>())
            {
                GiveDamage(collider.gameObject.GetComponent<Collider>(), Damage_Edge[(int)colorType]);
                Destroy(this.gameObject);
            }
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Missile_range_center);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Missile_range_edge);
    }


    private void Move()
    {
        rigid.AddForce(transform.up* Missile_fall_speed);
    }
}
