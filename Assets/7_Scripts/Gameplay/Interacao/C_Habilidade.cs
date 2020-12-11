using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;
using Save;

public class C_Habilidade : MonoBehaviour, ISelecionavel, ITagable
{
    [SerializeField] int pontos;
    [SerializeField] Habilidade habilidadeAtual;
    [SerializeField]List<Habilidade> habilidadesExistentes = new List<Habilidade>();
    [SerializeField] List<Habilidade> habilidadesPossuidas = new List<Habilidade>();
    int indexHabilidade = 0;
    [SerializeField] Habilidade mudarMapa;
    [SerializeField] GameObject menuHabilidades;

    Action ganhouPontos;

    private void Update()
    {
        MudarHabilidadeAtual();
    }

    public Habilidade GetHabilidade(int index)
    {
        return habilidadesExistentes.Find(x => x.GetIndex() == index);
    }

    public Habilidade GetHabilidadeAtual()
    {
        return habilidadeAtual;
    }

    public Habilidade GetMudarMapa()
    {
        return mudarMapa;
    }

    public void Selecionar()
    {
        menuHabilidades.SetActive(true);
        GameManager.instancia.PararJogador();
        Debug.Log("Menu de habilidades aberto");
    }

    public bool Deselecionar()
    {
        menuHabilidades.SetActive(false);
        GameManager.instancia.LiberarJogador();
        return true;
    }

    public int GetPontos()
    {
        return pontos;
    }

    public void AdicionarPonto()
    {
        pontos++;
        ganhouPontos?.Invoke();
    }

    public void AdicionarINGanharPontos(Action action)
    {
        ganhouPontos += action;
    }

    public bool PodeComprar(Habilidade habilidade)
    {
        if (pontos >= habilidade.GetCusto())
        {
            return true;
        }

        return false;
    }

    public void ComprarHabilidade(Habilidade habilidade)
    {
        if(pontos >= habilidade.GetCusto())
        {
            pontos -= habilidade.GetCusto();
            habilidadesPossuidas.Add(habilidade);
        }
    }  

    void MudarHabilidadeAtual()
    {
        if (Input.GetButtonDown("MudarHabilidade"))
        {
            indexHabilidade++;
            if (indexHabilidade == habilidadesPossuidas.Count)
            {
                indexHabilidade = 0;
            }
        }

        habilidadeAtual = habilidadesPossuidas[indexHabilidade];
    }

    public bool temHabilidade(Habilidade habilidade)
    {
        return habilidadesPossuidas.Exists(x => x == habilidade);
    }

    public string AcharTags(string mensagem)
    {
        Debug.Log("Achar tag chamado");

        string PadraoDarSkill = "<GiveSkill<[0-9]>>";


        MatchCollection matches = Regex.Matches(mensagem, PadraoDarSkill);
        if (matches.Count > 0)
        {
            int valor = int.Parse(Regex.Match(matches[0].Value, @"\d+").Value);
            habilidadesPossuidas.Add(habilidadesExistentes[valor]);
            return matches[0].Value;
            
        }
        else
        {
            Debug.Log("Nenhuma tag encontrada");
        }

        return "";
    }

    public void Salvar(C_Save c_save)
    {
        c_save.Salvar("PontosAtuais", pontos);
        c_save.Salvar("HabilidadeAtual", habilidadeAtual.GetIndex());
        c_save.Salvar("QuantidadeHabilidades", habilidadesPossuidas.Count);
        for (int i = 0; i < habilidadesPossuidas.Count; i++)
        {
            c_save.Salvar("habilidade" + i.ToString(), i);
        }

    }

    public void Carregar(C_Save c_Save)
    {
        habilidadesPossuidas.Clear();
        habilidadeAtual = null;

        pontos = (int)c_Save.Carregar("PontosAtuais");
        int Index = (int)c_Save.Carregar("HabilidadeAtual");
        habilidadeAtual = habilidadesExistentes.Find(x => x.GetIndex() == Index);

        Index = (int)c_Save.Carregar("QuantidadeHabilidades");
        int indexNovaHabilidade;

        for (int i = 0; i < Index; i++)
        {
            indexNovaHabilidade = (int)c_Save.Carregar("habilidade" + i.ToString());
            habilidadesPossuidas.Add(habilidadesExistentes.Find(x => x.GetIndex() == indexNovaHabilidade));
        }
    }
}
