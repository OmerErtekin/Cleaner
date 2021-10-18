using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShaker : MonoBehaviour
{
    private CinemachineVirtualCamera cineCam;
    public static CameraShaker shaker;
    private float intensity = 3;
    private bool isShaking = false;
    private void Awake()
    {
        shaker = this;
    }

    private void Start()
    {
        cineCam = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        SetCamera();
        if (Input.GetKey(KeyCode.S))
            CameraShake(3);
    }

    public void CameraShake(float value)
    {
        intensity = value;
        isShaking = true;
    }
    void SetCamera()
    {
        if (!isShaking)
            return;

        CinemachineBasicMultiChannelPerlin perlin = cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        intensity = Mathf.Lerp(intensity, 0, 2f* Time.deltaTime);

        if (intensity <= 0)
        {
            isShaking = false;
            intensity = 0;
            perlin.m_AmplitudeGain = 0;
        }

        perlin.m_AmplitudeGain = intensity;
    }
}
