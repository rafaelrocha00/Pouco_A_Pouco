using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reacao
{
    [SerializeField] GameObject objetoCorrespondente;
    [SerializeField] Habilidade habilidadeUsada;
    [SerializeField] int medoAdquirido;
    [SerializeField] int repeticoes;

    public void Reagir(NPC npc)
    {
        Debug.Log(npc.gameObject.name + " reagiu a algo.");
        if(npc.TeveOutrasReacoes() && npc.GetUltimaReacao() == this)
        {
            npc.AumentarMedo(medoAdquirido - repeticoes);
            Debug.Log("Repetiu reacao, novo medo é: " + (medoAdquirido - repeticoes));

        }
        else
        {
            npc.AumentarMedo(medoAdquirido);

        }
        npc.AdicionarNovaReacaoNaTimeline(this);
    }

    public GameObject getObjeto()
    {
        return objetoCorrespondente;
    }

    public Habilidade getHabilidade()
    {
        return habilidadeUsada;
    }
}
