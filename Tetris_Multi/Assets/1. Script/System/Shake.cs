using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public static Shake instance;
    Vector3 cameraInPos;
    public float shakeRange = 0.05f;
    private void Awake()
    {
        instance = this;
        cameraInPos = this.transform.position;
    }
    public void Shaking()
    {
        InvokeRepeating("StartShake", 0f, 0.1f);
        Invoke("StopShake", 0.1f);
    }

    void StartShake()
    {
        float x = Random.value * shakeRange * 2 - shakeRange;
        float y = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = this.transform.position;
        cameraPos.x += x;
        cameraPos.y += y;
        this.transform.position = cameraPos;
    }

    void StopShake()
    {
        CancelInvoke("StartShake");
        this.transform.position = cameraInPos;
    }
}
