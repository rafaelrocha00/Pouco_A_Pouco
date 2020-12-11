using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HabilidadeBotao : MonoBehaviour
{
    [SerializeField] Habilidade habilidadeAssociada;
    [SerializeField] GUI_Habilidades gui;
    [SerializeField] C_Habilidade c_habilidade;
    Button butaoAssociado;
    [SerializeField]Image icone;
    [SerializeField] Image imagemDeSelecao;
    Image Checker;

    private void Start()
    {
        butaoAssociado = GetComponent<Button>();
        Checker = GetComponent<Image>();
        c_habilidade = gui.GetControlador();
        butaoAssociado.onClick.AddListener(setarParametros);
        if (!c_habilidade.temHabilidade(habilidadeAssociada))
        {
            Bloquear();
        }
    }

    void setarParametros()
    {
        if (gui.EstaSelecionado(this) && c_habilidade.PodeComprar(habilidadeAssociada))
        {
            Debug.Log("Comprando habilidade");
            Comprar();
        }

        Debug.Log("Setando parametros");
        gui.SetarUI(habilidadeAssociada.GetNome(), habilidadeAssociada.GetDescricao(), habilidadeAssociada.GetCusto(), this);
        imagemDeSelecao.gameObject.SetActive(true);
       

    }

    void Comprar()
    {
        c_habilidade.ComprarHabilidade(habilidadeAssociada);
        Checker.color = Color.white;
        icone.color = Color.white;
    }

    void Bloquear()
    {
        Checker.color = gui.GetColor();
        icone.color = gui.GetColor();
    }

    public void Deselecionar()
    {
        imagemDeSelecao.gameObject.SetActive(false);
    }
}
