using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class C_Graficos : MonoBehaviour
{
    [SerializeField]PostProcessVolume volume;
    ChromaticAberration Ca;
    DepthOfField Df;
    Vignette Vi;

    private void Start()
    {
        volume.profile.TryGetSettings(out Ca);
        volume.profile.TryGetSettings(out Df);
        volume.profile.TryGetSettings(out Vi);
    }

    public void AtivarVsynch(bool ativar)
    {
        if(ativar)
        {
            QualitySettings.vSyncCount = 1;

        }
        else
        {
            QualitySettings.vSyncCount = 0;

        }

    }

    public void AtivarFullscreen(bool ativar)
    {
        if (ativar)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void MudarLuminosidade(float valor)
    {
        Screen.brightness = valor;
    }

    public void MudarQualidadeGrafica(int valor)
    {
        QualitySettings.SetQualityLevel(valor);
    }

    public void AtivarEfeitoAberration(float intensidadeMax, float tempoMax)
    {
        StartCoroutine(AumentarAberration(intensidadeMax, tempoMax));
    }

    public void AtivarDephOfField()
    {
        Df.active = true;
    }

    public void FecharDephOfField()
    {
        Df.active = false;
    }

    public void AtivarDano()
    {
        StartCoroutine(MostrarDano());
        StartCoroutine(AumentarAberration(0.5f,0.5f));
    }

    IEnumerator MostrarDano()
    {
        Vi.active = true;
        Vi.intensity.value = 1;
        yield return new WaitForSeconds(0.2f);
        while (Vi.intensity.value > 0.1f)
        {
            Vi.intensity.value -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Vi.intensity.value = 0;
        Vi.active = false;
    }

    IEnumerator AumentarAberration(float intensidadeMax, float tempoMax)
    {
        yield return new WaitForSeconds(0.1f);
        Ca.enabled.value = true;
        float contador = 0;
        while(contador < intensidadeMax)
        {
            contador += 0.1f;
            Ca.intensity.value = contador;
            yield return new WaitForSeconds(0.05f);
        }
        contador = intensidadeMax;
        yield return new WaitForSeconds(tempoMax);

        while(contador > 0)
        {
            contador -= 0.1f;
            Ca.intensity.value = contador;
            yield return new WaitForSeconds(0.05f);
        }
        Ca.intensity.value = 0f;
        Ca.enabled.value = false;
    }
}
