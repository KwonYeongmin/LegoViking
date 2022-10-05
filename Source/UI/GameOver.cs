using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private SceneManagement sceneManagement;

    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
    }

    public void ButtonReStartDown()
    {
        sceneManagement.ChangeScene("GameScene");
    }

    public void ButtonExitDown()
    {
        sceneManagement.QuitApplication();
    }
}
