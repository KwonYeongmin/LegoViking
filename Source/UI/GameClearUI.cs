using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    public GameObject panel;
    public SceneManagement sceneManagement;


    private void Start()
    {
        StartCoroutine(HideGameClearUI());
    }

    IEnumerator HideGameClearUI()
    {
        yield return new WaitForSecondsRealtime(3f);
        
        sceneManagement.ChangeScene("Title");
    }


}
