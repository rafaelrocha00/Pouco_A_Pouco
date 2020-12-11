using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI_Habilidades : MonoBehaviour
{
    C_Habilidade controlador;
    HabilidadeBotao selecionadoAtual;
    [SerializeField] TextMeshProUGUI localNome;
    [SerializeField] TextMeshProUGUI localDescricao;
    [SerializeField] TextMeshProUGUI localPreco;
    [SerializeField] Color corIconeDisabled;


    public void SetarUI(string nome, string descricao, int custo, HabilidadeBotao habilidadeAtual)
    {
        localNome.text = nome;
        localDescricao.text = descricao;
        localPreco.text = controlador.GetPontos().ToString() + "/" + custo.ToString();
        if(controlador.GetPontos() >= custo)
        {
            localPreco.color = Color.green;
        }
        else
        {
            localPreco.color = Color.red;
        }
        SetarSelecionado(habilidadeAtual);
    }

    void SetarSelecionado(HabilidadeBotao novoBotao)
    {
        if(selecionadoAtual != null && novoBotao != selecionadoAtual)
        {
            selecionadoAtual.Deselecionar();
        }

        selecionadoAtual = novoBotao;
    }

    public Color GetColor()
    {
        return corIconeDisabled;
    }

    public bool EstaSelecionado(HabilidadeBotao botao)
    {
        if(selecionadoAtual == botao)
        {
            return true;   
        }

        return false;
    }

   public C_Habilidade GetControlador()
    {
        if(controlador == null)
        {
            controlador = GameManager.instancia.GetComponent<C_Habilidade>();
        }
        return controlador;
    }
}
