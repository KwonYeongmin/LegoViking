using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Viking : MonoBehaviour
{   
    public float angle = 0;
    private float lerpTimer = 0;
    public float Speed { set; get; }
    public float DefaultSpeed = 5;
    private float durationValue = 30.0f;
    Quaternion defaultRotation;

    private void Awake()
    {
        DefaultSpeed = 5;
        Speed = DefaultSpeed;
        defaultRotation = transform.localRotation;
    }

    public void Reset()
    {
        lerpTimer = 0;
        transform.rotation = new Quaternion(defaultRotation.x, defaultRotation.y, defaultRotation.z, defaultRotation.w);
    }
   
    private void FixedUpdate()
    {
        lerpTimer += Time.deltaTime * (Speed) / durationValue;
        transform.rotation = PendulumRotation();
    }

    Quaternion PendulumRotation()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
                                           Quaternion.Euler(Vector3.back * angle), 
                                           ((Mathf.Sin(lerpTimer) + 1.0f) * 0.5f));
    }

    public void SetSpeed(float value)
    {
        Speed = DefaultSpeed;
        Speed *= value;
    }

}
