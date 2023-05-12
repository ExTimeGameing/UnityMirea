using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
    void Start()
    {
        WebCamTexture webCamTexture = new WebCamTexture();

        Renderer renderer = GetComponent<Renderer>();

        renderer.material.mainTexture = webCamTexture;
        
        webCamTexture.Play();
    }
}
