using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{

    float x = 0.5f;
    float xOffset;

    float y = 0.5f;
    float yOffset;
    
    void Update()
    {
        yOffset -= (Time.deltaTime * y) / 2f;
        gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, yOffset);
        gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0, yOffset);
        gameObject.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, yOffset));

    }
}
