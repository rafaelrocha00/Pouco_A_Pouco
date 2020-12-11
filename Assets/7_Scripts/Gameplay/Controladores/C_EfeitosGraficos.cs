using System;
using UnityEngine;

public class C_EfeitosGraficos : MonoBehaviour
{
    Transicao FadeAtual;
    [SerializeField] Transicao FadeToBlack;
    [SerializeField] Transicao FadeToWhite;

    public void FadeINToWhite()
    {
        FadeAtual = FadeToWhite;
        FadeIn();
    }

    public void FadeINToBlack()
    {
        Debug.Log("Fade in to black chamado");
        FadeAtual = FadeToBlack;
        FadeIn();
    }

    void FadeIn()
    {
        AtivarObjeto();
        Debug.Log("Tocando transicao " + FadeAtual.gameObject.name);
        FadeAtual.TocarTransicao();
    }

    public void FadeOff()
    {
        if (FadeAtual == null)
        {
            FadeAtual = FadeToBlack;
        }

        AtivarObjeto();
        FadeAtual.TerminoudeMostrar += DesativarObjeto;
        FadeAtual.FadeOff();
    }

    void DesativarObjeto()
    {
        FadeAtual.TerminoudeMostrar -= DesativarObjeto;

        FadeAtual.gameObject.SetActive(false);
    }

    void AtivarObjeto()
    {
        FadeAtual.gameObject.SetActive(true);
    }

    public void AdicionarFimDoFade(Action action)
    {
        FadeAtual.TerminoudeMostrar += action;
    }

    public void RemoverFimDoFade(Action action)
    {
        FadeAtual.TerminoudeMostrar -= action;
    }
}
