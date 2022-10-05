using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //[HideInInspector]
    public AudioClip title;
 public AudioClip btnSFX;

    private void Awake()
    {
     if(SceneManager.GetActiveScene().name.Equals("Title") || SceneManager.GetActiveScene().name.Equals("GameClear") )
            SoundManager.Instance.PlayBGM(title);   
    }

    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void PlayBtnSFX()
    {
        SoundManager.Instance.PlayUIAudio(btnSFX);
    }
}
