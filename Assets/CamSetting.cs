using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        cam.orthographicSize *= (Screen.height / (Screen.width / 16.0f)) / 9.0f;
    }
}
