using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject meshObj;
    public GameObject effectObj;
    private Rigidbody rigid;
    private TrailRenderer trail;

    public int damage;
    public float explosionTime;
    public float explosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionTime);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        ExplosionFX();
        HitGrenade();

        Destroy(gameObject, 2f);
    }

    private void ExplosionFX()
    {
        meshObj.SetActive(false);
        effectObj.SetActive(true);
        trail.enabled = false;
        SoundManager.Instance.PlaySE(SoundList.Sound_grenade, transform.position);
    }

    private void HitGrenade()
    {
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.forward, 0, LayerMask.GetMask("Enemy"));


        foreach (RaycastHit hitObj in rayHits)
        {
            hitObj.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            ExplosionFX();
            HitGrenade();
            Destroy(gameObject, 2.0f);
        }
    }
}
