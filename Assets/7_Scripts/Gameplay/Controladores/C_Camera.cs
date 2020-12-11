using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class C_Camera : MonoBehaviour
{
    float ZoomPadrao;
    Camera mainC;
    [SerializeField]CinemachineVirtualCamera Atual;
    [SerializeField] CinemachineFramingTransposer transposer;
    [SerializeField] CinemachineVirtualCamera cameraObjetos;
    CinemachineBasicMultiChannelPerlin perlimObjetos;
    Transform focoPadrao;

    private void Start()
    {
        if(mainC == null)
        {
            mainC = Camera.main;
            DontDestroyOnLoad(mainC);

        }


        transposer = Atual.GetCinemachineComponent<CinemachineFramingTransposer>();
        ZoomPadrao = transposer.m_CameraDistance;
        perlimObjetos = cameraObjetos.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    public void Zoom(float newZoom)
    {
        StartCoroutine(CZoom(newZoom));
    }

    public void ResetZoom()
    {
        StartCoroutine(CZoom(ZoomPadrao));
    }

    IEnumerator CZoom(float NovaDistancia, float contador = 0)
    {
        if (transposer.m_CameraDistance < NovaDistancia)
        {
            while (transposer.m_CameraDistance < NovaDistancia)
            {
                contador += Time.deltaTime;
                transposer.m_CameraDistance = Mathf.Lerp(transposer.m_CameraDistance, NovaDistancia, contador);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (transposer.m_CameraDistance > NovaDistancia)
            {
                contador += Time.deltaTime;
                transposer.m_CameraDistance = Mathf.Lerp(transposer.m_CameraDistance, NovaDistancia, contador);
                yield return new WaitForSeconds(0.01f);
            }
        }
       
    }

    public void MudarFoco(Transform transform)
    {
        focoPadrao = Atual.Follow;
        Atual.Follow = transform;
    }

    public void ResetFoco()
    {
        Atual.Follow = focoPadrao;
        Debug.Log("Resetar foco foi chamado");
    }

    public void MudarCamera(Transform transform)
    {
        Debug.Log("Camera mudou para" + transform.gameObject.name);
        cameraObjetos.Follow = transform;
        cameraObjetos.Priority = 11;
    }

    public void ResetarCamera()
    {
        cameraObjetos.Priority = 5;
    }

    public Camera GetCamera()
    {
        if(mainC == null)
        Start();
        return mainC;
    }

    public void ScreenShake(float duracao, float intensidade, float frequencia, bool objeto)
    {
        if (objeto)
        {            
            perlimObjetos.m_AmplitudeGain = intensidade;
            perlimObjetos.m_FrequencyGain = frequencia;
            Debug.Log("Screen shake chamado");
            StartCoroutine(EsperarCameraShake(duracao));
        }
    }

    IEnumerator EsperarCameraShake(float duracao)
    {
        yield return new WaitForSeconds(duracao);
        perlimObjetos.m_FrequencyGain = 0;
        perlimObjetos.m_AmplitudeGain = 0;
    }
}
