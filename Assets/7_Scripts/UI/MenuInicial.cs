using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [SerializeField]Transicao transicao;
    string novoMapa;

    public void MudarMapa(string novoMapa)
    {
        this.novoMapa = novoMapa;
        transicao.TerminoudeMostrar += PosMudarMapa;
        transicao.TocarTransicao();

    }

    public void PosMudarMapa()
    {
        string mapaAntigo = SceneManager.GetActiveScene().name;
        StartCoroutine(MudarMapaAsync(mapaAntigo, novoMapa));
    }

    IEnumerator MudarMapaAsync(string mapaAntigo, string mapaNovo)
    {
        AsyncOperation operantion = SceneManager.LoadSceneAsync(mapaNovo);
        while (!operantion.isDone)
        {
            yield return null;
        }

    }


    public void SairDoJogo()
    {
        Application.Quit();
    }
}
