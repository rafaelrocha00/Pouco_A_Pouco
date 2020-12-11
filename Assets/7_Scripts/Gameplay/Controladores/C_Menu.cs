using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SistemaDeInventario;

public class C_Menu : MonoBehaviour
{
    [SerializeField] List<ISelecionavel> selecionaveis = new List<ISelecionavel>();
    ISelecionavel selecionavelAtual;

    [SerializeField] GUI_Opcoes menuPrincipal;
    [SerializeField] GUI_Inventario inventario;

    private void Start()
    {
        selecionaveis.Add(GetComponent<C_Habilidade>());
        selecionaveis.Add(menuPrincipal);
        selecionaveis.Add(inventario);

    }

    private void Update()
    {
        ObservarInputMenu();
    }

    public void ObservarInputMenu()
    {

        if (Input.GetButtonDown("MenuHabilidade"))
        {
            PausarJogo();
            Selecionar(0);
        }

        if (Input.GetButtonDown("MenuPrincipal"))
        {
            PausarJogo();
            Selecionar(1);
        }

        if (Input.GetButtonDown("MenuInventario"))
        {
            PausarJogo();
            Selecionar(2);
        }
    }

    public void PausarJogo()
    {
        GameManager.instancia.PararJogador();
        Time.timeScale = 0;
    }

    public void LiberarJogo()
    {
        GameManager.instancia.LiberarJogador();
        Time.timeScale = 1;
    }

    public void Selecionar(int index)
    {
        bool resposta = true;

        if (selecionavelAtual != null)
        {
            resposta = selecionavelAtual.Deselecionar();

        }

        if (selecionavelAtual != selecionaveis[index])
        {
            Debug.Log("MenuAberto");
            selecionavelAtual = selecionaveis[index];
            selecionavelAtual.Selecionar();
        }
        else
        {
            if (resposta)
            {
                selecionavelAtual = null;
                LiberarJogo();
            }
          
        }
    }


}

interface ISelecionavel
{
    void Selecionar();
    bool Deselecionar();
}
