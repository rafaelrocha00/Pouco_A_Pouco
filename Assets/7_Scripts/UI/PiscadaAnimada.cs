using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiscadaAnimada : MonoBehaviour
{
    Image imagem;
    [SerializeField]List<Sprite> Animacao = new List<Sprite>();
    [SerializeField]float TempoDeAni;
    [SerializeField]float TempoEntreAnimacao;
    int index = 0;
    bool inverso;

    private void Start()
    {
        imagem = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if(imagem == null)
        {
            Start();
        }
        StartCoroutine(Animar());

    }

    IEnumerator Animar()
    {
        while (true)
        {

            if(index < 0)
            {
                index = 0;
                inverso = false;
                yield return new WaitForSeconds(TempoEntreAnimacao);

            }

            if (index >= Animacao.Count)
            {
                index = Animacao.Count - 1;
                inverso = true;
            }
            imagem.sprite = Animacao[index];
            yield return new WaitForSeconds(TempoDeAni);
            if (inverso)
            {
                index--;
            }
            else
            {
                index++;
            }

        }
    }
}
