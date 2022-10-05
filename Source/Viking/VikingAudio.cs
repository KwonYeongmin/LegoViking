using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingAudio : MonoBehaviour
{
   public  AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "Viking")
        {
            SoundManager.Instance.PlaySE(SoundList.Sound_viking, this.transform.position);
            //  audioSource.clip = SoundList.Sound_viking;
            // audioSource.Play();
            //Debug.Log(other.gameObject);

        }
    }

}
