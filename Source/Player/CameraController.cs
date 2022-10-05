using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    
    public float maxZPos;
    public float minZPos;
    public float maxXPos;
    public float minXPos;

    private void Awake()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = player.transform.position + offset;
        if (newPos.x <= minXPos)
        {
            newPos.x = minXPos;
        }
        if (newPos.x >= maxXPos)
        {
            newPos.x = maxXPos;
        }
        if (newPos.z <= minZPos)
        {
            newPos.z = minZPos;
        }
        if (newPos.z >= maxZPos)
        {
            newPos.z = maxZPos;
        }

        transform.position = newPos;
    }
}
