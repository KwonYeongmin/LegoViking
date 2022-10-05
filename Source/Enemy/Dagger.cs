using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : EnemyAttack
{

    [Header("낙하 속도")]
    [SerializeField]
    private float Dagger_fall_speed = 2.0f; 

    [Header("낙하 후 이동 거리")]
    [SerializeField]
    private float Dagger_range = 2.0f; 

    [Header("낙하 후 속도")]
    [SerializeField]
    private float dagger_speed = 2.0f;

    [Header("피해량")]
    private int dagger_damage = 0;
    [SerializeField]
    private int[] Damage;

    [Header("lifetime")]
    [SerializeField]
    private float lifeTime = 10.0f;
    public GameObject[] particles;

    [Header("Projector")]
    public Projector Projector;
    public Material[] ProjectorMaterials;

    private Rigidbody rig;

    float[] direction = new float[4];
   [HideInInspector] public Transform daggerTransform;

    private bool bIsFallen = false;

    private void Awake()
    {
        rig = this.GetComponent<Rigidbody>();
        InitializeState(); // 단계별 피해량 설정

        direction[0] = 0;
        direction[1] = 90;
        direction[2] = -90;
        direction[3] = 180.0f;
        float num = direction[Random.Range(0, 4)];
        this.gameObject.transform.rotation = Quaternion.Euler(90.0f, num, 0);

        bIsFallen = false;
    }


    public void InitializeState()
    {
        dagger_damage = Damage[(int)(colortype)];

        for(int i=0;i<3;i++) particles[i].SetActive(false);
        particles[(int)(colortype)].SetActive(true);

        Projector.material = ProjectorMaterials[(int)colortype];
    }



    private void Update()
    {
         Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            GiveDamage(other);
            Debug.Log("triggerEnter");
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag != "deck" && other.gameObject.tag != "Player")
            Destroy(this.gameObject);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "deck")
        {
            bIsFallen = true;
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "deck") Destroy(this.gameObject);
    }

    private void GiveDamage(Collider collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(dagger_damage);
        Debug.Log(dagger_damage);
        SoundManager.Instance.PlaySE(SoundList.Sound_dagger_hit, this.transform.position);
    }

    



    private void Move()
    {

        if (!bIsFallen)
        {
            Vector3 direction = transform.forward;
           float sp= Dagger_fall_speed;
            rig.AddForce(direction * sp);
        }
        else
        {
            Vector3 direction = - transform.up;
            float sp = dagger_speed;
            rig.AddForce(direction * sp);
            transform.Rotate(0.0f,0.0f,0.0f);
            this.GetComponent<AudioSource>().Play();
            daggerTransform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
        }

    }

    /*
    private void CheckDeck()
    {
        Collider[] collidersEdge = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (Collider collider in collidersEdge)
            if (collider.gameObject.tag == "deck")
                bIsFallen = true;
    }*/
}
