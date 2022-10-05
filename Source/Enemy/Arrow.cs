using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : EnemyAttack
{
   // public AttackState state = AttackState.one;
    
    [Header("낙하 속도")]
    [SerializeField]
    private float arrow_fall_speed = 3.0f;

    [Header("낙하 속도")]
    private int arrow_damage = 0;
    
    [SerializeField] private int[] Damage;

    [Header("캐릭터 이동속도 감소 비율")]
    [SerializeField]
    private float arrow_speed_down = 20.0f; //화살 피격에 의한 캐릭터 이동속도 감소 비율


    [Header("lifetime")]
    [SerializeField]
    private float lifeTime = 1.0f;

    [Header("Particles")]
    public GameObject[] particles;

    [Header("Projector")]
    public Projector projector;
    public Material[] materials;

    private Rigidbody rig;

    private void Awake()
    {
        rig = this.GetComponent<Rigidbody>();
        InitializeState();
    }

    private void Start()
    {
       Destroy(this.gameObject, 10.0f);
        SoundManager.Instance.PlaySE(SoundList.Sound_arrow_stick,this.transform.position);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rig.AddForce(- transform.up * arrow_fall_speed);
    }

    public void InitializeState()
    {
        for(int i=0;i<3;i++) particles[i].SetActive(false);
        particles[(int)(colortype)].SetActive(true);

        arrow_damage = Damage[(int)(colortype)];

        projector.material = materials[(int)colortype];
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) //플레이어가 맞았다면
        {
            GiveDamage(collision.gameObject.GetComponent<Collider>());
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == ("deck")) Destroy(this.gameObject,1f);
    }


    private void GiveDamage(Collider collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(arrow_damage); //데미지 주기

        float value = collision.gameObject.GetComponent<Movement>().moveSpeed;
        value *= arrow_speed_down;
        collision.gameObject.GetComponent<Player>().DecreaseSpeed(value); //스피드 감소

        SoundManager.Instance.PlaySE(SoundList.Sound_arrow_hit, this.transform.position);
    }
}
