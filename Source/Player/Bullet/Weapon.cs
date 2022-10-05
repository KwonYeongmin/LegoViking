using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate;
    public Transform bulletPos;
    public GameObject bullet;
    public int currentAmmo;

    private void Awake()
    {
        currentAmmo = GetComponentInParent<Player>().defaultAmmo;
    }

    public void Use()
    {
        if(currentAmmo > 0)
        {
            SoundManager.Instance.PlaySE(SoundList.Sound_shoot, transform.position);
            currentAmmo--;
            StartCoroutine(Shot());
        }
        else
        {
            SoundManager.Instance.PlaySE(SoundList.Sound_lack, transform.position);
        }
    }

    IEnumerator Shot()
    {
        // #. √—æÀ πﬂªÁ
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;
        // #. ≈∫«« πË√‚
        yield return null;
    }
}


