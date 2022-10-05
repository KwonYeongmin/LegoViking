using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFix : MonoBehaviour
{
    public Material normalGhostMaterial;
    public Material lightGhostMaterial;
    public Material darkGhostMaterial;
    public float runForSeconds = 5;

    private Renderer rend;
    private float timer = 0;
    private float blinkInterval = 0.4f;
    private float lastBlinkTime = 0f;
    private int blinker = 1;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    void Update()
    {
        checkTimesUp();
    }

    public void startRunning()
    {
        rend.material = darkGhostMaterial;
        startTimer();
    }

    void startTimer()
    {
        timer = Time.time;
    }

    void checkTimesUp()
    {
        if (Time.time - timer > runForSeconds)
        {
            rend.material = normalGhostMaterial;
            blinker = 0;
        }
        else
        {
            if (Time.time - timer > 2.5f && Time.time - timer < runForSeconds && Time.time > 5)
            {
                blink();
            }
        }
    }

    void blink()
    {
        if (blinkTimeChange())
        {
            blinker++;
        }
        if (blinker % 2 == 0)
        {
            rend.material = lightGhostMaterial;
        }
        else
        {
            rend.material = darkGhostMaterial;
        }
    }

    void startBlinkTime()
    {
        lastBlinkTime = Time.time;
    }

    bool blinkTimeChange()
    {
        if (Time.time - lastBlinkTime > blinkInterval)
        {
            startBlinkTime();
            return true;
        }
        else
        {
            return false;
        }
    }
}
