using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class C_Audio : MonoBehaviour
{
    [SerializeField]AudioMixer musica;
    [SerializeField]AudioMixer sons;
    [SerializeField]AudioSource[] SonsVFX;
    [SerializeField] AudioClip papel;
    [SerializeField] AudioClip flutuar;

   public void MudarVolumeMusica(float valor)
   {
        musica.SetFloat("Volume", valor);
   }

    public void MudarVolumeAudio(float valor)
    {
        sons.SetFloat("Volume", valor);
    }

    public void MostrarSomVFX(AudioClip clipe)
    {

        for (int i = 0; i < SonsVFX.Length; i++)
        {
            if (!SonsVFX[i].isPlaying)
            {
                SonsVFX[i].clip = clipe;
                SonsVFX[i].Play();
                return;
            }
        }
      
    }

    public void MostrarSomInteracao()
    {
        MostrarSomVFX(papel);
    }

    public void MostrarSomFlutuar()
    {
        MostrarSomVFX(flutuar);
    }
}
