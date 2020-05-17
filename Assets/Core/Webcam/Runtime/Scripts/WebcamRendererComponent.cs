using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class WebcamRendererComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private RawImage m_webcamImage;

    private WebCamTexture m_texture;

    void Start()
    {
        m_texture = new WebCamTexture();
        
        m_webcamImage.texture = m_texture;
        m_webcamImage.material.mainTexture = m_texture;
        m_texture.Play();

        
    }

    private void FixedUpdate()
    {
        var data = m_texture.GetPixels32();

        
    }

    private void OnDestroy()
    {
        m_texture.Stop();
    }
}
